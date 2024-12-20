using DataAccess;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ExamRepository : IExamRepository
    {
        public void DeleteExam(int id) => ExamDAO.Instance.DeleteExam(id);

        public Exam GetExamById(int id) => ExamDAO.Instance.GetExamById(id);

        public IEnumerable<Exam> GetExams() => ExamDAO.Instance.GetExams();

        public Exam GetRoom(int room) => ExamDAO.Instance.GetRoom(room);

        public void InsertExam(Exam exam) => ExamDAO.Instance.InsertExam(exam);

        public void UpdateExam(Exam exam) => ExamDAO.Instance.UpdateExam(exam);
        public IEnumerable<Exam> GetAllExamEnd() => ExamDAO.Instance.GetAllExamEnd();
        public void DeleteExamEnd() => ExamDAO.Instance.DeleteExamEnd();
        public IEnumerable<Exam> SearchOrSortByExam(string SearchString, string sortBy) => ExamDAO.Instance.SearchOrSortByExam(SearchString, sortBy);
    }
}
