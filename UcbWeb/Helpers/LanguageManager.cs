using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UcbWeb.Helpers
{
    public class LanguageManager
    {
        private ISessionManager sessionManager;

        /// <summary>
        /// Create new instance of LanguageManager
        /// </summary>
        /// <remarks>Use when no existing SessionManager object is available</remarks>
        public LanguageManager() : this(new SessionManager())
        {
        }

        /// <summary>
        /// Create new instance of LanguageManager
        /// </summary>
        /// <remarks>Use when an existing SessionManager object is available</remarks>
        public LanguageManager(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        public string GetLocale()
        {
            string locale = sessionManager.Locale;

            if (null == locale)
            {
                // Get users localisation
                locale = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();

                // If not Welsh then set to British English
                if (locale != "cy-GB") locale = "en-GB";

                // Save in session so we can override the default
                sessionManager.Locale = locale;
            }

            return locale;
        }
    }
}