using Core.Events;

namespace Core.InternalEventSystem
{
    /// <summary>
    /// Internal event handler
    /// </summary>
    /// <typeparam name="T">IMessage</typeparam>
    public interface IInternalEventHandler<in T> : IEventHandler<T> 
        where T: IEvent 
    {
    }
}