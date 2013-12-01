using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using ShareEntity.Entity;

namespace MyLifeService
{
    public class Communicator
    {
        private readonly Repository _repository = new Repository();
        public string JsonTest(string loginId, string pw)
        {
            
            string errorMessage;
            var user = _repository.ValidUser(loginId,pw,out errorMessage);
            var result = user.ToJson();
            return result;
        }


        public string JsonTestList()
        {
            var query = _repository.GetUserList();
            return query.ToJson();
        }
    }
}
