using ElPlatform.DAL.Infrastructure;
using ElPlatform.DAL.Repositories;
using System;
using System.Linq;

namespace ElPlatform.FuncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var ufw = new UnitOfWork();
            var userRepo = new ApplicationUserRepository(ufw);
            var users = userRepo.GetAll().ToList();
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    if (user != null)
                    {
                        Console.WriteLine(user.Email);
                    }
                }
            }
            else
            {
                Console.WriteLine("no Users found");
            }
            Console.ReadLine();

        }
    }
}
