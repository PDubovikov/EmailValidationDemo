using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UserAutorizationDemo
{
    class Program
    {
        private static void Input(Application.User user, string flag)
        {
            Type type = user.GetType();
            var property = type.GetProperty(flag);

            Console.ResetColor();
            string exit = "";
                Console.WriteLine("Enter " + flag);


            while (!exit.Equals("\r"))
            {
                string keypress = Console.ReadKey(true).KeyChar.ToString();

                switch (keypress)
                {
                    case "\b":
                        if (Console.CursorLeft > 0)
                        {
                            Console.CursorLeft = Console.CursorLeft - 1;
                            Console.Write(" ");

                            property.SetValue(user,property.GetValue(user).ToString().Substring(0, property.GetValue(user).ToString().Length - 1)); 
                        }
                        break;
                    case "\r":
                        Console.WriteLine();
                        exit = "\r";
                        break;
                    default:
                           property.SetValue(user, property.GetValue(user)?.ToString() + keypress);
                        break;
                }

            }

        }

        static void Main(string[] args)
        {
            Application.User user = Application.Application.Instance().NewUser;

            Input(user, "Email");
            Input(user, "Password");

            Console.ReadKey();
        }
    }
}
