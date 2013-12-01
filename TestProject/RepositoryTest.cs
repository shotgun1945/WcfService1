using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MyLifeService;
using ShareEntity.Entity;

namespace TestProject
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Login()
        {

        }

        [TestMethod]
        public void JsonTest()
        {
            var result = new Communicator().JsonTest("TestUser", "1234");
            Debug.Print(result);
            //var buf = string.Empty;
            //var buffor = new JsonBuffer(buf);
            //var reader = new Bsons(buffor, new JsonReaderSettings());

            var user = BsonSerializer.Deserialize<User>(result);

            Assert.AreEqual(user.LoginId, "TestUser");
        }
        [TestMethod]
        public void JsonListTest()
        {
            var query =  new Repository().GetUserList();
            var result = query.ToJson();
            var users = BsonSerializer.Deserialize<List<User>>(result);

            Debug.Write(result);
        }
    }
}
