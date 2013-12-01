using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLifeService.Helper;

namespace TestProject
{
    using ShareEntity.Entity;

    using WcfService1;

    [TestClass]
    public class MongoDBSServiceTest
    {
        //[TestMethod]
        /// <summary>
        /// 생각해보니 의미 없는 테스트.
        /// </summary>
        public void Test_CreateDatabase()
        {
            var mongoService = new MongoDbService();
            var dbName = "Users";
            if (mongoService.CreateDatabase(dbName, typeof(User)))
                Assert.Fail();
            var cols = mongoService.GetDatabaseNames().ToList();
            Assert.IsTrue(cols.Contains(dbName));
        }
        //[TestMethod]
        /// <summary>
        /// 생각해보니 의미 없는 테스트.
        /// </summary>
        public void Test_CreateCollection()
        {
            var mongoService = new MongoDbService();
            var dbName = "Users";
            mongoService.DatabaseName = dbName;

        }
        [TestMethod]
        public void Test_Insert_User()
        {
            var mongoService = new MongoDbService();
            var dbName = "mylife";
            var colName = "Users";
            var user = new User();
            user.LoginId = "admin";
            user.Name = "관리자";
            user.Password = "admin";
            mongoService.DatabaseName = dbName;
            mongoService.CollectionName = colName;
            var objId = mongoService.Insert(user);
            Assert.IsNotNull(objId);
        }
        [TestMethod]
        public void TestHeplerCRUDUser()
        {
            var mongoHelper = new MongoHelper("mongodb://localhost", "mylife");
            mongoHelper.CollectionName = "Users";
            var user = new User();
            var time = DateTime.Now.Ticks;
            user.LoginId = "TestUser"+time;
            user.Name = "테스트"+time;
            user.Password = "1234";

            var isInserted = mongoHelper.Insert(user);
            Assert.IsTrue(isInserted,"InsertError");
            //읽기
            var findUser = mongoHelper.FindObject<User, string>(u => u.LoginId, user.LoginId);

            Assert.IsNotNull(findUser,"FindError");
            var updatedValue = "UpdatedTestUser";
            //업데이트 다수 (Update)
            var isUpdateMany = mongoHelper.UpdateObject<User, string, string>(u => u.LoginId, user.LoginId,
                u => u.LoginId, updatedValue);
            
            Assert.IsTrue(isUpdateMany, "UpdateObject Method Error");

            findUser = mongoHelper.FindObject<User, string>(u => u.LoginId, updatedValue);
            
            Assert.IsNotNull(findUser, "Updated Object Find Error");

            updatedValue = "SavedTestUser";
            //업데이트 한개 (Save)
            user.LoginId = updatedValue;
            var isUpdateOne = mongoHelper.UpdateOneObject(user);

            Assert.IsTrue(isUpdateOne, "UpdateObject Method Error");

            findUser = mongoHelper.FindObject<User, string>(u => u.LoginId, updatedValue);

            while (findUser.MoveNext())
            {
                Assert.IsTrue(findUser.Current.LoginId == user.LoginId, "Save Object Error");
            }

            //다수 삭제
            var isdeleted = mongoHelper.DeleteObejct<User, string>(u => u.LoginId, updatedValue);

            Assert.IsTrue(isdeleted, "Delete Method Error");;
            //생성
            //var mongoService = new MongoDbService();
            //var dbName = "mylife";
            //var colName = "Users";
            //var user = new User();
            //user.LoginId = "TestUser";
            //user.Name = "테스트";
            //user.Password = "1234";
            //mongoService.DatabaseName = dbName;
            //mongoService.CollectionName = colName;
            //var isInserted = mongoService.Insert(user);
            //Assert.IsNotNull(isInserted);
            ////읽기
            //var json = mongoService.FindMany(u => u.LoginId, "TestUser");


        }
        [TestMethod]
        public void Test_Find_User()
        {
            var mongoService = new MongoDbService();
            var dbName = "mylife";
            var colName = "Users";
            mongoService.DatabaseName = dbName;
            mongoService.CollectionName = colName;
            var query = mongoService.FindMany(u => u.LoginId, "TestUser");

        }
        [TestMethod]
        public void Test_Save_User()
        {
            //save 테스트
            var mongoService = new MongoDbService();
            var dbName = "mylife";
            var colName = "Users";
            var user = new User();
            user.LoginId = "TestUser";
            user.Name = "테스트";
            user.Password = "1234";
            mongoService.DatabaseName = dbName;
            mongoService.CollectionName = colName;
            var objId = mongoService.Insert(user);
            Assert.IsNotNull(objId);
        }
        [TestMethod]
        public void Test_Remove_User()
        {
            //삭제 테스트
            var mongoService = new MongoDbService();
            var dbName = "mylife";
            var colName = "Users";
            var user = new User();
            user.LoginId = "TestUser";
            user.Name = "테스트";
            user.Password = "1234";
            mongoService.DatabaseName = dbName;
            mongoService.CollectionName = colName;
            var objId = mongoService.Insert(user);
            Assert.IsNotNull(objId);
        }
    }
}
