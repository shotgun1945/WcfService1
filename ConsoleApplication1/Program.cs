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
                //Console.WriteLine("3. Collection 모두 출력\n");
                //Console.WriteLine(". Collection 모두 출력\n");
                Console.WriteLine("q:종료");
                Console.Write("명령을 입력하세요:");
                var input = Console.ReadLine();
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
                case "q":
                    return true;
            }
            return false;
        }

        private static void SelectDatabase(MongoDbService service)
        {
            var input = 0;
            var dbName = string.Empty;
            do
            {
                input -= 1;
                var database = PrintDatabase(service);
                if (database.Count()<input ||input <=0)
                {
                    dbName = database[input];
                    break;
                }
                Console.Write("Database 선택: ");
            }
            while (!int.TryParse(Console.ReadLine(), out input));

            
            PrintCollection(service, dbName);
        }

        private static List<string> PrintCollection(MongoDbService service, string dbName)
        {
            Console.WriteLine("Collection 모두 출력");
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
