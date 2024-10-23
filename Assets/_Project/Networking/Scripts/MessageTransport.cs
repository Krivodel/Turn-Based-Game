using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TurnBasedGame.Networking
{
    public class MessageTransport : IMessageTransport
    {
        private readonly Dictionary<string, List<MessageReceivedHandler>> _listeners = new();

        public void Send<TMessage>(TMessage message) where TMessage : struct
        {
            string id = message.GetType().Name;

            if (_listeners.TryGetValue(id, out var listeners))
            {
                MessageReceivedEventArgs e = new(JObject.FromObject(message).ToString());

                foreach (var listener in listeners)
                    listener?.Invoke(e);
            }
        }

        public IMessageTransport AddListener<TMessage>(MessageReceivedHandler handler) where TMessage : struct
        {
            string id = typeof(TMessage).Name;
            
            if (!_listeners.TryGetValue(id, out var listeners))
            {
                listeners = new();

                _listeners.Add(id, listeners);
            }

            listeners.Add(handler);

            return this;
        }

        public IMessageTransport RemoveListener<TMessage>(MessageReceivedHandler handler) where TMessage : struct
        {
            string id = typeof(TMessage).Name;

            if (_listeners.TryGetValue(id, out var listeners))
                listeners.Remove(handler);

            return this;
        }

        public IMessageTransport Clear()
        {
            _listeners.Clear();

            return this;
        }
    }
}
