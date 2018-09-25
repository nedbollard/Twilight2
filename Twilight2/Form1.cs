using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using ClassLibrary2;

namespace Twilight2
{
    public partial class TwilightGolf : Form
    {
        public TwilightGolf()
        {
            InitializeComponent();

            Settings.LoadSettings();

            if (Settings.RoundId == 0)
            {
                new Round();
            };
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            var index = tabControl1.SelectedIndex;

            // 0 = intro, 1 = player, 2 = scores !!!

            switch (index)
            {
                case 0:
                    //txtIntro.Text = "The quality of the moment before releasing the arrow determines the quality of the shot ...";

                    break;
                case 1:
                    refreshPlayerPlayerIdListBox();
                    break;
                case 2:
                    RefreshEntriesTab();
                    break;
                case 3:
                    displayResultsTab();
                    break;

                default:
                    MessageBox.Show("You are in the Control.TabIndexChanged event. Index = " + index.ToString());
                    break;
            }

        }



        #region player tab

        private void refreshPlayerPlayerIdListBox()
        {
            txtNickname.Enabled = false;

            lblNickname.Visible = false;
            lblSurname.Visible = false;
            lblForename.Visible = false;
            lblHandicap.Visible = false;

            txtNickname.Visible = false;
            txtSurname.Visible = false;
            txtForename.Visible = false;
            txtHandicap.Visible = false;

            txtNickname.Text = "";
            txtSurname.Text = "";
            txtForename.Text = "";
            txtHandicap.Text = "";

            var playerRepo = new PlayerRepository();
            var playerList = playerRepo.ListAll();

            LstBoxNickName.DataSource = playerList;

            LstBoxNickName.DisplayMember = "PlayerId";
            LstBoxNickName.ValueMember = "PlayerId";

        }

