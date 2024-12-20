using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface INewsCategoryRepository
    {
        void InsertNewsCategory(NewsCategory n);
        void EditNewsCategory(NewsCategory n);
        void DeleteNewsCategory(NewsCategory n);
        NewsCategory GetNewsCategoryById(int id);
        List<NewsCategory> GetNewsCategoriesList();
    }
}
