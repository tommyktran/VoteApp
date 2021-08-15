using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.IO;


namespace VoteApp
{
    public class Contest
    {
        public string name;
        public string contestCode;
        public int numberToVote;
        public List<Candidate> candidates = new List<Candidate>();
        public Contest(string newContestCode, string newname, int newNumber)
        {
            contestCode = newContestCode;
            name = newname;
            numberToVote = newNumber;
        }
        public Contest(string newContestCode, JsonElement candidateCodes)
        {
            string data = File.ReadAllText("CONTEST_" + newContestCode + ".json");
            JsonDocument doc = JsonDocument.Parse(data);

            contestCode = newContestCode;
            name = doc.RootElement.GetProperty("ContestName").GetString();
            numberToVote = doc.RootElement.GetProperty("MaxChoices").GetInt32();

            var candidatesArray = doc.RootElement.GetProperty("Candidates");
            for (int y = 0; y < doc.RootElement.GetProperty("Candidates").GetArrayLength(); y++)
            {
                for (int i = 0; i < doc.RootElement.GetProperty("Candidates").GetArrayLength(); i++)
                {
                    if (candidatesArray[i].GetProperty("CandidateCode").GetString() == candidateCodes[y].GetString())
                    {
                        string newCandidateCode = candidatesArray[i].GetProperty("CandidateCode").GetString();
                        string newCandidateName = candidatesArray[i].GetProperty("CandidateName").GetString();
                        string newCandidateParty = candidatesArray[i].GetProperty("CandidateParty").GetString();

                        candidates.Add(new Candidate(newCandidateName, newCandidateName, newCandidateParty, newCandidateCode));
                        i = doc.RootElement.GetProperty("Candidates").GetArrayLength();
                    }
                }
            }
            AddBlankWriteIns();
        }

        public void AddCandidate(Candidate candidate)
        {
            candidates.Add(candidate);
        }

        public void AddBlankWriteIns()
        {
            for (int x = numberToVote; x > 0; x--)
            {
                AddCandidate(new Candidate("", "", "WRITE-IN"));
            }
        }

        public int GetHowManySelected()
        {
            int result = 0;
            foreach (var candidate in candidates)
            {
                if (candidate.isSelected)
                {
                    result++;
                }
            }
            return result;
        }
    }
}
