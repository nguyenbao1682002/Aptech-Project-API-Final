using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRoundRepository
    {
        public IEnumerable<Round> GetRounds();
        public Round GetRoundById(int id);
        public void InsertRound(Round round);
        public void UpdateRound(Round round);
        public void DeleteRound(int id);
        IEnumerable<Round> GetRoundId();
    }
}
