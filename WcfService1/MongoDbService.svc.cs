using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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

        private MongoCollection<dynamic> CreateClientAndGetCollection()
        {
            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();
            //Get database
            var db = server.GetDatabase(DatabaseName);
            //Get Collection
            return db.GetCollection<dynamic>(CollectionName);
        }


        public dynamic FindOneById(ObjectId id)
        {
            var collection = CreateClientAndGetCollection();
            //var query = Query<Entity2>.EQ(e => e.Id, id);
            return collection.FindOneById(id);
        }

        public bool Save(ObjectId id, dynamic item)
        {
            throw new NotImplementedException();
        }

        public dynamic Update(ObjectId id, dynamic newValue)
        {
            throw new NotImplementedException();
        }

        public ObjectId Insert(dynamic item)
        {
            throw new NotImplementedException();
        }

        public bool SetDatabase(string database, string collection)
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
            collection.Save(entity);
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
}
