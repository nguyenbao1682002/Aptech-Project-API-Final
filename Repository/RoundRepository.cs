using DataAccess;
using ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoundRepository : IRoundRepository
    {
        public IEnumerable<Round> GetRounds() => RoundDAO.Instance.GetRounds();
        public Round GetRoundById(int id) => RoundDAO.Instance.GetRoundById(id);

        public void InsertRound(Round round) => RoundDAO.Instance.InsertRound(round);

        public void UpdateRound(Round round) => RoundDAO.Instance.UpdateRound(round);

        public void DeleteRound(int id) => RoundDAO.Instance.DeleteRound(id);

        public IEnumerable<Round> GetRoundId() => RoundDAO.Instance.GetRoundId();
    }
}
