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
    public class Elect
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Elect ID")]
        public int ElectID { get; set; }
        [Display(Name = "Admitted status")]
        public bool Status { get; set; } // True(False) is Passed(No passed)
        [JsonIgnore]
        public virtual ICollection<ResultCandidate>? ResultCandidate { get; set; }
    }
}
