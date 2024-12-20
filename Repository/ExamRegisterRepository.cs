using DataAccess;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ExamRegisterRepository : IExamRegisterRepository
    {

        public IEnumerable<ExamRegister> GetExamRegister() => ExamRegisterDAO.Instance.GetExamRegister();

        public ExamRegister GetExamRegisterById(int id) => ExamRegisterDAO.Instance.GetExamRegisterById(id);

        public void InsertExamRegister(ExamRegister register) => ExamRegisterDAO.Instance.InsertExamRegister(register);

        public void UpdateExamRegister(ExamRegister register) => ExamRegisterDAO.Instance.UpdateExamRegister(register);
        public void DeleteExamRegister(int id) => ExamRegisterDAO.Instance.DeleteExamRegister(id);
    }
}
