using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ObjectBussiness
{
    public class ResultCandidate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Result candidate ID")]
        public int ResultCandidateID { get; set; }
        [Display(Name = "Exam ID")]
        public int ExamID { get; set; }
        [Display(Name = "Elect ID")]
        public int ElectID { get; set; }
        [JsonIgnore]
        public virtual Exam? Exam { get; set; }
        [JsonIgnore]
        public virtual Elect? Elect { get; set; }
    }
}
