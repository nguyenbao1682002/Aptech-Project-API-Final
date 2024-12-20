using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;

namespace ObjectBussiness
{
    public class NewsImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsID { get; set; }
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [AllowHtml]
        public string? Contents { get; set; }
        [Display(Name = "Short contents")]
        public string? ShortContents { get; set; }
        [Display(Name = "Date Publish")]
        public DateTime DateSubmitted { get; set; }
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }
        [Display(Name = "Upload image")]
        public IFormFile ImageFile { get; set; }
    }
}
