using System;
using System.IO;

// http://csharpindepth.com/Articles/General/Singleton.aspx

namespace ClassLibrary2
{
    public sealed class Round
    {
        public int roundId { get; set; }
        public string datePlayed { get; set; }  // yyyy=mm-dd
        public string courseId { get; set; }
        public int firstHole { get; set; }

        private string todaysDate = DateTime.Now.ToString("yyyy-MM-dd");

        private string roundFileName = "round.txt";
        private string fullRoundFileName = Settings.fileDirectory + "round.txt";
        private string scoresFileName = "scores.txt";
        private string fullscoresFileName = Settings.fileDirectory + "scores.txt";

        private static readonly Lazy<Round> lazy = new Lazy<Round>(() => new Round());

        public static Round Instance { get { return lazy.Value; } }

        public Round()
        {
            bool newRoundReqd = false;
            int lastRoundId = 0;

            try
            {
                using (StreamReader sr = new StreamReader(fullRoundFileName))
                {
                    string line = sr.ReadLine();

                    if (string.IsNullOrEmpty(line))
                    {
                        newRoundReqd = true;
                        lastRoundId = 0;
                    }
                    else
                    {
                        string[] roundFields = line.Split(',');

                        if (todaysDate == roundFields[0])
                        {
                            datePlayed = roundFields[0];
                            roundId = Int32.Parse(roundFields[1]);

                            courseId = roundFields[2];
                            firstHole = Int32.Parse(roundFields[3]);
                        }
                        else
                        {
                            newRoundReqd = true;
                            lastRoundId = Int32.Parse(roundFields[1]);
                        };
                    }
                }

            }

            catch (Exception ex)
            {
                // Let the user know what went wrong.

                Console.WriteLine(roundFileName + " ... the file could not be read:");
                Console.WriteLine(ex.Message);
            }

            if (newRoundReqd)
            {
                NewRound(lastRoundId);
            };

            Settings.RoundId = roundId;
            Settings.CourseId = courseId;

        }

        private void NewRound(int X )
        {
            roundId = ++X;

            datePlayed = DateTime.Now.ToString("yyyy-MM-dd");

            string ddTodayString = DateTime.Now.ToString("dd");
            int ddTodayInt = Int32.Parse(ddTodayString);

            if ((ddTodayInt % 2) == 0)
            {
                courseId = "Blues";
            }
            else
            {
                courseId = "Whites";
            };

            Course todaysCourse = new Course(courseId);

            firstHole = todaysCourse.FirstHole;

            //rename round & scores files with current timestamp

            FileHandler.BackupFile(scoresFileName);

            FileHandler.BackupFile(roundFileName);

            try
            {
                using (StreamWriter sw = new StreamWriter(fullRoundFileName))
                {
                    var roundRec = datePlayed + "," + roundId + "," + courseId + "," + firstHole;
                    sw.WriteLine(roundRec);

                    Console.WriteLine("New round created: " + roundId.ToString() + " / " + courseId + " / " + datePlayed);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(roundFileName + "... the file could not be refreshed/rewritten:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
