using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ExamDAO
    {
        private readonly PetroleumBusinessDBContext db;
        public ExamDAO()
        {
            db = new PetroleumBusinessDBContext();
        }
        private static ExamDAO instance = null;
        private static readonly object Lock = new object();
        public static ExamDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new ExamDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Exam> GetExams()
        {
            using var context = new PetroleumBusinessDBContext();
            var list = context.Exams.ToList();
            return list;
        }
        public IEnumerable<Exam> SearchOrSortByExam(string SearchString, string? sortBy)
        {
            using var context = new PetroleumBusinessDBContext();
            var listModel = context.Exams.ToList();
            if (SearchString != null)
            {
                try
                {
                    listModel = listModel.Where(x => x.ExamName.ToLower().Contains(SearchString.ToLower())).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return listModel;
        }
        public Exam GetRoom(int room)
        {
            var context = new PetroleumBusinessDBContext();
            List<Exam> listExams = context.Exams.ToList();
            var checkRoom = GetExamById(room);
            return checkRoom;

        }
        public IEnumerable<Exam> GetAllExamEnd()
        {
            using var context = new PetroleumBusinessDBContext();
            var ListItem = context.Exams.Where(l => l.Status == "End").ToList();
            return ListItem;
        }
        public void DeleteExamEnd()
        {
            using var context = new PetroleumBusinessDBContext();
            var checkExam = GetAllExamEnd();
            if (checkExam != null)
            {
                try
                {
                    context.Exams.RemoveRange(checkExam);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public Exam GetExamById(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var exam = context.Exams.FirstOrDefault(e => e.ExamID == id);
            return exam;
        }
        public void InsertExam(Exam exam)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.Exams.Add(exam);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateExam(Exam exam)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.Entry<Exam>(exam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteExam(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var checkExam = GetExamById(id);
            if (checkExam != null)
            {
                try
                {
                    context.Exams.Remove(checkExam);
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
