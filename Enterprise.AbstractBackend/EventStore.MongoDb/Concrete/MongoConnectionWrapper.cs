using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using EventStore.MongoDb.Abstract;

namespace EventStore.MongoDb.Concrete
{
    public class MongoConnectionWrapper : IMongoConnectionWrapper
    {
        public IMongoClient MongoClient { get; }


        public MongoConnectionWrapper(MongoConfig config)
        {
            MongoClient = new MongoClient(config.ConnectionString);
        }
    }
}
