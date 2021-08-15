using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace VoteApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ballot = new Ballot("0002");
            ballot.Output();
            Vote(ballot);
        }

        static void Vote(Ballot ballot)
        {
            Contest currentContest = ballot.contests[ballot.currentContestIndex];
            Candidate currentCandidate = ballot.contests[ballot.currentContestIndex].candidates[ballot.currentCandidateIndex];
            bool isDone = false;

            Console.Write(currentContest.name + " --- " + currentCandidate.fullName);
            if (currentCandidate.party == "WRITE-IN")
            {
                Console.Write(" (WRITE-IN)");
            }
            if (currentCandidate.isSelected)
            {
                Console.WriteLine(" (Selected)");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write("Press a key --  ");
            List<int> options = new List<int>();

            options.Add(0);
            Console.Write("0: Display Ballot  ");

            if (ballot.currentContestIndex != 0)
            {
                options.Add(2);
                Console.Write("2: Prev Contest  ");
            }
            if (ballot.currentCandidateIndex != 0)
            {
                options.Add(4);
                Console.Write("4: Prev Candidate  ");
            }
            if (currentCandidate.isSelected)
            {
                options.Add(5);
                Console.Write("5: Deselect  ");
            }
            else
            {
                options.Add(5);
                Console.Write("5: Select  ");
            }

            if (ballot.currentCandidateIndex != currentContest.candidates.Count - 1)
            {
                options.Add(6);
                Console.Write("6: Next Candidate  ");
            }

            if (ballot.currentContestIndex != ballot.contests.Count - 1)
            {
                options.Add(8);
                Console.Write("8: Next Contest  ");
            }
            else
            {
                isDone = true;
                options.Add(8);
                Console.Write("8: Done  ");
            }
            Console.WriteLine();
            int input = ReadKey();
            Console.WriteLine();


            if (!options.Contains(input))
            {
                Console.WriteLine("Invalid input!");
                Console.WriteLine();
                Vote(ballot);
                return;
            }

            if (input == 0)
            {
                ballot.Output();
            }
            if (input == 2)
            {
                ballot.currentContestIndex--;
                ballot.currentCandidateIndex = 0;
            }
            if (input == 4)
            {
                ballot.currentCandidateIndex--;
            }
            if (input == 5)
            {
                if (currentCandidate.isSelected)
                {
                    if (currentCandidate.party == "WRITE-IN")
                    {
                        currentCandidate.fullName = "";
                    }
                    currentCandidate.isSelected = false;
                }
                else
                {
                    if (currentContest.GetHowManySelected() >= currentContest.numberToVote)
                    {
                        Console.WriteLine("Overvote!");
                    }
                    else
                    {
                        if (currentCandidate.party == "WRITE-IN")
                        {
                            Console.WriteLine("Enter the write-in name: ");
                            string nameInput = Console.ReadLine().Trim();
                            if (nameInput == "")
                            {
                                Console.WriteLine("Invalid name");
                            }
                            else
                            {
                                currentCandidate.fullName = nameInput;
                                currentCandidate.isSelected = true;
                            }
                        }
                    }
                    currentCandidate.isSelected = true;
                }
            }
            if (input == 6)
            {
                ballot.currentCandidateIndex++;
            }
            if (input == 8)
            {
                if (isDone)
                {
                    return;
                }
                else
                {
                    ballot.currentContestIndex++;
                    ballot.currentCandidateIndex = 0;
                }
            }

            Console.WriteLine();
            Vote(ballot);
        }

        static int ReadKey()
        {
            while (true)
            {
                char ch = Console.ReadKey().KeyChar;
                int result;

                if (int.TryParse(ch.ToString(), out result))
                {
                    return result;
                }
            }
        }
    }
}
