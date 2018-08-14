using OnlineTranzaktionCustomer;
using OnlineTranzaktionCustomer.Modul;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTranzaktionOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service(@"C:\Users\Клюкинк\Documents\Visual Studio 2015\Projects\OnlineTranzaktionOperator\OnlineTranzaktionOperator\bin\Debug");
                                 
            try
            {
                User user = new User();
                user.email = "klyukin@gmail.com";
                user.Login = "kostya";
                user.Password = "21313";

                service.CreateUser(user);
                Console.WriteLine("Create!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
