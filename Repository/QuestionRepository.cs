using DataAccess;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public void DeleteQuestion(int id) => QuestionDAO.Instance.DeleteQuestion(id);

        public Question GetQuestionById(int id) => QuestionDAO.Instance.GetQuestionById(id);

        public IEnumerable<Question> GetQuestions() => QuestionDAO.Instance.GetQuestions();

        public IEnumerable<Question> GetQuestionsByRound(int id) => QuestionDAO.Instance.GetQuestionsByRound(id);

        public void InsertQuestion(Question question) => QuestionDAO.Instance.InsertQuestion(question);

        public IEnumerable<Question> SearchByNameOrSortBy(string name, string sortBy) => QuestionDAO.Instance.SearchByNameOrSortBy(name, sortBy);

        public void UpdateQuestion(Question question) => QuestionDAO.Instance.UpdateQuestion(question);
    }
}
