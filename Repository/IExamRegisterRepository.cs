using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IExamRegisterRepository
    {
        public IEnumerable<ExamRegister> GetExamRegister();
        public ExamRegister GetExamRegisterById(int id);
        public void InsertExamRegister(ExamRegister register);
        public void UpdateExamRegister(ExamRegister register);
        public void DeleteExamRegister(int id);
    }
}
