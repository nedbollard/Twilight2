using System;
using System.IO;

using System.Collections.Generic;
using System.Text;

namespace ClassLibrary2
{
    public class Entry
    {
            private static string fileName = "entries.txt";
            private static string fullFileName = Settings.fileDirectory + fileName;

            public string RoundId { get; set; }
            public string PlayerId { get; set; }

            public bool entryFeePaid { get; set; }
            public bool greenFeesPaid { get; set; }

            public int scoreTotal { get; set; }
            public int scoreHole1 { get; set; }
            public int scoreHole2 { get; set; }
            public int scoreHole3 { get; set; }
            public int scoreHole4 { get; set; }
            public int scoreHole5 { get; set; }
            public int scoreHole6 { get; set; }
            public int scoreHole7 { get; set; }
            public int scoreHole8 { get; set; }
            public int scoreHole9 { get; set; }

            public int sfpTotal { get; set; }
            public int sfpHole1 { get; set; }
            public int sfpHole2 { get; set; }
            public int sfpHole3 { get; set; }
            public int sfpHole4 { get; set; }
            public int sfpHole5 { get; set; }
            public int sfpHole6 { get; set; }
            public int sfpHole7 { get; set; }
            public int sfpHole8 { get; set; }
            public int sfpHole9 { get; set; }
            
        // only default constructor

            public bool getEntry(int roundId, string playerId)
            {
                if (File.Exists(fullFileName))
                {
                    bool scoreExists = false;

                    try
                    {
                        // Create an instance of StreamReader to read from a file.
                        // The using statement also closes the StreamReader.

                        using (StreamReader sr = new StreamReader(fullFileName))
                        {
                            string line;

                            // Read and display lines from the file until the end of 
                            // the file is reached.

                            // unstring comma delimited data into a array of strings 

                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] scoreFields = new string[24];
                                scoreFields = line.Split(',');

                                if ((roundId == Int32.Parse(scoreFields[0])) && (playerId == scoreFields[1]))
                                {
                                    scoreExists = true;

                                    RoundId = scoreFields[0];
                                    PlayerId = scoreFields[1];
                                    scoreTotal = Int32.Parse(scoreFields[2]);
                                    scoreHole1 = Int32.Parse(scoreFields[3]);
                                    scoreHole2 = Int32.Parse(scoreFields[4]);
                                    scoreHole3 = Int32.Parse(scoreFields[5]);
                                    scoreHole4 = Int32.Parse(scoreFields[6]);
                                    scoreHole5 = Int32.Parse(scoreFields[7]);
                                    scoreHole6 = Int32.Parse(scoreFields[8]);
                                    scoreHole7 = Int32.Parse(scoreFields[9]);
                                    scoreHole8 = Int32.Parse(scoreFields[10]);
                                    scoreHole9 = Int32.Parse(scoreFields[11]);
                                    sfpTotal = Int32.Parse(scoreFields[12]);
                                    sfpHole1 = Int32.Parse(scoreFields[13]);
                                    sfpHole2 = Int32.Parse(scoreFields[14]);
                                    sfpHole3 = Int32.Parse(scoreFields[15]);
                                    sfpHole4 = Int32.Parse(scoreFields[16]);
                                    sfpHole5 = Int32.Parse(scoreFields[17]);
                                    sfpHole6 = Int32.Parse(scoreFields[18]);
                                    sfpHole7 = Int32.Parse(scoreFields[19]);
                                    sfpHole8 = Int32.Parse(scoreFields[20]);
                                    sfpHole9 = Int32.Parse(scoreFields[21]);

                                    entryFeePaid = (scoreFields[22] == "Y") ? true : false;
                                    greenFeesPaid = (scoreFields[23] == "Y") ? true : false;

                            };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Let the user know what went wrong.

                        Console.WriteLine(fileName + " ... file could not be read:");
                        Console.WriteLine(ex.Message);
                    }

                    return (scoreExists);
                }
                else
                {
                    return (false);
                };
            }

