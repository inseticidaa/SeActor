using Akka.Actor;
using OpenQA.Selenium;
using SeActor.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeActor
{
    public class TabNavigator : INavigation
    {
        private string _currentWindowHandle;
        private IActorRef _webExecutor;

        public TabNavigator(string currentWindowHandle, IActorRef webExecutor)
        {
            _currentWindowHandle = currentWindowHandle;
            _webExecutor = webExecutor;
        }

        public void Back()
        {
            throw new NotImplementedException();
        }

        public void Forward()
        {
            throw new NotImplementedException();
        }

        public void GoToUrl(string url)
        {
            var a = _webExecutor.Ask(new GoTo(url, _currentWindowHandle)).Result;
        }

        public void GoToUrl(Uri url)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
