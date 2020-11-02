namespace Core.MessageBroker
{
    /// <summary>
    /// Consumer's EventHandler result
    /// </summary>
    public enum HandleResult : byte
    {
        Success = 1,
        Fail = 2
    }
}
