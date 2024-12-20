using DataAccess;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NewsRepository: INewsRepository
    {
        public void InsertNews(News n) => NewsDAO.InsertNews(n);
        public void EditNews(News n) => NewsDAO.EditNews(n);
        public void DeleteNews(News n) => NewsDAO.DeleteNews(n);
        public News GetNewsById(int id) => NewsDAO.GetNewsById(id);
        public List<NewsCategory> GetNewsCategories() => NewsCategoryDAO.GetNewsCategoriesList();
        public List<News> GetNewsList() => NewsDAO.GetNewsList();
    }
}