        private void LstBoxNickName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstBoxNickName.SelectedIndex != -1)
            {
                var playerRepo = new PlayerRepository();
                ClassLibrary2.Player playerSelected = playerRepo.GetById(LstBoxNickName.Text);


                // this event is apparently being triggered @ setup ... the test value is "Classlibrary1.Player"
                // this value is displayed in the list box entries when the Messagebox is in play
                // bust is overwritten and invisible when it is not.

                if (playerSelected == null)
                {; }                                     // Null statement.  
                else
                {

                    lblNickname.Visible = true;
                    lblSurname.Visible = true;
                    lblForename.Visible = true;
                    lblHandicap.Visible = true;
                    lblEmail.Visible = true;

                    txtNickname.Visible = true;
                    txtNickname.Enabled = false;
                    txtSurname.Visible = true;
                    txtForename.Visible = true;
                    txtHandicap.Visible = true;
                    txtEmail.Visible = true;

                    txtNickname.Text = LstBoxNickName.Text;

                    txtSurname.Text = playerSelected.Surname;
                    txtForename.Text = playerSelected.Forename;
                    txtHandicap.Text = playerSelected.Handicap.ToString();
                    txtEmail.Text = playerSelected.EmailAddress;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtNickname.Enabled = true;

            lblNickname.Visible = true;
            lblSurname.Visible = true;
            lblForename.Visible = true;
            lblHandicap.Visible = true;
            lblEmail.Visible = true;

            txtNickname.Visible = true;
            txtSurname.Visible = true;
            txtForename.Visible = true;
            txtHandicap.Visible = true;
            txtEmail.Visible = true;

            txtNickname.Text = "";
            txtSurname.Text = "";
            txtForename.Text = "";
            txtHandicap.Text = "";
            txtEmail.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            short handicap = 0;
            if (Int16.TryParse(txtHandicap.Text, out handicap))
            {
                if ((handicap > 0) && (handicap < 40))
                {; }
                else
                { MessageBox.Show("Handicaps must be reasonable!"); };
            }
            else
            {
                MessageBox.Show("Handicaps must be integers!");
            };

            if ((handicap > 0) && (handicap < 40))
            {
                ClassLibrary2.Player player = new ClassLibrary2.Player();

                player.PlayerId = txtNickname.Text;
                player.Surname = txtSurname.Text;
                player.Forename = txtForename.Text;
                player.Handicap = handicap;
                player.EmailAddress = txtEmail.Text;

                var playerRepo = new PlayerRepository();
                playerRepo.Update(player);

                refreshPlayerPlayerIdListBox();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        #endregion

        #region entries tab

        private void RefreshEntriesTab()
        {

            txtRoundId.Text = Settings.RoundId.ToString();
            txtCourse.Text = Settings.CourseId;
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            var playerRepo = new PlayerRepository();
            var playerList = playerRepo.ListAll();

            LstBoxNickNameCS.DataSource = playerList;

            LstBoxNickNameCS.DisplayMember = "PlayerId";
            LstBoxNickNameCS.ValueMember = "PlayerId";
        }


        private void scoreHole9_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole9.Text, scoreHole9.Text);
        }

        private void scoreHole8_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole8.Text, scoreHole8.Text);
        }

        private void scoreHole7_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole7.Text, scoreHole7.Text);
        }

        private void scoreHole6_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole6.Text, scoreHole6.Text);
        }

        private void scoreHole5_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole5.Text, scoreHole5.Text);
        }

        private void scoreHole4_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole4.Text, scoreHole4.Text);
        }

        private void scoreHole3_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole3.Text, scoreHole3.Text);
        }

        private void scoreHole2_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole2.Text, scoreHole2.Text);
        }

        private void scoreHole1_Validating(object sender, CancelEventArgs e)
        {
            validateScore(lblHole1.Text, scoreHole1.Text);
        }

        private void validateScore(string hole, string score)

        // validate integer ... input is string
        // if valid calculate stabbies
        // return stabbies for display
        {
            int iscore = 0;
            if (Int32.TryParse(score, out iscore))
            {
                ;
            }
            else
            { MessageBox.Show("Scores really must be integers please - hole (" + hole + ")"); }
    ;

        }


        private void LstBoxNickNameCS_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                if (LstBoxNickNameCS.SelectedIndex != -1)
                {
                    // more TEMP DUPLICATION !!!

                    var playerRepo = new PlayerRepository();
                    var playerList = playerRepo.ListAll();

                    // list entry is a player object ...

                    var playerSelected = new Player();
                    playerSelected = playerList.Find(x => x.PlayerId.Equals(LstBoxNickNameCS.Text));

                    // this event is apparently being triggered @ setup ... the test value is "Classlibrary1.Player"
                    // this value is displayed in the list box entries when the Messagebox is in play
                    // bust is overwritten and invisible when it is not.

                    if (playerSelected == null)
                    {; }                                     // Null statement.  
                    else
                    {

                        txtSurnameCS.Text = playerSelected.Surname;
                        txtForenameCS.Text = playerSelected.Forename;
                        txtHandicapCS.Text = playerSelected.Handicap.ToString();

                        Round todaysRound = new Round();
                        Entry entry = new Entry();

                        bool entryExists = entry.getEntry(todaysRound.roundId, LstBoxNickNameCS.Text);

                        int hole = todaysRound.firstHole;
                        string s = "";

                        for (int i = 1; i < 10; i++)
                        {
                            s = hole.ToString();

                            switch (i)
                            {
                                case 1:
                                    lblHole1.Text = s;
                                    break;
                                case 2:
                                    lblHole2.Text = s;
                                    break;
                                case 3:
                                    lblHole3.Text = s;
                                    break;
                                case 4:
                                    lblHole4.Text = s;
                                    break;
                                case 5:
                                    lblHole5.Text = s;
                                    break;
                                case 6:
                                    lblHole6.Text = s;
                                    break;
                                case 7:
                                    lblHole7.Text = s;
                                    break;
                                case 8:
                                    lblHole8.Text = s;
                                    break;
                                case 9:
                                    lblHole9.Text = s;
                                    break;
                                default:
                                    break;

                            }

                            hole++;

                        }
                        if (entryExists)
                        {
                            ckbEntryFee.Checked = entry.entryFeePaid;
                            ckbGreenFees.Checked = entry.greenFeesPaid;

                            scoreHole1.Text = entry.scoreHole1.ToString();
                            scoreHole2.Text = entry.scoreHole2.ToString();
                            scoreHole3.Text = entry.scoreHole3.ToString();
                            scoreHole4.Text = entry.scoreHole4.ToString();
                            scoreHole5.Text = entry.scoreHole5.ToString();
                            scoreHole6.Text = entry.scoreHole6.ToString();
                            scoreHole7.Text = entry.scoreHole7.ToString();
                            scoreHole8.Text = entry.scoreHole8.ToString();
                            scoreHole9.Text = entry.scoreHole9.ToString();
                            scoreTotal.Text = entry.scoreTotal.ToString();

                            sfpHole1.Text = entry.sfpHole1.ToString();
                            sfpHole2.Text = entry.sfpHole2.ToString();
                            sfpHole3.Text = entry.sfpHole3.ToString();
                            sfpHole4.Text = entry.sfpHole4.ToString();
                            sfpHole5.Text = entry.sfpHole5.ToString();
                            sfpHole6.Text = entry.sfpHole6.ToString();
                            sfpHole7.Text = entry.sfpHole7.ToString();
                            sfpHole8.Text = entry.sfpHole8.ToString();
                            sfpHole9.Text = entry.sfpHole9.ToString();
                            sfpTotal.Text = entry.sfpTotal.ToString();

                            sfpHole1.BackColor = getForeColour(entry.sfpHole1);
                            sfpHole2.BackColor = getForeColour(entry.sfpHole2);
                            sfpHole3.BackColor = getForeColour(entry.sfpHole3);
                            sfpHole4.BackColor = getForeColour(entry.sfpHole4);
                            sfpHole5.BackColor = getForeColour(entry.sfpHole5);
                            sfpHole6.BackColor = getForeColour(entry.sfpHole6);
                            sfpHole7.BackColor = getForeColour(entry.sfpHole7);
                            sfpHole8.BackColor = getForeColour(entry.sfpHole8);
                            sfpHole9.BackColor = getForeColour(entry.sfpHole9);

                        }
                        else
                        {
                            ckbEntryFee.Checked = true;
                            ckbGreenFees.Checked = false;

                            scoreHole1.Text = " ";
                            scoreHole2.Text = " ";
                            scoreHole3.Text = " ";
                            scoreHole4.Text = " ";
                            scoreHole5.Text = " ";
                            scoreHole6.Text = " ";
                            scoreHole7.Text = " ";
                            scoreHole8.Text = " ";
                            scoreHole9.Text = " ";
                            scoreTotal.Text = " ";

                            sfpTotal.Text = " ";
                            sfpHole1.Text = " ";
                            sfpHole2.Text = " ";
                            sfpHole3.Text = " ";
                            sfpHole4.Text = " ";
                            sfpHole5.Text = " ";
                            sfpHole6.Text = " ";
                            sfpHole7.Text = " ";
                            sfpHole8.Text = " ";
                            sfpHole9.Text = " ";
                            sfpTotal.Text = " ";

                            sfpHole1.BackColor = Color.Empty;
                            sfpHole2.BackColor = Color.Empty;
                            sfpHole3.BackColor = Color.Empty;
                            sfpHole4.BackColor = Color.Empty;
                            sfpHole5.BackColor = Color.Empty;
                            sfpHole6.BackColor = Color.Empty;
                            sfpHole7.BackColor = Color.Empty;
                            sfpHole8.BackColor = Color.Empty;
                            sfpHole9.BackColor = Color.Empty;

                        };

                    }
                }
            }
        }

        private void btnSaveCS_Click(object sender, EventArgs e)
        {
            Entry currentPlayerEntry = new Entry();

            currentPlayerEntry.RoundId = txtRoundId.Text;
            currentPlayerEntry.PlayerId = LstBoxNickNameCS.Text;

            currentPlayerEntry.getEntry(Int32.Parse(txtRoundId.Text), LstBoxNickNameCS.Text);

            bool allGood = true;
            int shots = 0;

            allGood = Int32.TryParse(scoreHole1.Text, out shots);
            if (allGood)
            {
                currentPlayerEntry.scoreHole1 = shots;
                allGood = Int32.TryParse(scoreHole2.Text, out shots);

                if (allGood)
                {
                    currentPlayerEntry.scoreHole2 = shots;
                    allGood = Int32.TryParse(scoreHole3.Text, out shots);

                    if (allGood)
                    {
                        currentPlayerEntry.scoreHole3 = shots;
                        allGood = Int32.TryParse(scoreHole4.Text, out shots);

                        if (allGood)
                        {
                            currentPlayerEntry.scoreHole4 = shots;
                            allGood = Int32.TryParse(scoreHole5.Text, out shots);
                            if (allGood)
                            {
                                currentPlayerEntry.scoreHole5 = shots;
                                allGood = Int32.TryParse(scoreHole6.Text, out shots);

                                if (allGood)
                                {
                                    currentPlayerEntry.scoreHole6 = shots;
                                    allGood = Int32.TryParse(scoreHole7.Text, out shots);

                                    if (allGood)
                                    {
                                        currentPlayerEntry.scoreHole7 = shots;
                                        allGood = Int32.TryParse(scoreHole8.Text, out shots);

                                        if (allGood)
                                        {
                                            currentPlayerEntry.scoreHole8 = shots;
                                            allGood = Int32.TryParse(scoreHole9.Text, out shots);

                                            if (allGood)
                                            {
                                                currentPlayerEntry.scoreHole9 = shots;
                                            };
                                        };
                                    };
                                };
                            };

                        };
                    };
                };
            };

            if (allGood)
            {
                currentPlayerEntry.entryFeePaid = ckbEntryFee.Checked;
                currentPlayerEntry.greenFeesPaid = ckbGreenFees.Checked;

                currentPlayerEntry.CalculatePoints();

                sfpHole1.Text = currentPlayerEntry.sfpHole1.ToString();
                sfpHole1.BackColor = getForeColour(currentPlayerEntry.sfpHole1);

                sfpHole2.Text = currentPlayerEntry.sfpHole2.ToString();
                sfpHole2.BackColor = getForeColour(currentPlayerEntry.sfpHole2);

                sfpHole3.Text = currentPlayerEntry.sfpHole3.ToString();
                sfpHole3.BackColor = getForeColour(currentPlayerEntry.sfpHole3);

                sfpHole4.Text = currentPlayerEntry.sfpHole4.ToString();
                sfpHole4.BackColor = getForeColour(currentPlayerEntry.sfpHole4);

                sfpHole5.Text = currentPlayerEntry.sfpHole5.ToString();
                sfpHole5.BackColor = getForeColour(currentPlayerEntry.sfpHole5);

                sfpHole6.Text = currentPlayerEntry.sfpHole6.ToString();
                sfpHole6.BackColor = getForeColour(currentPlayerEntry.sfpHole6);

                sfpHole7.Text = currentPlayerEntry.sfpHole7.ToString();
                sfpHole7.BackColor = getForeColour(currentPlayerEntry.sfpHole7);

                sfpHole8.Text = currentPlayerEntry.sfpHole8.ToString();
                sfpHole8.BackColor = getForeColour(currentPlayerEntry.sfpHole8);

                sfpHole9.Text = currentPlayerEntry.sfpHole9.ToString();
                sfpHole9.BackColor = getForeColour(currentPlayerEntry.sfpHole9);

                currentPlayerEntry.CalculateTotals();

                scoreTotal.Text = currentPlayerEntry.scoreTotal.ToString();
                sfpTotal.Text = currentPlayerEntry.sfpTotal.ToString();

                currentPlayerEntry.updateEntry();
            }
            else
            {
                MessageBox.Show("All scores need to be integers");
            };

        }

        private Color getForeColour(int points)
        {
            switch (points)
            {
                case 0:
                    return (Color.Plum);
                case 1:
                    return (Color.Pink);
                case 2:
                    return (Color.LimeGreen);
                case 3:
                    return (Color.Gold);
                default:
                    return (Color.Yellow);
            }
        }

        private void btnCancelCS_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        #endregion

        #region leader board

        public void displayResultsTab()
        {
            //need to list: PlayerScore.NickName, PlayerScore.Points, PlayerScore.Score, (Player.Handicap) in a datagridview control

            Entry score = new Entry();

            List<PlayerResult> resultsList = score.getResultsList();

            // descending handicap with descending points

            resultsList.Sort(delegate (PlayerResult x, PlayerResult y)
            {
                if (x.SFPTotal.CompareTo(y.SFPTotal) == 0)
                {
                    return y.Handicap.CompareTo(x.Handicap);
                }
                else
                {
                    return y.SFPTotal.CompareTo(x.SFPTotal);
                };
            }
            );

            //dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgvResults.DataSource = resultsList;

            dgvResults.Columns[1].HeaderText = "Points";
            dgvResults.Columns[2].HeaderText = "Nett";
            dgvResults.Columns[3].HeaderText = "Gross";
        }

        #endregion

    }
} 