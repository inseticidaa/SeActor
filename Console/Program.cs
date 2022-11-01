using Akka.Actor;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SeActor;
using SeActor.Messages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

new DriverManager().SetUpDriver(new EdgeConfig());

var driver = new EdgeDriver();

var prices = new Dictionary<string, string>();

//foreach (var i in Enumerable.Range(1, 10))
//{
//    var site = $"http://automationpractice.com/index.php?id_product={i}&controller=product";

//    driver.Navigate().GoToUrl(site);

//    try
//    {
//        var priceElement = driver.FindElement(By.ClassName("our_price_display"));

//        var price = priceElement.Text;

//        prices.Add(site, price);
//    }
//    catch
//    {
//    }
//}

var system = ActorSystem.Create("MySystem");

var executor = system.ActorOf(WebExecutor.Props(driver), "demo");

Parallel.ForEach(Enumerable.Range(1, 10), (i) =>
{
    var tab = executor.Ask<BrowserTab>(new GetTab()).Result;

    var site = $"http://automationpractice.com/index.php?id_product={i}&controller=product";

    tab.Navigate().GoToUrl(site);

    var priceElement = tab.FindElement(By.ClassName("our_price_display"));

    if (priceElement != null)
    {
        var price = priceElement.Text;

        prices.Add(site, price);
    }
});

var res = executor.GracefulStop(TimeSpan.FromMinutes(1)).Result;

var teste = 1;