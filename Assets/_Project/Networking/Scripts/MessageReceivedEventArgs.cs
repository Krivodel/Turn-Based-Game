using Newtonsoft.Json.Linq;

namespace TurnBasedGame.Networking
{
    public sealed class MessageReceivedEventArgs
    {
        private readonly string _data;

        public MessageReceivedEventArgs(string data)
        {
            _data = data;
        }

        public T GetMessage<T>()
        {
            return JObject.Parse(_data).ToObject<T>();
        }
    }
}
