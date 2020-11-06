using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace EventStore.MongoDb.Abstract
{
    public interface IMongoConnectionWrapper
    {
        IMongoClient MongoClient { get; }
    }
}
