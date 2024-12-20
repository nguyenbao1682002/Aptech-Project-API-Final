using Microsoft.EntityFrameworkCore;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class NewsDAO
    {
        private static NewsDAO instance = null;
        public static  readonly object instanceLock = new object();
        public static NewsDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new NewsDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<News> GetNewsList()
        {
            using (var context = new PetroleumBusinessDBContext())
            {
                var list = from a in context.News
                           join b in context.NewsCategories on a.CategoryID equals b.CategoryID
                           join c in context.Accounts on a.AccountID equals c.AccountID
                           select new News
                           {
                               NewsID = a.NewsID,
                               Title = a.Title,
                               Contents = a.Contents,
                               ShortContents = a.ShortContents,
                               Picture = a.Picture,
                               DateSubmitted = a.DateSubmitted,
                               AccountID = a.AccountID,
                               CategoryID = a.CategoryID,
                               CategoryName = b.CategoryName,
                               AccountName = c.Password
                           };

                return list.ToList();
            }
        }
        public static News GetNewsById(int id)
        {
            News n = new News();
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    n = context.News.FirstOrDefault(x => x.NewsID == id);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return n;
        }

        public static void InsertNews(News n)
        {
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    context.News.Add(n);
                    context.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void EditNews(News n)
        {
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    context.Entry<News>(n).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteNews(News n)
        {
            try
            {
                using (var context = new PetroleumBusinessDBContext())
                {
                    var _n = context.News.SingleOrDefault(x => x.NewsID == n.NewsID);
                    context.News.Remove(_n);
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
