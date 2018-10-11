﻿/**
 * \file      frmAddVideoGames.cs
 * \author    F. Andolfatto
 * \version   1.0
 * \date      August 22. 2018
 * \brief     Form to play.
 *
 * \details   This form enables to choose coins or cards to get ressources (precious stones) and prestige points 
 * to add and to play with other players
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splendor
{
    /// <summary>
    /// manages the form that enables to play with the Splendor
    /// </summary>
    public partial class frmSplendor : Form
    {
        //used to store the number of coins selected for the current round of game
        private int nbRubis;
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        private int nbSaphir;
        private int CoinClick;

        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn;

        /// <summary>
        /// constructor
        /// </summary>
        public frmSplendor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads the form and initialize data in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_Load(object sender, EventArgs e)
        {
            lblGoldCoin.Text = "5";

            lblDiamandCoin.Text = "7";
            lblEmeraudeCoin.Text = "7" ;
            lblOnyxCoin.Text = "7";
            lblRubisCoin.Text = "7";
            lblSaphirCoin.Text = "7";

            conn = new ConnectionDB();

            //load cards from the database
            //they are not hard coded any more
            //TO DO

            /*
            Card card11 = new Card();
            card11.Level = 1;
            card11.PrestigePt = 1;
            card11.Cout = new int[] { 1, 0, 2, 0, 2 };
            card11.Ress = Ressources.Rubis;

            Card card12 = new Card();
            card12.Level = 1;
            card12.PrestigePt = 0;
            card12.Cout = new int[] { 0, 1, 2, 1, 0 };
            card12.Ress = Ressources.Saphir;
            


            txtLevel11.Text = card11.ToString();
            txtLevel12.Text = card12.ToString();
            */



            //load cards from the database
            Stack<Card> listCardOne = conn.GetListCardAccordingToLevel(1);
            Stack<Card> listCardtow = conn.GetListCardAccordingToLevel(2);
            Stack<Card> listCardtree = conn.GetListCardAccordingToLevel(3);
            Stack<Card> listCardfour = conn.GetListCardAccordingToLevel(4);
            //Go through the results
            //Don't forget to check when you are at the end of the stack

            //load cards from the database
            
            //Level 1
            int nbDataInStack = listCardOne.Count;
            int i = 0;
            foreach (Control ctrl in flwCardLevel1.Controls)
            {
                if (i < nbDataInStack)
                {
                    ctrl.Text = listCardOne.Pop().ToString();
                    i++;
                }

            }

            //Levle 2
            nbDataInStack = listCardtow.Count;
            i = 0;
            foreach (Control ctrl in flwCardLevel2.Controls)
            {
                if (i < nbDataInStack)
                {
                    ctrl.Text = listCardtow.Pop().ToString();
                    i++;
                }

            }

            //levle 3
            nbDataInStack = listCardtree.Count;
            i = 0;
            foreach (Control ctrl in flwCardLevel3.Controls)
            {
                if (i < nbDataInStack)
                {
                    ctrl.Text = listCardtree.Pop().ToString();
                    i++;
                }

            }

            // Noble
            nbDataInStack = listCardfour.Count;
            i = 0;
            foreach (Control ctrl in flwCardNoble.Controls)
            {
                if (i < nbDataInStack)
                {
                    ctrl.Text = listCardfour.Pop().ToString();
                    i++;
                }

            }

            //fin TO DO

            this.Width = 680;
            this.Height = 540;

            enableClicLabel = false;

            lblChoiceDiamand.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceEmeraude.Visible = false;
            cmdValidateChoice.Visible = false;
            cmdNextPlayer.Visible = false;

            //we wire the click on all cards to the same event
            //TO DO for all cards
            txtLevel11.Click += ClickOnCard;
        }

        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
        }

        /// <summary>
        /// click on the play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.Width = 680;
            this.Height = 780;

            int id = 0;
           
            LoadPlayer(id);

        }

        public void testCoin()
        {

            if (lblRubisCoin.Text == "0" || nbRubis == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;

                }

            else if (CoinClick == 0)
                {
                    lblRubisCoin.Enabled = true;
                    lblSaphirCoin.Enabled = true;
                    lblOnyxCoin.Enabled = true;
                    lblEmeraudeCoin.Enabled = true;
                    lblDiamandCoin.Enabled = true;
                    nbRubis++;

                }


             if (CoinClick == 1)
                 {
                        lblRubisCoin.Enabled = true;
                        lblSaphirCoin.Enabled = true;
                        lblOnyxCoin.Enabled = true;
                        lblEmeraudeCoin.Enabled = true;
                        lblDiamandCoin.Enabled = true;
                        nbSaphir++;

                    if (lblSaphirCoin.Text == "0" || nbSaphir == 2)
                    {
                            if (nbSaphir == 1 && (nbRubis == 1 || nbOnyx == 1 || nbEmeraude == 1 || nbDiamand == 1))
                                {
                                    lblSaphirCoin.Enabled = false;
                                    nbSaphir--;
                                }
                            else
                                {
                                    nbSaphir--;
                                    lblRubisCoin.Enabled = false;
                                    lblSaphirCoin.Enabled = false;
                                    lblOnyxCoin.Enabled = false;
                                    lblEmeraudeCoin.Enabled = false;
                                    lblDiamandCoin.Enabled = false;
                                }
                    }
                 }
            

                    if (lblOnyxCoin.Text == "0" || nbOnyx == 2)
                        {
                            lblOnyxCoin.Enabled = false;
                        }
                
            else if (CoinClick == 2)
                {
                    lblRubisCoin.Enabled = true;
                    lblSaphirCoin.Enabled = true;
                    lblOnyxCoin.Enabled = true;
                    lblEmeraudeCoin.Enabled = true;
                    lblDiamandCoin.Enabled = true;
                    nbOnyx++;
                }
            

            if (lblEmeraudeCoin.Text == "0" || nbEmeraude == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;
                }
            else if (CoinClick == 3)
                {
                    lblRubisCoin.Enabled = true;
                    lblSaphirCoin.Enabled = true;
                    lblOnyxCoin.Enabled = true;
                    lblEmeraudeCoin.Enabled = true;
                    lblDiamandCoin.Enabled = true;
                    nbEmeraude++;
                }
            
            if (lblDiamandCoin.Text == "0" || nbDiamand == 2)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;
                }
                else if (CoinClick == 4)
                {
                    lblRubisCoin.Enabled = true;
                    lblSaphirCoin.Enabled = true;
                    lblOnyxCoin.Enabled = true;
                    lblEmeraudeCoin.Enabled = true;
                    lblDiamandCoin.Enabled = true;
                    nbDiamand++;
                }


            if (nbRubis +  nbSaphir + nbOnyx + nbEmeraude + nbDiamand >= 3)
                {
                    lblRubisCoin.Enabled = false;
                    lblSaphirCoin.Enabled = false;
                    lblOnyxCoin.Enabled = false;
                    lblEmeraudeCoin.Enabled = false;
                    lblDiamandCoin.Enabled = false;   
                }


        }

        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) { 

            enableClicLabel = true;

            string name = conn.GetPlayerName(currentPlayerId);

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = "";
            lblChoiceOnyx.Text = "";
            lblChoiceRubis.Text = "";
            lblChoiceSaphir.Text = "";
            lblChoiceEmeraude.Text = "";

            lblChoiceCard.Text = "";

            //no coins selected
            nbDiamand = 0;
            nbOnyx = 0;
            nbRubis = 0;
            nbSaphir = 0;
            nbEmeraude = 0;

            Player player = new Player();
            player.Name = name;
            player.Id = id;
            player.Ressources = new int[] { 2, 0, 1, 1, 1 };
            player.Coins = new int[] { 0, 1, 0, 1, 1 };

            lblPlayerDiamandCoin.Text = player.Coins[0].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[1].ToString();
            lblPlayerRubisCoin.Text = player.Coins[2].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[3].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[4].ToString();
            currentPlayerId = id;

            lblPlayer.Text = "Jeu de " + name;

            cmdPlay.Enabled = false;
        }

        /// <summary>
        /// click on the red coin (rubis) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRubisCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceRubis.Visible = true;
                //TO DO check if possible to choose a coin, update the number of available coin
                lblChoiceRubis.Enabled = true;
                CoinClick = 0;
                testCoin();

               
                lblChoiceRubis.Text = nbRubis + "\r\n";
            }
        }

        /// <summary>
        /// click on the blue coin (saphir) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSaphirCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceSaphir.Visible = true;
                lblChoiceSaphir.Enabled = true;
                CoinClick = 1;
                testCoin();

                lblChoiceSaphir.Text = nbSaphir + "\r\n";
            }


            
        }

        /// <summary>
        /// click on the black coin (onyx) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblOnyxCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceOnyx.Visible = true;
                lblChoiceOnyx.Enabled = true;
                CoinClick = 2;
                testCoin();

                lblChoiceOnyx.Text = nbOnyx + "\r\n";


            }
        }

        /// <summary>
        /// click on the green coin (emeraude) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmeraudeCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceEmeraude.Visible = true;
                lblChoiceEmeraude.Enabled = true;
                CoinClick = 3;
                testCoin();

                lblChoiceEmeraude.Text = nbEmeraude + "\r\n";


            }

        }

        /// <summary>
        /// click on the white coin (diamand) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDiamandCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceDiamand.Visible = true;
                lblChoiceDiamand.Enabled = true;
                CoinClick = 4;
                testCoin();
               
                lblChoiceDiamand.Text = nbDiamand + "\r\n";


            }
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidateChoice_Click(object sender, EventArgs e)
        {
            cmdNextPlayer.Visible = true;
            //TO DO Check if card or coins are selected, impossible to do both at the same time
            
            cmdNextPlayer.Enabled = true;
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertPlayer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A implémenter");
        }

        /// <summary>
        /// click on the next player to tell him it is his turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button
            
        }

        private void lblChoiceRubis_Click(object sender, EventArgs e)
        {
            if (nbRubis == 0)
            {
                lblChoiceRubis.Enabled = false;
            }
            else
            {
                lblChoiceRubis.Enabled = true;
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbRubis--;
                lblChoiceRubis.Text = nbRubis + "\r\n";
            }
        }
    }
}