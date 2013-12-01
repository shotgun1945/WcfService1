using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MyLifeService.Helper;
using ShareEntity.Entity;

namespace MyLifeService
{
    public class Repository
    {
        private MongoHelper mongoHelper;
        public Repository()
        {
            mongoHelper = new MongoHelper("mongodb://localhost", "mylife");

        }

        public void Login(string userId, string userPw)
        {
            string errorCode;
            var user = ValidUser(userId, userPw, out errorCode);
            if (user != null)
            {
                
            }
            
        }
        /// <summary>
        /// 사용자 검증
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userPw"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public User ValidUser(string userId, string userPw, out string errorMessage)
        {
            User user = null;
            errorMessage = "";
            mongoHelper.CollectionName = "Users";
            var findUserCursor = mongoHelper.FindObject<User, string>(u => u.LoginId, userId);
            if (findUserCursor.MoveNext())
            {
                User findUser = findUserCursor.Current;

                if (ValidUserPw(findUser.Password, userPw))
                {
                    user = findUser;
                }
                else
                {
                    errorMessage = "password";
                }
            }
            else
            {
                errorMessage = "loginid";
            }
            
            return user;
        }


        public IQueryable<User> GetUserList()
        {
            mongoHelper.CollectionName = "Users";
            var collection =  mongoHelper.MongoDatabase.GetCollection("Users");
            return (from u in collection.AsQueryable<User>() select u);
        }
        //TODO 암호화 체크 넣어야될듯.
        private bool ValidUserPw(string dbPw, string inputPw)
        {
            return dbPw == inputPw;
        }
    }
}
