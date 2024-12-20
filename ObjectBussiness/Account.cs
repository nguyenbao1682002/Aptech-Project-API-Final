using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ObjectBussiness
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }
        [Display(Name = "Exam ID")]
        public int ExamID { get; set; }
        [ForeignKey("ExamRegister")]
        [Display(Name = "Exam Register ID")]
        public int ExamRegisterID { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Decentralization? Decentralization { get; set; }
        [JsonIgnore]
        public virtual ICollection<News>? News { get; set; }
        [JsonIgnore]
        public virtual Exam? Exam { get; set; }
        [JsonIgnore]
        public virtual ExamRegister? ExamRegister { get; set; }
        [NotMapped]
        public string? UserName { get; set; }
    }
}
