using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBussiness
{
    public class NewsCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        public virtual ICollection<News>? News { get; set; } = new List<News>();
    }
}
