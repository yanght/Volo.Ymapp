using Bogus;
using Serilog;
using Serilog.Core;
using ServiceStack.Text;
using System;
using System.Threading;

namespace Volo.Ymapp.Redis
{
    class Program
    {
        private static Logger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        private static RedisCacheService redis = new RedisCacheService("129.28.157.213:6379,defaultDatabase=0,password=1qaz2wsx!@#", log);
        static void Main(string[] args)
        {

            redis.Prefix = "ymapp_";

            for (int i = 1; i <= 1; i++)
            {
                Thread t = new Thread(TestMethod);
                t.Start();
            }

            Console.WriteLine("done..");
            Console.ReadKey();
        }

        static int index = 0;
        private static void TestMethod()
        {
            for (var i = 0; i < 1000; i++)
            {
                var person = new Faker<Person>()
               .CustomInstantiator(f => new Person())
               .RuleFor(u => u.Id, f => i)
               .RuleFor(u => u.Age, f => f.Random.Number(1, 100))
               .RuleFor(u => u.Sex, f => (short)f.PickRandom<Gender>())
               .RuleFor(u => u.Name, f => f.Internet.UserName()).Generate();
                redis.Set($"person_{index}", person, DateTime.Now.AddMinutes(10));
                log.Information($"person_{index}");
                index++;
            }
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string IDCard { get; set; }
        public short Sex { get; set; }
    }
}
