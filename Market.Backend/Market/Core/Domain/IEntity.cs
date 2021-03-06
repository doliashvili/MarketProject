﻿using System;

namespace Core.Domain
{
    public interface IEntity<out TId> where TId: IComparable, IEquatable<TId>
    {
        TId Id { get; }
    }
}