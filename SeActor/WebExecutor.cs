using Akka.Actor;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeActor.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SeActor
{
    public class WebExecutor : ReceiveActor
    {
        private IWebDriver _driver;

        public static Props Props(IWebDriver driver)
        {
            return Akka.Actor.Props.Create(() => new WebExecutor(driver));
        }

        protected override void PostStop()
        {
            _driver.Dispose();
            base.PostStop();
        }

        public WebExecutor(IWebDriver driver)
        {
            _driver = driver;

            Receive<GetTab>((goTo) =>
            {
                var tabId = Guid.NewGuid();
                _driver.SwitchTo().NewWindow(WindowType.Tab);

                var tab = new BrowserTab(_driver.CurrentWindowHandle, Self);

                Sender.Tell(tab, Self);
            });

            Receive<GoTo>((goTo) =>
            {
                var tab = _driver.WindowHandles.FirstOrDefault(x => x == goTo.currentWindowHandle);

                if (tab != null)
                {
                    _driver.SwitchTo().Window(tab);
                    
                    //_driver.Navigate().GoToUrl(goTo.Url);
                    Task.Run(() => driver.Scripts().ExecuteScript($"setTimeout('', 5000); window.open('{goTo.Url}', '_self');"));
                    //driver.Url = goTo.Url;
                   

                    Sender.Tell(true, Self);
                }
            });

            Receive<FindElementBy>((findElementBy) =>
            {
                var tab = _driver.WindowHandles.FirstOrDefault(x => x == findElementBy.currentWindowHandle);

                if (tab != null)
                {
                    _driver.SwitchTo().Window(tab);

                    WithRetries(2, Context, Self, Sender, findElementBy, () =>
                    {
                        if (isReady())
                        {
                            try
                            {
                                var element = _driver.FindElement(findElementBy.ByClausule);

                                Sender.Tell(element, Self);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                        else
                        {
                            throw new Exception("faio");
                        }
                    });
                }
            });
        }

        public void WithRetries(int noSeconds, IUntypedActorContext context, IActorRef receiver, IActorRef sender, Object message, Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                context.System.Scheduler.ScheduleTellOnce(new TimeSpan(0, 0, noSeconds), receiver, message, sender);
                throw;
            }
        }

        public bool isReady()
        {
            return _driver.Scripts().ExecuteScript("return document.readyState").Equals("complete");
        }
    }
}