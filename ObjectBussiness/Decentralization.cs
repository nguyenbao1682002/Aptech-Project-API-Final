using Microsoft.EntityFrameworkCore;
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
    public class Decentralization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DecentralizationID { get; set; }
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }
        [ForeignKey("Account")]
        [Display(Name = "Account ID")]
        public int AccountID { get; set; }
        [Display(Name = "Role grant date")]
        public DateTime RoleGrantDate { get; set; }
        [JsonIgnore]
        public virtual Account? Account { get; set; }
        [JsonIgnore]
        public virtual Role? Role { get; set; }
    }
}
