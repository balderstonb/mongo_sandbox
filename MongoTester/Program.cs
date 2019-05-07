using Mongo2Go;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Linq;

namespace MongoTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = MongoDbRunner.Start();

            var client = new MongoClient(runner.ConnectionString);

            var db = client.GetDatabase("testing");

            var collection = db.GetCollection<Foo>(nameof(Foo));

            collection.InsertOne(new Foo { Bar = "Hello" });
            collection.InsertOne(new Foo { Bar = "hello" });

            var results = collection.AsQueryable<Foo>().Where(f => f.Bar.ToLower() == "Hello".ToLower());

            foreach(var result in results)
            {
                Console.WriteLine(result.Bar);
            }
        }

        public class Foo
        {
            [BsonId]
            public ObjectId _id { get; set; }

            public string Bar { get; set; }
        }
    }
}
