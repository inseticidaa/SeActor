using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeActor.Messages
{
    public class FindElementBy
    {
        public string currentWindowHandle { get; private set; }

        public By ByClausule { get; private set; }

        public FindElementBy(By byClausule, string currentWindowHandle)
        {
            this.ByClausule = byClausule;
            this.currentWindowHandle = currentWindowHandle;
        }
    }
}
