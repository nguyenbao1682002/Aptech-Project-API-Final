using Microsoft.EntityFrameworkCore;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class NewsCategoryDAO
    {
        private static NewsCategoryDAO instance = null;
        public static readonly object instanceLock = new object();
        public static NewsCategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new NewsCategoryDAO();
                    }
                    return instance;
                }
            }
        }

        //Get News Category
        public static List<NewsCategory> GetNewsCategoriesList()
        {
            var list = new List<NewsCategory>();
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    list = context.NewsCategories.ToList();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
            return list;
        }

        public static NewsCategory GetNewsCategoryById(int id)
        {
            NewsCategory n = new NewsCategory();
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    n = context.NewsCategories.FirstOrDefault(x => x.CategoryID == id);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return n;
        }

        public static void InsertNewsCategory(NewsCategory n)
        {
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    context.NewsCategories.Add(n);
                    context.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void EditNewsCategory(NewsCategory n)
        {
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    context.Entry<NewsCategory>(n).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteNewsCategory(NewsCategory n)
        {
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    var _n = context.NewsCategories.SingleOrDefault(x => x.CategoryID == n.CategoryID);
                    context.NewsCategories.Remove(_n);
                    context.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
