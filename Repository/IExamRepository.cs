using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IExamRepository
    {
        public IEnumerable<Exam> GetExams();
        public IEnumerable<Exam> SearchOrSortByExam(string SearchString, string sortBy);
        public Exam GetExamById(int id);
        public void InsertExam(Exam exam);
        public void UpdateExam(Exam exam);
        public void DeleteExam(int id);
        public Exam GetRoom(int room);
        public IEnumerable<Exam> GetAllExamEnd();
        public void DeleteExamEnd();
    }
}