            public void CalculatePoints()
            {
                var playerRepo = new PlayerRepository();
                Player playerSelected = playerRepo.GetById(PlayerId);

                int handicap = playerSelected.Handicap;

                Course todaysCourse = new Course(Settings.CourseId);

                sfpHole1 = CalculatePointsForHole(scoreHole1, todaysCourse.Par[0], todaysCourse.Stroke[0], handicap);
                sfpHole2 = CalculatePointsForHole(scoreHole2, todaysCourse.Par[1], todaysCourse.Stroke[1], handicap);
                sfpHole3 = CalculatePointsForHole(scoreHole3, todaysCourse.Par[2], todaysCourse.Stroke[2], handicap);
                sfpHole4 = CalculatePointsForHole(scoreHole4, todaysCourse.Par[3], todaysCourse.Stroke[3], handicap);
                sfpHole5 = CalculatePointsForHole(scoreHole5, todaysCourse.Par[4], todaysCourse.Stroke[4], handicap);
                sfpHole6 = CalculatePointsForHole(scoreHole6, todaysCourse.Par[5], todaysCourse.Stroke[5], handicap);
                sfpHole7 = CalculatePointsForHole(scoreHole7, todaysCourse.Par[6], todaysCourse.Stroke[6], handicap);
                sfpHole8 = CalculatePointsForHole(scoreHole8, todaysCourse.Par[7], todaysCourse.Stroke[7], handicap);
                sfpHole9 = CalculatePointsForHole(scoreHole9, todaysCourse.Par[8], todaysCourse.Stroke[8], handicap);

            }

            public int CalculatePointsForHole(int score, int par, int stroke, int handicap)
            {
                int points = 0;
                int shots = 0;

                if (handicap > 36)
                {
                    if ((handicap - 36) >= stroke)
                    { shots = 3; }
                    else
                    { shots = 2; };
                }
                else if (handicap > 18)
                {
                    if ((handicap - 18) >= stroke)
                    { shots = 3; }
                    else
                    { shots = 2; };
                }
                else
                {
                    shots = 1;
                };

                points = Math.Max((shots + 2 + par - score), 0);

                return (points);
            }

            public void CalculateTotals()
            {
                scoreTotal = scoreHole1 +
                                   scoreHole2 +
                                   scoreHole3 +
                                   scoreHole4 +
                                   scoreHole5 +
                                   scoreHole6 +
                                   scoreHole7 +
                                   scoreHole8 +
                                   scoreHole9;

                sfpTotal = sfpHole1 +
                                sfpHole2 +
                                sfpHole3 +
                                sfpHole4 +
                                sfpHole5 +
                                sfpHole6 +
                                sfpHole7 +
                                sfpHole8 +
                                sfpHole9;

            }

