using ObjectBussiness;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace WebMVC.ModelsView
{
    public class NewsPageMV
    {
        public string? Title { get; set; }
        public string? ShortContents { get; set; }
        public string? Picture { get; set; }
        public DateTime DateSubmitted { get; set; }
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }
        public IPagedList<News>? News { get; set; }
    }
}
