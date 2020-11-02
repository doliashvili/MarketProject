namespace Core.Commands
{
    public interface ICommand 
    {
        CommandMeta CommandMeta { get; }
    }
}