using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LoginDAO
    {
        private readonly PetroleumBusinessDBContext db;
        public LoginDAO()
        {
            db = new PetroleumBusinessDBContext();
        }
        private static LoginDAO instance = null;
        private static readonly object Lock = new object();
        public static LoginDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new LoginDAO();
                    }
                    return instance;
                }
            }
        }
        public bool SignIn(string username, string password)
        {
            var check = from a in db.Accounts
                        join b in db.ExamRegister
                        on a.ExamRegisterID equals b.ExamRegisterID
                        where a.Password == password && b.Email == username
                        select new Account
                        {
                            UserName = b.Email,
                            Password = a.Password,
                            
                        };
            if (check.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
