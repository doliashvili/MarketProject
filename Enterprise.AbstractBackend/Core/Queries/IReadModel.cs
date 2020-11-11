using System;

namespace Core.Queries
{
    /// <summary>
    /// ReadModel interface
    /// </summary>
    public interface IReadModel<TId>
    where TId : IComparable , IEquatable<TId>
    {
       public TId Id { get; set; }
    }
}