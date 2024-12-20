using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LoginRepository : ILoginRepository
    {
        public bool SignIn(string username, string password) => LoginDAO.Instance.SignIn(username, password);
    }
}
