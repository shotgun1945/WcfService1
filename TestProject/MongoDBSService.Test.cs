using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    using ShareEntity.Entity;

    using WcfService1;

    [TestClass]
    public class MongoDBSServiceTest
    {
        [TestMethod]
        public void Test_CreateDatabase()
        {
            var mongoService = new MongoDbService();
            if(mongoService.CreateDatabase("Users",typeof(User)))
                Assert.Fail();
            var cols = mongoService.GetCollectionNames();
            
        }
        [TestMethod]
        public void Test_CreateCollection()
        {
            var mongoService = new MongoDbService();
            //mongoService.DatabaseName
            //mongoService.Find
        }
    }
}