            public void updateEntry()
            {
                List<Entry> ScoreList = new List<Entry>();

                bool scoreExists = false;

                try
                {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.

                    using (StreamReader sr = new StreamReader(fullFileName))
                    {
                        string line;

                        // Read and display lines from the file until the end of 
                        // the file is reached.

                        // unstring comma delimited data into a array of strings 

                        while ((line = sr.ReadLine()) != null)
                        {

                            string[] scoreFields = new string[24];
                            scoreFields = line.Split(',');

                            if ((RoundId == scoreFields[0]) && (PlayerId == scoreFields[1]))
                            {
                                scoreExists = true;

                                ScoreList.Add(new Entry()
                                {
                                    RoundId = RoundId,
                                    PlayerId = PlayerId,
                                    entryFeePaid = entryFeePaid,
                                    greenFeesPaid = greenFeesPaid,
                                    scoreTotal = scoreTotal,
                                    scoreHole1 = scoreHole1,
                                    scoreHole2 = scoreHole2,
                                    scoreHole3 = scoreHole3,
                                    scoreHole4 = scoreHole4,
                                    scoreHole5 = scoreHole5,
                                    scoreHole6 = scoreHole6,
                                    scoreHole7 = scoreHole7,
                                    scoreHole8 = scoreHole8,
                                    scoreHole9 = scoreHole9,
                                    sfpTotal = sfpTotal,
                                    sfpHole1 = sfpHole1,
                                    sfpHole2 = sfpHole2,
                                    sfpHole3 = sfpHole3,
                                    sfpHole4 = sfpHole4,
                                    sfpHole5 = sfpHole5,
                                    sfpHole6 = sfpHole6,
                                    sfpHole7 = sfpHole7,
                                    sfpHole8 = sfpHole8,
                                    sfpHole9 = sfpHole9,

                                }
                                );

                                Console.WriteLine(RoundId + "/" + PlayerId + " ... updated");
                            }
                            else
                            {
                                ScoreList.Add(new Entry()
                                {
                                    RoundId = scoreFields[0],
                                    PlayerId = scoreFields[1],
                                    entryFeePaid = (scoreFields[22] == "Y") ? true : false,
                                    greenFeesPaid = (scoreFields[23] == "Y") ? true : false,
                                    scoreTotal = Int32.Parse(scoreFields[2]),
                                    scoreHole1 = Int32.Parse(scoreFields[3]),
                                    scoreHole2 = Int32.Parse(scoreFields[4]),
                                    scoreHole3 = Int32.Parse(scoreFields[5]),
                                    scoreHole4 = Int32.Parse(scoreFields[6]),
                                    scoreHole5 = Int32.Parse(scoreFields[7]),
                                    scoreHole6 = Int32.Parse(scoreFields[8]),
                                    scoreHole7 = Int32.Parse(scoreFields[9]),
                                    scoreHole8 = Int32.Parse(scoreFields[10]),
                                    scoreHole9 = Int32.Parse(scoreFields[11]),
                                    sfpTotal = Int32.Parse(scoreFields[12]),
                                    sfpHole1 = Int32.Parse(scoreFields[13]),
                                    sfpHole2 = Int32.Parse(scoreFields[14]),
                                    sfpHole3 = Int32.Parse(scoreFields[15]),
                                    sfpHole4 = Int32.Parse(scoreFields[16]),
                                    sfpHole5 = Int32.Parse(scoreFields[17]),
                                    sfpHole6 = Int32.Parse(scoreFields[18]),
                                    sfpHole7 = Int32.Parse(scoreFields[19]),
                                    sfpHole8 = Int32.Parse(scoreFields[20]),
                                    sfpHole9 = Int32.Parse(scoreFields[21])
                                }
                                            );
                            };

                        }

                        // at end, add any new score
                        if (scoreExists)
                        {; }
                        else
                        {
                            ScoreList.Add(new Entry()
                            {
                                RoundId = RoundId,
                                PlayerId = PlayerId,
                                entryFeePaid = entryFeePaid,
                                greenFeesPaid = greenFeesPaid,
                                scoreTotal = scoreTotal,
                                scoreHole1 = scoreHole1,
                                scoreHole2 = scoreHole2,
                                scoreHole3 = scoreHole3,
                                scoreHole4 = scoreHole4,
                                scoreHole5 = scoreHole5,
                                scoreHole6 = scoreHole6,
                                scoreHole7 = scoreHole7,
                                scoreHole8 = scoreHole8,
                                scoreHole9 = scoreHole9,
                                sfpTotal = sfpTotal,
                                sfpHole1 = sfpHole1,
                                sfpHole2 = sfpHole2,
                                sfpHole3 = sfpHole3,
                                sfpHole4 = sfpHole4,
                                sfpHole5 = sfpHole5,
                                sfpHole6 = sfpHole6,
                                sfpHole7 = sfpHole7,
                                sfpHole8 = sfpHole8,
                                sfpHole9 = sfpHole9

                            });
                            Console.WriteLine(PlayerId + " ... added");
                        }

                    }
                }
                catch (Exception ex)
                {
                    // Let the user know what went wrong.

                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(ex.Message);
                }

                RefreshScores(ScoreList);
            }


