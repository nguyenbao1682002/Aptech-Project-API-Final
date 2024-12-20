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
    public class History
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HistoryID { get; set; }
        public DateTime DateCreated{ get; set; }
        [JsonIgnore]
        public virtual ICollection<ResultCandidate>? ResultCandidates { get; set; }
    }
}
