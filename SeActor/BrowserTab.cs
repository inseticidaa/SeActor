using Akka.Actor;
using OpenQA.Selenium;
using SeActor.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SeActor
{
    public class BrowserTab : IWebDriver
    {
        private string _currentWindowHandle;
        private IActorRef _webExecutor;

        public BrowserTab(string currentWindowHandle, IActorRef webExecutor)
        {
            _currentWindowHandle = currentWindowHandle;
            _webExecutor = webExecutor;
        }

        public string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Title => throw new NotImplementedException();

        public string PageSource => throw new NotImplementedException();

        public string CurrentWindowHandle => throw new NotImplementedException();

        public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElement(By by)
        {
            return _webExecutor.Ask<IWebElement>(new FindElementBy(by, _currentWindowHandle)).Result;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public IOptions Manage()
        {
            throw new NotImplementedException();
        }

        public INavigation Navigate()
        {
            return new TabNavigator(_currentWindowHandle, _webExecutor);
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }
    }
}
