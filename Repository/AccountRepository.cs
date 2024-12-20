using DataAccess;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        public void DeleteAccount(int id) => AccountDAO.Instance.DeleteAccount(id);

        public IEnumerable<Account> GetAccount() => AccountDAO.Instance.GetAccount();

        public Account GetAccountById(int id) => AccountDAO.Instance.GetAccountById(id);

        public void InsertAccount(Account account) => AccountDAO.Instance.InsertAccount(account);

        public void UpdateAccount(Account account) => AccountDAO.Instance.UpdateAccount(account);
        public IEnumerable<string> GetAllPassword() => AccountDAO.Instance.GetAllPassword();
    }
}
