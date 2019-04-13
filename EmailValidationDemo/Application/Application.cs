using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAutorizationDemo.Application
{  
    public class Application
    {
        public static Application self;

        public User NewUser { get; set; } = new User();
        public List<User> Users { get; set; } = new List<User>();

        private Application()
        {
            new ApplicationManager(NewUser);
            Users.Add(new User() { Email = "user@mail.com", Password = "password123" });
        }

        public static Application Instance()
        {
            if(self == null)
            {
                self = new Application(); 
            }

            return self;
        }

    }
}
