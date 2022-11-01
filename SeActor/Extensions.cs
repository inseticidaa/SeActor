using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeActor
{
    public static class Extensions
    {
        public static IJavaScriptExecutor Scripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }
    }
}
