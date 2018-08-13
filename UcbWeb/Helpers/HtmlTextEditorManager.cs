using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UcbWeb.Helpers
{
    public class HtmlTextEditorManager
    {
        /// <summary>
        /// Convert tagged text to HTML 
        /// </summary>
        /// <param name="taggedText">Tagged text string</param>
        /// <returns>Html string </returns>
        /// <remarks>Converts text content. If null, returns null</remarks>
        public static string ConvertTaggedTextToHtml(string taggedText)
        {
            if (null == taggedText) return null;

            return taggedText.Replace("\r\n", "<br/>")
                .Replace("[b]", "<strong>")
                .Replace("[/b]", "</strong>")
                .Replace("[h]", "<h4>")
                .Replace("[/h]", "</h4>")
                .Replace("[blue]", "<span class='blue-text'>")
                .Replace("[/blue]", "</span>")
                .Replace("[red]", "<span class='red-text'>")
                .Replace("[/red]", "</span>");
        }
    }
}