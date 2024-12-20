using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ObjectBussiness
{
    public class News
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewsID { get; set; }
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [AllowHtml]
        public string? Contents { get; set; }
        public string? ShortContents { get; set; }
        public string? Picture { get; set; }
        public DateTime DateSubmitted { get; set; }
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }
        [NotMapped]
        public string? AccountName { get; set; }
        [JsonIgnore]
        public virtual Account? Account { get; set; }
        [JsonIgnore]
        public virtual NewsCategory? Category { get; set; }
    }
}
