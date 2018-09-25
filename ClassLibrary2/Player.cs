using System;
using System.IO;
using System.Collections.Generic;

namespace ClassLibrary2

{
    public class Player : EntityBase
    {
        public string PlayerId { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public int Handicap { get; set; }
        public string EmailAddress { get; set; }

        public List<Player> List()
        {
            List<Player> keyList = new List<Player>();
            return (keyList);
        }


        public void Insert(Player Player) { }


        public void Update(Player Player) { }


        public void Delete(int PlayerId) { }

    }

    //The PlayerRepository class implements the generic IRepository<T> interface.

    public class PlayerRepository : IRepository<Player>

    {
        //Write your code here to implement each of the methods of the IRepository interface.

        private static string fileName = "players.txt";
        private static string fullFileName = Settings.fileDirectory + fileName;

        public Player GetById(string playerId)
        {
            List<Player> playerList = ListAll();

            Player playerSelected = playerList.Find(x => x.PlayerId.Equals(playerId));

            return (playerSelected);
        }

        public List<Player> ListAll()

        {
            List<Player> playerList = new List<Player>();

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

                        string[] playerFields = new string[5];
                        playerFields = line.Split(',');

                        playerList.Add(new Player() { PlayerId = playerFields[0], Surname = playerFields[1], Forename = playerFields[2], Handicap = Int32.Parse(playerFields[3]), EmailAddress = playerFields[4] });

                    }
                }
            }
            catch (Exception ex)
            {
                // Let the user know what went wrong.

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }

            return (playerList);
        }

        public void Create(Player player) { return; }


        public void Update(Player player)
        {
            updatePlayer(player);

            return;
        }


        public void Delete(Player player) { return; }

        private void updatePlayer(Player player)
        {

            List<Player> playerList = new List<Player>();

            bool playerExists = false;

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

                        string[] playerFields = new string[5];
                        playerFields = line.Split(',');

                        if (player.PlayerId == playerFields[0])
                        {
                            playerExists = true;

                            playerList.Add(new Player() { PlayerId = player.PlayerId, Surname = player.Surname, Forename = player.Forename, Handicap = player.Handicap, EmailAddress = player.EmailAddress  });
                            Console.WriteLine(player.PlayerId + " ... updated");
                        }
                        else
                        {
                            playerList.Add(new Player() { PlayerId = playerFields[0], Surname = playerFields[1], Forename = playerFields[2], Handicap = Int32.Parse(playerFields[3]), EmailAddress = playerFields[4] });
                        }
                    }

                    // at end, add any newplayer

                    if (playerExists)
                    {; }
                    else
                    {
                        playerList.Add(new Player() { PlayerId = player.PlayerId, Surname = player.Surname, Forename = player.Forename, Handicap = player.Handicap, EmailAddress = player.EmailAddress });
                        Console.WriteLine(player.PlayerId + " ... added");
                    }

                }
            }
            catch (Exception ex)
            {
                // Let the user know what went wrong.

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }

            RefreshPlayersList(playerList);
        }

        private void RefreshPlayersList(List<Player> playerList)
        {
            //rename file with current timestamp

            FileHandler.BackupFile(fileName);

            playerList.Sort(delegate (Player x, Player y)
            {
                return x.PlayerId.CompareTo(y.PlayerId);
            }
            );

            try
            {
                using (StreamWriter sw = new StreamWriter(fullFileName))
                {
                    foreach (var Player in playerList)
                    {
                        var playerRec = Player.PlayerId + "," + Player.Surname + "," + Player.Forename + "," + Player.Handicap.ToString() + "," + Player.EmailAddress;

                        sw.WriteLine(playerRec);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be refreshed/rewritten:");
                Console.WriteLine(ex.Message);
            }
        }

    }
}
