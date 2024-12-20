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
    public class Question
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Question ID")]
        public int QuestionID { get; set; }
        [Display(Name = "Round")]
        public int RoundID { get; set; }
        [Display(Name = "Question name")]
        public string QuestionName { get; set; }
        [Display(Name = "Answer A")]
        public string AnswerA { get; set; }
        [Display(Name = "Answer B")]
        public string AnswerB { get; set; }
        [Display(Name = "Answer C")]
        public string AnswerC { get; set; }
        [Display(Name = "Answer D")]
        public string AnswerD { get; set; }
        [Display(Name = "Correct answer")]
        public string CorrectAnswer { get; set; }
        public string? Note { get; set; }
        [Display(Name = "Day created")]
        public DateTime DateMake { get; set; }
        [JsonIgnore]
        public virtual Round? Round { get; set; }
        [NotMapped]
        public string? SelectedAnswer { get; set; }
        [NotMapped]
        public string? RoundName{ get; set; }
    }
}
