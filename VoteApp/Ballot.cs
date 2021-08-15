using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;


namespace VoteApp
{
    public class Ballot
    {
        public string name;
        public List<Contest> contests = new List<Contest>();
        public List<string> candidateCodes = new List<string>();
        public int currentContestIndex;
        public int currentCandidateIndex;

        public Ballot(string newBallotCode)
        {
            string data = File.ReadAllText("BALLOT_" + newBallotCode + ".json");

            JsonDocument doc = JsonDocument.Parse(data);
            name = newBallotCode;
            var thing = doc.RootElement.GetProperty("Contests");
            for (int i = 0; i < thing.GetArrayLength(); i++)
            {
                contests.Add(new Contest(thing[i].GetProperty("ContestCode").GetString(),
                    thing[i].GetProperty("CandidateCodes")));
            }
        }

        public void AddContest(Contest contest)
        {
            contests.Add(contest);
        }

        public void Output()
        {
            foreach (Contest contest in contests)
            {
                Console.WriteLine("Contest " + (contests.IndexOf(contest) + 1) + " of " + contests.Count + ": " + contest.name);
                foreach (Candidate candidate in contest.candidates)
                {
                    Console.Write("     ");
                    if (candidate.party == "WRITE-IN")
                    {
                        Console.Write("Write-in: " + candidate.fullName);
                    }
                    else
                    {
                        Console.Write(candidate.fullName + " (" + candidate.party + ")");
                    }

                    if (candidate.isSelected)
                    {
                        Console.WriteLine(" (Selected)");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
