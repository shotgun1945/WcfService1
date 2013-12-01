using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ShareEntity.Entity;

namespace MyLifeService.Helper
{
    public class MongoHelper
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }

        public MongoClient MongoClient { get; set; }

        public MongoDatabase MongoDatabase { get; set; }

        public MongoHelper() : this("mongodb://localhost")
        {

        }

        public MongoHelper(string connectionString)
        {
            ConnectionString = connectionString;
            MongoClient = CreateClient();
        }

        public MongoHelper(string connectionString, string dbName) : this(connectionString)
        {
            DatabaseName = dbName;
            MongoDatabase = CreateClientAndGetDatabase();
        }


        /// <summary>
        /// MongoDatabase 객체에서 컬렉션을 가져온다. 콜렉션 네임이 없으면 MonogoDbServiceException 에러 발생.
        /// 
        /// </summary>
        /// <returns></returns>
        private MongoCollection<dynamic> CreateClientAndGetCollection()
        {
            if (string.IsNullOrEmpty(CollectionName))
            {
                throw new MongoDbServiceException("No CollectionName");
            }
            if (MongoDatabase == null)
            {
                MongoDatabase = CreateClientAndGetDatabase();
            }

            //Get Collection
            return MongoDatabase.GetCollection<dynamic>(CollectionName);
        }

        private MongoServer CreateClientAndGetServer()
        {
            var client = CreateClient();
            return client.GetServer();
        }

        private MongoClient CreateClient()
        {
            var client = new MongoClient(ConnectionString);            
            return client;
        }

        /// <summary>
        /// Get database
        /// </summary>
        /// <returns></returns>
        private MongoDatabase CreateClientAndGetDatabase()
        {
            if (string.IsNullOrEmpty(CollectionName))
            {
                throw new MongoDbServiceException("No DatabaseName");
            }
            var server = CreateClientAndGetServer();
            return server.GetDatabase(DatabaseName);
        }
        public dynamic FindOneById(string collectionName, ObjectId id)
        {
            CollectionName = collectionName;
            var collection = CreateClientAndGetCollection();
            //var query = Query<Entity2>.EQ(e => e.Id, id);
            return collection.FindOneById(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1">return Type</typeparam>
        /// <typeparam name="T2">Query Type</typeparam>
        /// <param name="expression">해당 엔티티의 표현식</param>
        /// <param name="value">리턴값</param>
        /// <returns></returns>
        public IEnumerator<dynamic> FindObject<T1, T2>(Expression<Func<T1, T2>> expression, T2 value)
        {
            var collection = CreateClientAndGetCollection();
            var query = Query<T1>.EQ(expression, value);
            var cursor = collection.Find(query);

            return cursor.GetEnumerator();
        }

        public bool Insert(dynamic item)
        {
            var collection = CreateClientAndGetCollection();
            var result = collection.Insert(item);
            return result.Ok;

        }

        public bool UpdateOneObject(dynamic entity)
        {
            var collection = CreateClientAndGetCollection();
            return collection.Save(entity).Ok;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1">Target type</typeparam>
        /// <typeparam name="T2">Query Type</typeparam>
        /// <typeparam name="T3">update type</typeparam>
        /// <param name="qureyExpression"></param>
        /// <param name="findValue">query value</param>
        /// <param name="updateExpression"></param>
        /// <param name="updateValue">update value</param>
        /// <returns></returns>
        public bool UpdateObject<T1, T2, T3>(Expression<Func<T1, T2>> qureyExpression, T2 findValue,
            Expression<Func<T1, T3>> updateExpression, T3 updateValue)
        {
            var collection = CreateClientAndGetCollection();
            var query = Query<T1>.EQ(qureyExpression, findValue);
            var updateQuery = Update<T1>.Set(updateExpression, updateValue);
            WriteConcernResult result = collection.Update(query, updateQuery);
            return result.Ok;
        }

        public bool DeleteObejct<T1, T2>(Expression<Func<T1, T2>> expression, T2 value)
        {
            var collection = CreateClientAndGetCollection();
            var query = Query<T1>.EQ(expression, value);
            WriteConcernResult result = collection.Remove(query);

            return result.Ok;
        }
    }
    public class MongoDbServiceException : Exception
    {
        public MongoDbServiceException()
        {

        }
        public MongoDbServiceException(string message)
            : base(message)
        {

        }
    }
}
    

