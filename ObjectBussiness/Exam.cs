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
    public class Exam
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Exam ID")]
        public int ExamID { get; set; }
        [Display(Name = "Exam name")]
        public string ExamName { get; set; }
        [Display(Name = "Date create test")]
        public DateTime DateCreateTest { get; set; }
        [Display(Name = "Time begin")]
        public DateTime TimeBegin { get; set; }
        [Display(Name = "Time end")]
        public DateTime TimeEnd { get; set; }
        [Display(Name = "Time delay")]
        public DateTime? TimeDelay { get; set; }
        public string? Status { get; set; } // Start or End
        [JsonIgnore]
        public virtual ICollection<ResultCandidate>? ResultCandidate { get; set; }
        [JsonIgnore]
        public virtual ICollection<Account>? Account { get; set; }
        [JsonIgnore]
        public virtual ICollection<Round>? Round { get; set; }
    }
}
