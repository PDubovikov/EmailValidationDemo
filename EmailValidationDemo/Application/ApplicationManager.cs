using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace UserAutorizationDemo.Application
{
    public class ApplicationManager
    {
        List<string> errorList = new List<string>();

        bool MailComplite { get; set; }
        bool PasswordComplite { get; set; }

        bool MailValid { get; set; }
        bool PasswordValid { get; set; }

        public ApplicationManager(User user)
        {
            user.PropertyChanged += (s, e) => { Autorized(user); };

        }

        private void Autorized(User user)
        {
            Validation(user);

        }

        private void Validation(User user)
        {
            Type type = typeof(User);
            var properties = type.GetProperties()
                                 .Where(p => Attribute.IsDefined(p, typeof(RegexPatternAttribute)));


            foreach (var prop in properties)
            {

                RegexPatternAttribute regexAttrib = (RegexPatternAttribute)prop.GetCustomAttribute(typeof(RegexPatternAttribute), false);
                
                string value = prop.GetValue(user)?.ToString();

                if (value != null)
                {
                    if(prop.Name.Equals("Email"))
                        MailValid = Regex.IsMatch(value, regexAttrib.Pattern);

                    if(prop.Name.Equals("Password") )
                        PasswordValid = Regex.IsMatch(value, regexAttrib.Pattern);

                    Console.CursorLeft = 0;
                    for (int i = value.Length; i > 0; i--)
                    {
                        Console.Write(" ");
                    }
                    Console.CursorLeft = 0;

                    Console.ForegroundColor = ConsoleColor.Red;
                    if (MailValid && !MailComplite) { Console.ForegroundColor = ConsoleColor.Green; }
                    if (PasswordValid && !PasswordComplite) { Console.ForegroundColor = ConsoleColor.Green; }

                    if (prop.Name.Equals("Email") && !MailComplite)
                        Console.Write(value);

                    if (prop.Name.Equals("Password") && !PasswordComplite)
                        Console.Write(value);

                    errorList.Add(regexAttrib.Error);

                    if (Application.Instance().Users.Select(u => u.Email.Equals(value)).FirstOrDefault())
                    {
                        if (prop.Name.Equals("Email") && !MailComplite)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("\n" + "Email is found");
                        }

                        MailComplite = true;
                    }

                    if (Application.Instance().Users.Select(u => u.Password.Equals(value)).FirstOrDefault())
                    {
                        if (prop.Name.Equals("Password") && !PasswordComplite)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("\n" + "User is authorized");
                        }
                            PasswordComplite = true;
                    }

                }

            }

        }


    }
}