            private void RefreshScores(List<Entry> ScoreList)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fullFileName))
                    {
                        foreach (var Score in ScoreList)
                        {
                            var playerRec =
                                    Score.RoundId + "," +
                                    Score.PlayerId + "," +
                                    Score.scoreTotal.ToString() + "," +
                                    Score.scoreHole1.ToString() + "," +
                                    Score.scoreHole2.ToString() + "," +
                                    Score.scoreHole3.ToString() + "," +
                                    Score.scoreHole4.ToString() + "," +
                                    Score.scoreHole5.ToString() + "," +
                                    Score.scoreHole6.ToString() + "," +
                                    Score.scoreHole7.ToString() + "," +
                                    Score.scoreHole8.ToString() + "," +
                                    Score.scoreHole9.ToString() + "," +
                                    Score.sfpTotal.ToString() + "," +
                                    Score.sfpHole1.ToString() + "," +
                                    Score.sfpHole2.ToString() + "," +
                                    Score.sfpHole3.ToString() + "," +
                                    Score.sfpHole4.ToString() + "," +
                                    Score.sfpHole5.ToString() + "," +
                                    Score.sfpHole6.ToString() + "," +
                                    Score.sfpHole7.ToString() + "," +
                                    Score.sfpHole8.ToString() + "," +
                                    Score.sfpHole9.ToString() + "," +
                                    ((Score.entryFeePaid) ? "Y" : "N") + "," +
                                    ((Score.greenFeesPaid) ? "Y" : "N")
                                    ;

                            sw.WriteLine(playerRec);
                            Console.WriteLine("score written for " + Score.RoundId.ToString() + "/" + Score.PlayerId.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The file could not be refreshed/rewritten:");
                    Console.WriteLine(ex.Message);
                }
            }

            public List<Entry> getScoreList()
            {
                List<Entry> ScoreList = new List<Entry>();

                try
                {
                    using (StreamReader sr = new StreamReader(fullFileName))
                    {
                        string line;

                        // Read and display lines from the file until the end of 
                        // the file is reached.

                        // unstring comma delimited data into a array of strings 

                        while ((line = sr.ReadLine()) != null)
                        {

                            string[] scoreFields = new string[22];
                            scoreFields = line.Split(',');

                            ScoreList.Add(new Entry()
                            {
                                RoundId = scoreFields[0],
                                PlayerId = scoreFields[1],
                                scoreTotal = Int32.Parse(scoreFields[2]),
                                scoreHole1 = Int32.Parse(scoreFields[3]),
                                scoreHole2 = Int32.Parse(scoreFields[4]),
                                scoreHole3 = Int32.Parse(scoreFields[5]),
                                scoreHole4 = Int32.Parse(scoreFields[6]),
                                scoreHole5 = Int32.Parse(scoreFields[7]),
                                scoreHole6 = Int32.Parse(scoreFields[8]),
                                scoreHole7 = Int32.Parse(scoreFields[9]),
                                scoreHole8 = Int32.Parse(scoreFields[10]),
                                scoreHole9 = Int32.Parse(scoreFields[11]),
                                sfpTotal = Int32.Parse(scoreFields[12]),
                                sfpHole1 = Int32.Parse(scoreFields[13]),
                                sfpHole2 = Int32.Parse(scoreFields[14]),
                                sfpHole3 = Int32.Parse(scoreFields[15]),
                                sfpHole4 = Int32.Parse(scoreFields[16]),
                                sfpHole5 = Int32.Parse(scoreFields[17]),
                                sfpHole6 = Int32.Parse(scoreFields[18]),
                                sfpHole7 = Int32.Parse(scoreFields[19]),
                                sfpHole8 = Int32.Parse(scoreFields[20]),
                                sfpHole9 = Int32.Parse(scoreFields[21])

                            });

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Let the user know what went wrong.

                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(ex.Message);
                }
                return ScoreList;
            }

            public List<PlayerResult> getResultsList()
            {
                List<PlayerResult> resultsList = new List<PlayerResult>();

                var playerRepo = new ClassLibrary2.PlayerRepository();
                var playerList = playerRepo.ListAll();

            try
                {
                    using (StreamReader sr = new StreamReader(fullFileName))
                    {
                        string line;

                        // Read and display lines from the file until the end of 
                        // the file is reached.

                        // unstring comma delimited data into a array of strings 

                        while ((line = sr.ReadLine()) != null)
                        {

                            string[] scoreFields = new string[22];
                            scoreFields = line.Split(',');

                            Player player = playerList.Find(x => x.PlayerId == scoreFields[1]);

                            int handicap = player.Handicap;
                            int scoreTot = Int32.Parse(scoreFields[2]);

                            resultsList.Add(new PlayerResult()
                            {
                                Nickname = scoreFields[1],
                                ScoreTotal = scoreTot,
                                ScoreNet = scoreTot - handicap,
                                SFPTotal = Int32.Parse(scoreFields[12]),
                                Handicap = handicap
                            });

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Let the user know what went wrong.

                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(ex.Message);
                }
                return resultsList;
            }
        }
    }



