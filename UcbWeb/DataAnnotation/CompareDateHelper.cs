using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml;

namespace UcbWeb.DataAnnotation
{
    public static class CompareDateHelper
    {
        public static MvcHtmlString EditorForCompareDate<TModel, TValue>(
           this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression,
           string templateName = null, string htmlFieldName = null,
           object additionalViewData = null)
        {
            string mvcHtml = html.EditorFor(expression, templateName,
                        htmlFieldName, additionalViewData).ToString();

            //string element = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
            //            ExpressionHelper.GetExpressionText(expression));
            //string Key = html.ViewData.Model.ToString() + "." + element;
            //RequiredIfAttribute.countPerField.Remove(Key);
            //if (RequiredIfAttribute.countPerField.Count == 0)
            //    RequiredIfAttribute.countPerField = null;

            string pattern = @"data\-val\-comparedate[a-z]+";

            if (Regex.IsMatch(mvcHtml, pattern))
            {
                return MergeClientValidationRules(mvcHtml);
            }
            return MvcHtmlString.Create(mvcHtml);
        }
        public static MvcHtmlString MergeClientValidationRules(string str)
        {
            const string searchStr = "data-val-comparedate";
            const string val1Str = "propertytested";
            const string val2Str = "allowequaldates";
            const string val3Str = "isdatebefore";

            List<XmlAttribute> mainAttribs = new List<XmlAttribute>();
            List<XmlAttribute> val1Attribs = new List<XmlAttribute>();
            List<XmlAttribute> val2Attribs = new List<XmlAttribute>();
            List<XmlAttribute> val3Attribs = new List<XmlAttribute>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);
            XmlNode node = doc.DocumentElement;

            foreach (XmlAttribute attrib in node.Attributes)
            {
                if (attrib.Name.StartsWith(searchStr))
                {
                    if (attrib.Name.EndsWith("-" + val1Str))
                        val1Attribs.Add(attrib);
                    else if (attrib.Name.EndsWith("-" + val2Str))
                        val2Attribs.Add(attrib);
                    else if (attrib.Name.EndsWith("-" + val3Str))
                        val3Attribs.Add(attrib);
                    else
                        mainAttribs.Add(attrib);
                }
            }
            var mainAttrib = doc.CreateAttribute(searchStr + "multiple");
            var val1Attrib = doc.CreateAttribute(searchStr + "multiple-" + val1Str);
            var val2Attrib = doc.CreateAttribute(searchStr + "multiple-" + val2Str);
            var val3Attrib = doc.CreateAttribute(searchStr + "multiple-" + val3Str);

            mainAttribs.ForEach(new Action<XmlAttribute>(delegate(XmlAttribute attrib)
            {
                mainAttrib.Value += attrib.Value + "!";
                node.Attributes.Remove(attrib);
            }
            ));

            val1Attribs.ForEach(new Action<XmlAttribute>(delegate(XmlAttribute attrib)
            {
                val1Attrib.Value += attrib.Value + "!";
                node.Attributes.Remove(attrib);
            }
            ));

            val2Attribs.ForEach(new Action<XmlAttribute>(delegate(XmlAttribute attrib)
            {
                val2Attrib.Value += attrib.Value + "!";
                node.Attributes.Remove(attrib);
            }
            ));

            val3Attribs.ForEach(new Action<XmlAttribute>(delegate(XmlAttribute attrib)
            {
                val3Attrib.Value += attrib.Value + "!";
                node.Attributes.Remove(attrib);
            }
            ));

            mainAttrib.Value = mainAttrib.Value.TrimEnd('!');
            val1Attrib.Value = val1Attrib.Value.TrimEnd('!');
            val2Attrib.Value = val2Attrib.Value.TrimEnd('!');
            val3Attrib.Value = val3Attrib.Value.TrimEnd('!');

            node.Attributes.Append(mainAttrib);
            node.Attributes.Append(val1Attrib);
            node.Attributes.Append(val2Attrib);
            node.Attributes.Append(val3Attrib);

            return MvcHtmlString.Create(node.OuterXml);
        }
    }
}