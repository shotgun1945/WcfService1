using System;
using System.Collections.Generic;
using WcfService1;

namespace ConsoleApplication1
{
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            //DB 관리 프로그램
            while (true)
            {
                Console.WriteLine("1. Database 모두 출력");
                Console.WriteLine("2. Collection 모두 출력");
                Console.WriteLine("3. MyLife초기 데이터 생성 \n");
                //Console.WriteLine(". Collection 모두 출력\n");
                Console.WriteLine("q:종료");
                Console.Write("명령을 입력하세요:");
                var input = Console.ReadLine();
                Console.WriteLine();
                if(SwitchInput(input)) return;
                PrintLastLine();
            }
        }

        private static bool SwitchInput(string input)
        {
            var service = new MongoDbService();
            switch (input)
            {
                case "1":
                    PrintDatabase(service);
                    break;
                case "2":
                    SelectDatabase(service);
                    break;
                case "3":
                    InitMyLifeDB();
                    break;
                case "q":
                    return true;
            }
            return false;
        }

        private static void InitMyLifeDB()
        {
            
        }

        private static void SelectDatabase(MongoDbService service)
        {
            
            var dbName = string.Empty;
            //똑바로 입력할때까지 while문
            //수정해야될듯..
            while(true)
            {
                
                int input;
                var database = PrintDatabase(service);
                
                Console.Write("Database 선택: ");
                var userInput = Console.ReadLine();
                if (int.TryParse(userInput, out input))
                {
                    input -= 1;
                    if (database.Count() > input && input >= 0)
                    {
                        dbName = database[input];
                        break;
                    }
                }
                else if (userInput.ToLower() =="q")
                {
                    return;
                }
            }

            
            PrintCollection(service, dbName);
        }

        private static List<string> PrintCollection(MongoDbService service, string dbName)
        {
            Console.WriteLine("Collection 모두 출력");
            service.DatabaseName = dbName;
            var collectionName = service.GetCollectionNames().ToList();
            PrintLine(collectionName);
            return collectionName;
        }

        private static List<string> PrintDatabase(MongoDbService service)
        {
            Console.WriteLine("Database 모두 출력");
            var databases = service.GetDatabaseNames().ToList();
            PrintLine(databases);
            return databases;
        }
        private static void PrintLastLine()
        {
            Console.Write("Prees Any Key To Continue.....");
            Console.ReadKey();
            Console.Clear();
        }
        private static void PrintLine(IEnumerable<string> strings)
        {
            var count = 1;
            foreach (var s in strings)
            {
                Console.WriteLine("{0} {1}", count++, s);
            }
        }
    }
}
