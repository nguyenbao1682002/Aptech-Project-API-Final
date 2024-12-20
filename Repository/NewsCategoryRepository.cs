using DataAccess;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NewsCategoryRepository: INewsCategoryRepository
    {
        public void InsertNewsCategory(NewsCategory n) => NewsCategoryDAO.InsertNewsCategory(n);
        public void EditNewsCategory(NewsCategory n) => NewsCategoryDAO.EditNewsCategory(n);
        public void DeleteNewsCategory(NewsCategory n) => NewsCategoryDAO.DeleteNewsCategory(n);
        public NewsCategory GetNewsCategoryById(int id) => NewsCategoryDAO.GetNewsCategoryById(id);
        public List<NewsCategory> GetNewsCategoriesList() => NewsCategoryDAO.GetNewsCategoriesList();
    }
}
