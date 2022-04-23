using Shared;

namespace SimpleMultiplayer;

public class MessageHandler
{
    public interface IMessageHandler
    {
        Type ConcreteType { get; }
        bool HandleMessage(Messages.IMessage msg);
    }
    
    public abstract class MessageHandlerBase<T> : IMessageHandler where T : Messages.IMessage
    {
        public Type ConcreteType => typeof(T);

        public bool HandleMessage(Messages.IMessage msg)
        {
            if (!(msg is T concreteMsg)) 
                return false;
            
            Handle(concreteMsg);
            return true;
        }

        protected abstract void Handle(T msg);
    }
}