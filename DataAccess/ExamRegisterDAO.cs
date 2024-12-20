using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ExamRegisterDAO
    {
        private readonly PetroleumBusinessDBContext db;
        public ExamRegisterDAO()
        {
            db = new PetroleumBusinessDBContext();
        }
        private static ExamRegisterDAO instance = null;
        private static readonly object Lock = new object();
        public static ExamRegisterDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new ExamRegisterDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<ExamRegister> GetExamRegister()
        {
            using var context = new PetroleumBusinessDBContext();
            var list = context.ExamRegister.ToList();
            return list;
        }
        public ExamRegister GetExamRegisterById(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var examRegister = context.ExamRegister.FirstOrDefault(e => e.ExamRegisterID == id);
            return examRegister;
        }
        public void InsertExamRegister(ExamRegister register)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.ExamRegister.Add(register);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateExamRegister(ExamRegister register)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.Entry<ExamRegister>(register).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteExamRegister(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var check = GetExamRegisterById(id);
            if (check != null)
            {
                try
                {
                    context.ExamRegister.Remove(check);
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
