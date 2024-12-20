using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDAO
    {
        private readonly PetroleumBusinessDBContext db;
        public AccountDAO()
        {
            db = new PetroleumBusinessDBContext();
        }
        private static AccountDAO instance = null;
        private static readonly object Lock = new object();
        public static AccountDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Account> GetAccount()
        {
            using var context = new PetroleumBusinessDBContext();
            var list = context.Accounts.ToList();
            return list;
        }
        public IEnumerable<string> GetAllPassword()
        {
            using var context = new PetroleumBusinessDBContext();
            var listPassword = context.Accounts.Select(x => x.Password).ToList();
            return listPassword;
        }
        public Account GetAccountById(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var account = context.Accounts.FirstOrDefault(e => e.AccountID == id);
            return account;
        }
        public void InsertAccount(Account account)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateAccount(Account account)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.Entry<Account>(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteAccount(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var checkExam = GetAccountById(id);
            if (checkExam != null)
            {
                try
                {
                    context.Accounts.Remove(checkExam);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
