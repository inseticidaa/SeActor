using SeActor.Bases;

namespace SeActor.Messages
{
    public class GoTo : BaseMessage
    {
        public string currentWindowHandle { get; private set; }

        public string Url { get; private set; }

        public GoTo(string url, string currentWindowHandle)
        {
            Url = url;
            this.currentWindowHandle = currentWindowHandle;
        }
    }
}