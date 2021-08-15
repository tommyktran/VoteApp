using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApp
{
    public class Candidate
    {
        public string shortName;
        public string fullName;
        public string party;
        public string candidateCode;
        public bool isSelected;
        public Candidate(string newSName, string newFName, string newParty)
        {
            shortName = newSName;
            fullName = newFName;
            party = newParty;
        }
        public Candidate(string newSName, string newFName, string newParty, string newCandidateCode)
        {
            shortName = newSName;
            fullName = newFName;
            party = newParty;
            candidateCode = newCandidateCode;
        }
    }
}
