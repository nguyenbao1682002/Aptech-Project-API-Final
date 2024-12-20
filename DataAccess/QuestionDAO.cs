using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class QuestionDAO
    {
        private readonly PetroleumBusinessDBContext db;
        private static QuestionDAO instance = null;
        private static readonly object Lock = new object();
        public static QuestionDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new QuestionDAO();
                    }
                    return instance;
                }
            }
        }
        public QuestionDAO()
        {
            db = new PetroleumBusinessDBContext();
        }
        public IEnumerable<Question> GetQuestionsByRound(int id)
        {
            var question = from a in db.Questions
                           join b in db.Rounds
                           on a.RoundID equals b.RoundID
                           join c in db.Exams
                           on b.ExamID equals c.ExamID
                           where c.ExamID == id
                           select new Question
                           {
                               QuestionID = a.QuestionID,
                               QuestionName = a.QuestionName,
                               AnswerA = a.AnswerA,
                               AnswerB = a.AnswerB,
                               AnswerC = a.AnswerC,
                               AnswerD = a.AnswerD,
                               CorrectAnswer = a.CorrectAnswer,
                               DateMake = a.DateMake,
                               Note = a.Note,
                           };
            return question;
        }
        public IEnumerable<Question> GetQuestions()
        {
            var question = from a in db.Questions
                           join b in db.Rounds
                           on a.RoundID equals b.RoundID
                           select new Question
                           {
                               QuestionID = a.QuestionID,
                               QuestionName = a.QuestionName,
                               AnswerA = a.AnswerA,
                               AnswerB = a.AnswerB,
                               AnswerC = a.AnswerC,
                               AnswerD = a.AnswerD,
                               CorrectAnswer = a.CorrectAnswer,
                               DateMake = a.DateMake,
                               Note = a.Note,
                               RoundName = b.RoundName
                           };
            return question;
        }
        public IEnumerable<Question> SearchByNameOrSortBy(string name, string sortBy)
        {
            try
            {
                using var context = new PetroleumBusinessDBContext();
                var listModel = context.Questions.Where(q => q.QuestionName.ToLower().Contains(name.ToLower())).ToList();
                return listModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Question GetQuestionById(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var check = context.Questions.SingleOrDefault(q => q.QuestionID == id);
            if (check != null)
            {
                return check;
            }
            else
            {
                throw new ArgumentException("Question not found.");
            }
        }
        public void InsertQuestion(Question question)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.Questions.Add(question);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateQuestion(Question question)
        {
            using var context = new PetroleumBusinessDBContext();
            try
            {
                context.Entry<Question>(question).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteQuestion(int id)
        {
            using var context = new PetroleumBusinessDBContext();
            var check = GetQuestionById(id);
            if (check != null)
            {
                try
                {
                    context.Questions.Remove(check);
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
