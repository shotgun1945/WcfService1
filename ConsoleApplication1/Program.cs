using System;
using WcfService1;

namespace ConsoleApplication1
{

    class Program
    {
        static void Main(string[] args)
        {
            //MongoDb();
            var servce = new MongoDbService();
            Console.WriteLine("Plesase Enter Your Name");
            var message = servce.SampleMethod(Console.ReadLine());

            Console.WriteLine(message);
            Console.ReadLine();
            
        }
    }
}
