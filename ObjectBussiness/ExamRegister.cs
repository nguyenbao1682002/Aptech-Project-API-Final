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
    public class ExamRegister
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Exam Register ID")]
        public int ExamRegisterID { get; set; }
        [Display(Name = "Candidate name")]
        public string CandidateName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; } //True(False) is Male(Female)
        [Display(Name = "Birth Day")]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Phone number")]
        [RegularExpression("^0[1-9]\\d\\d{3}\\d{4}$")]
        public string Phone { get; set; }
        [RegularExpression("\\w+@+\\w+\\.+\\w+\\w")]
        public string Email { get; set; }
        [Display(Name = "Place of birth")]
        public string PlaceOfBirth { get; set; }
        [Display(Name = "Residential address")]
        public string ResidentialAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [JsonIgnore]
        public virtual Account? Account { get; set; }
        [NotMapped]
        [Display(Name = "Exam name")]
        public int ExamID { get; set; }
    }
}
