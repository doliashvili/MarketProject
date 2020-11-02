namespace Core.Commands
{
    /// <summary>
    /// Command
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class Command<TId> : ICommand
    {
        public abstract TId Id { get; protected set; }
        public long? ExpectedVersion { get; private set; }
        public CommandMeta CommandMeta { get; private set; }

        //For json serialization
        protected Command(){ }
        protected Command(CommandMeta commandMeta, long? expectedVersion = null)
        {
            ExpectedVersion = expectedVersion;
            CommandMeta = commandMeta;
        }
    }
}