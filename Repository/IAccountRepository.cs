using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepository
    {
        public void InsertAccount(Account account);
        public void UpdateAccount(Account account);
        public void DeleteAccount(int id);
        public Account GetAccountById(int id);
        public IEnumerable<Account> GetAccount();
        public IEnumerable<string> GetAllPassword();
    }
}
