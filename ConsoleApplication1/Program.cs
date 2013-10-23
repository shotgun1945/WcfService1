using System;
using System.Collections.Generic;
using WcfService1;

namespace ConsoleApplication1
{

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
                    SelectDatabase();
                    break;
                case "q":
                    return true;
            }
            return false;
        }

        private static void SelectDatabase()
        {
            
        }

        private static void PrintDatabase(MongoDbService service)
        {
            PrintLine("Database 모두 출력");
            PrintLine(service.GetDatabaseNames());
        }

        private static void PrintLine(IEnumerable<string> strings)
        {
            var count = 1;
            foreach (var s in strings)
            {
                PrintLine(string.Format("{0} {1}", count++, s));
            }
        }

        private static void PrintLine(string s)
        {
            Console.WriteLine("{0}",s);
        }
    }
}
