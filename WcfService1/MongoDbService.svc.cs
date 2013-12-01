using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ShareEntity.Entity;

namespace WcfService1
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드, svc 및 config 파일에서 클래스 이름 "MongoDbService"을 변경할 수 있습니다.
    // 참고: 이 서비스를 테스트하기 위해 WCF 테스트 클라이언트를 시작하려면 솔루션 탐색기에서Service1.svc나 MongoDbService.svc.cs를 선택하고 디버깅을 시작하십시오.
    public class MongoDbService : IMongoDbService
    {
        /// <summary>
        /// DB 커넥션 스트링
        /// </summary>
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
        /// <summary>
        /// 디폴트 ConnectionString(mongodb://localhost;)가 저장됩니다.
        /// </summary>
        public MongoDbService()
        {
            ConnectionString = "mongodb://localhost";
        }

        public MongoDbService( string connectionString)
        {
            ConnectionString = connectionString;
        }

        public MongoDbService(string connectionString, string dbName) : this(connectionString)
        {
            DatabaseName = dbName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private MongoServer CreateClientAndGetServer()
        {
            var client = new MongoClient(ConnectionString);
            return client.GetServer();
        }
        /// <summary>
        /// Get database
        /// </summary>
        /// <returns></returns>
        private MongoDatabase CreateClientAndGetDatabase()
        {
            var server = CreateClientAndGetServer();
            return server.GetDatabase(DatabaseName);
        }
        private MongoCollection<dynamic> CreateClientAndGetCollection()
        {
            var db = CreateClientAndGetDatabase();
                
            //Get Collection
            return db.GetCollection<dynamic>(CollectionName);
        }

        public IEnumerable<string> GetDatabaseNames()
        {
            var server = CreateClientAndGetServer();
            return server.GetDatabaseNames();
        }
        public IEnumerable<string> GetCollectionNames()
        {
            var database = CreateClientAndGetDatabase();
            return database.GetCollectionNames();
        }

        public dynamic FindOneById(string collectionName, ObjectId id)
        {
            CollectionName = collectionName;
            var collection = CreateClientAndGetCollection();
            //var query = Query<Entity2>.EQ(e => e.Id, id);
            return collection.FindOneById(id);
        }
        public string FindMany(string collectionName, Func<EntityBase, dynamic> expression, dynamic value)
        {
            CollectionName = collectionName;
            var collection = CreateClientAndGetCollection();
            var query = Query<EntityBase>.EQ(expression, value);
            MongoCursor<dynamic> cursor = collection.Find(query);

            return cursor.ToJson();
        }
        public dynamic FindMany(Func<dynamic,dynamic> expression, dynamic value)
        {
            return FindMany(CollectionName, expression, value);
        }
        public bool Save(ObjectId id, dynamic item)
        {
            throw new NotImplementedException();
        }

        public dynamic Update(ObjectId id, dynamic newValue)
        {
            throw new NotImplementedException();
        }

        public bool Insert(dynamic item)
        {
            if (string.IsNullOrEmpty(CollectionName))
            {
                throw new MongoDbServiceException("No CollectionName");
            }
            var collection = CreateClientAndGetCollection();
            var result = collection.Insert(item);
            return result.Ok;

        }
        public bool CreateDatabase(string name, Type type)
        {
            throw new NotImplementedException();
        }
        public string Select(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return string.Empty;
            }
            return string.Empty;
        }
        #region Example
        private void MongoDb()
        {
            //Connection MongoDB

            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();
            //Get database
            var db = server.GetDatabase("test");
            //Get Collection
            var collection = db.GetCollection<Entity2>("1234");

            //FindAll
            var cursor = collection.FindAll().GetEnumerator();
            while (cursor.MoveNext())
            {
                PrintEntity(cursor.Current);
            }

            //Insert
            var entity = new Entity2 { Name = "Tom" };
            collection.Insert(entity);
            var id = entity.Id;

            //Find
            var query = Query<Entity2>.EQ(e => e.Id, id);
            entity = collection.FindOne(query);
            PrintEntity(entity);
            //Save Document
            //Save is sends the entire document back to the server
            entity.Name = "Dick";
            var r = collection.Save(entity);
            PrintEntity(entity);
            //Update Document
            //Update is sends just Change
            //local entity is not changed
            var update = Update<Entity2>.Set(e => e.Name, "Harry");
            collection.Update(query, update);
            PrintEntity(entity);
            //AfterUpdate
            entity = collection.FindOne(query);
            PrintEntity(entity);

            //collection.Remove(query);

        }

        private class Entity2
        {
            public ObjectId Id { get; set; }

            public string Name { get; set; }
        }

        private static void PrintEntity(Entity2 entity)
        {
            Console.WriteLine("id:{0}, name:{1}", entity.Id, entity.Name);
        }
        #endregion

        
    }

    public class MongoDbServiceException : Exception
    {
        public MongoDbServiceException()
        {
                
        }
        public MongoDbServiceException(string message) :base(message)
        {
            
        }
    }
}