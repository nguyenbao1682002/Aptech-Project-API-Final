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
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Role ID")]
        public int RoleID { get; set; }
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Decentralization>? Decentralizations { get; set; }
    }
}
