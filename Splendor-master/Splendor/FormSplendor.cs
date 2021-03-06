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

        private int idplayer;

        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn;
        //New player
        private Player player = new Player();
        

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
            Stack<Card> listCardOne = conn.GetListCardAccordingToLevel(1);
            Stack<Card> listCardtow = conn.GetListCardAccordingToLevel(2);
            Stack<Card> listCardtree = conn.GetListCardAccordingToLevel(3);
            Stack<Card> listCardfour = conn.GetListCardAccordingToLevel(4);
            //Go through the results
            //Don't forget to check when you are at the end of the stack

            //load cards from the database
            
            //Level 1 cards
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

            //Levle 2 cards
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

            //Levle 3 cards
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

            // Noble cards
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
            
            //Level 1 cards
            txtLevel11.Click += ClickOnCard;
            txtLevel12.Click += ClickOnCard;
            txtLevel13.Click += ClickOnCard;
            txtLevel14.Click += ClickOnCard;

            //level 2 cards
            txtLevel21.Click += ClickOnCard;
            txtLevel22.Click += ClickOnCard;
            txtLevel23.Click += ClickOnCard;
            txtLevel24.Click += ClickOnCard;

            //level 3 cards
            txtLevel31.Click += ClickOnCard;
            txtLevel32.Click += ClickOnCard;
            txtLevel33.Click += ClickOnCard;
            txtLevel34.Click += ClickOnCard;

            //Noble cards
            txtNoble1.Click += ClickOnCard;
            txtNoble2.Click += ClickOnCard;
            txtNoble3.Click += ClickOnCard;
            txtNoble4.Click += ClickOnCard;
        }

        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            
            TextBox txtBox = sender as TextBox;
            
            //get the text displayed in the textbox that has been clicked
            MessageBox.Show(txtBox.Text);
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

            idplayer = 0;
           
            LoadPlayer(idplayer);

        }


        /// <summary>
        /// Test the number of coin that the player can take
        /// </summary>
        public void testCoin()
        {

            if (CoinClick == 0)
            {
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbRubis++;

                if (lblRubisCoin.Text == "0" || nbRubis == 2)
                {
                    if (nbRubis == 1 && (nbSaphir == 1 || nbOnyx == 1 || nbEmeraude == 1 || nbDiamand == 1))
                    {
                        lblRubisCoin.Enabled = false;
                        nbRubis--;
                     
                    }
                    else
                    {
                        if (nbRubis == 2 && (nbSaphir == 1 || nbOnyx == 1 || nbEmeraude == 1 || nbDiamand == 1))
                        {
                            nbRubis--;
                        }
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                    }
                }
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
                                    if (nbSaphir == 2 && (nbRubis == 1 || nbOnyx == 1 || nbEmeraude == 1 || nbDiamand == 1))
                                        {
                                            nbSaphir--;
                                        }
                                    lblRubisCoin.Enabled = false;
                                    lblSaphirCoin.Enabled = false;
                                    lblOnyxCoin.Enabled = false;
                                    lblEmeraudeCoin.Enabled = false;
                                    lblDiamandCoin.Enabled = false;
                                }
                    }
                 }


            if (CoinClick == 2)
            {
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbOnyx++;

                if (lblOnyxCoin.Text == "0" || nbOnyx == 2)
                {
                    if (nbOnyx == 1 && (nbSaphir == 1 || nbRubis == 1 || nbEmeraude == 1 || nbDiamand == 1))
                    {
                        lblOnyxCoin.Enabled = false;
                        nbOnyx--;
                    }
                    else
                    {
                        if (nbOnyx == 2 && (nbRubis == 1 || nbSaphir == 1 || nbEmeraude == 1 || nbDiamand == 1))
                        {
                            nbOnyx--;
                        }
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                    }
                }
            }

            if (CoinClick == 3)
            {
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbEmeraude++;

                if (lblEmeraudeCoin.Text == "0" || nbOnyx == 2)
                {
                    if ( nbEmeraude== 1 && (nbSaphir == 1 || nbRubis == 1 || nbOnyx == 1 || nbDiamand == 1))
                    {
                        lblEmeraudeCoin.Enabled = false;
                        nbEmeraude--;
                    }
                    else
                    {
                        if (nbEmeraude == 2 && (nbRubis == 1 || nbSaphir == 1 || nbOnyx == 1 || nbDiamand == 1))
                        {
                            nbEmeraude--;
                        }
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                    }
                }
            }
            if (CoinClick == 4)
            {
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbDiamand++;

                if (lblDiamandCoin.Text == "0" || nbOnyx == 2)
                {
                    if (nbDiamand == 1 && (nbSaphir == 1 || nbRubis == 1 || nbOnyx == 1 || nbEmeraude == 1))
                    {
                        lblDiamandCoin.Enabled = false;
                        nbDiamand--;
                    }
                    else
                    {
                        if (nbDiamand == 2 && (nbRubis == 1 || nbOnyx == 1 || nbEmeraude == 1 || nbSaphir == 1))
                        {
                            nbDiamand--;
                        }
                        lblRubisCoin.Enabled = false;
                        lblSaphirCoin.Enabled = false;
                        lblOnyxCoin.Enabled = false;
                        lblEmeraudeCoin.Enabled = false;
                        lblDiamandCoin.Enabled = false;
                    }
                }
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

        //reduce de number of the coin by 1
        public string Sustlblrocks(string lblrock)
        {
            int Valrock = Convert.ToInt16(lblrock);

            Valrock -= 1;

            return Convert.ToString(Valrock);

        }

        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) { 

            enableClicLabel = true;

            string name = conn.GetPlayerName(id);

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

            player.Name = name;
            player.Id = id;
            player.Ressources = new int[] { 0, 0, 0, 0, 0 };
            player.Coins = new int[] { 0, 0, 0, 0, 0 };

            lblPlayerDiamandCoin.Text = conn.GetCoins(idplayer, 4);
            lblPlayerOnyxCoin.Text = conn.GetCoins(idplayer, 2);
            lblPlayerRubisCoin.Text = conn.GetCoins(idplayer, 0);
            lblPlayerSaphirCoin.Text = conn.GetCoins(idplayer, 3);
            lblPlayerEmeraudeCoin.Text = conn.GetCoins(idplayer, 1);

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
                
                lblChoiceRubis.Enabled = true;
                CoinClick = 0;
                testCoin();

                
                lblRubisCoin.Text = Sustlblrocks(lblRubisCoin.Text);

               
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

                lblSaphirCoin.Text = Sustlblrocks(lblSaphirCoin.Text);

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

                lblOnyxCoin.Text = Sustlblrocks(lblOnyxCoin.Text);

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

                lblEmeraudeCoin.Text = Sustlblrocks(lblEmeraudeCoin.Text);

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

                lblDiamandCoin.Text = Sustlblrocks(lblDiamandCoin.Text);

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

            //Number of coin that the player takes
            player.Coins[0] += nbRubis;
            player.Coins[1] += nbEmeraude;
            player.Coins[2] += nbOnyx;
            player.Coins[3] += nbSaphir;
            player.Coins[4] += nbDiamand;

            //Save the data from the player
            for (int i = 0; i < player.Coins.Count(); i++)
            {

                conn.InsertCoinToPlayer(idplayer, i, player.Coins[i]);

            }

            //Show how many coins he has taken
            lblPlayerDiamandCoin.Text = conn.GetCoins(idplayer, 4);
            lblPlayerOnyxCoin.Text = conn.GetCoins(idplayer, 2);
            lblPlayerRubisCoin.Text = conn.GetCoins(idplayer, 0);
            lblPlayerSaphirCoin.Text = conn.GetCoins(idplayer, 3);
            lblPlayerEmeraudeCoin.Text = conn.GetCoins(idplayer, 1);

            //put to 0 the number of coin that the player has in the round
            nbRubis = 0;
            nbEmeraude = 0;
            nbOnyx = 0;
            nbSaphir = 0;
            nbDiamand = 0;

            lblChoiceRubis.Text = "0";
            lblChoiceEmeraude.Text = "0";
            lblChoiceOnyx.Text = "0";
            lblChoiceSaphir.Text = "0";
            lblChoiceDiamand.Text = "0";

            //Shows the next player buttun and disable the validation
            cmdNextPlayer.Visible = true;
            cmdValidateChoice.Enabled = false;


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
            
            //Enable the possibility to get coins
            lblRubisCoin.Enabled = true;
            lblEmeraudeCoin.Enabled = true;
            lblOnyxCoin.Enabled = true;
            lblSaphirCoin.Enabled = true;
            lblDiamandCoin.Enabled = true;

            
            cmdNextPlayer.Visible = true;
            cmdValidateChoice.Visible = false;

            //Verifie witch player is playing
            idplayer++;

            if (idplayer > 2)
            {
                idplayer = 0;
            }

            LoadPlayer(idplayer);

            cmdNextPlayer.Visible = false;
            cmdValidateChoice.Enabled = true;

        }

        /// <summary>
        /// Substraction of rubis coin choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblChoiceRubis_Click(object sender, EventArgs e)
        {

                //Enable the possibility to get coins
                lblChoiceRubis.Enabled = true;
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbRubis--;

                 if (nbRubis == 0)
                 {
                    lblChoiceRubis.Visible = false;
                 }

            int value = Int32.Parse(lblRubisCoin.Text);
                lblRubisCoin.Text = (value + 1).ToString(); 
                lblChoiceRubis.Text = nbRubis + "\r\n";
        }
        /// <summary>
        /// Substraction of Saphir coin choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblChoiceSaphir_Click(object sender, EventArgs e)
        {

                //Enable the possibility to get coins
                lblChoiceSaphir.Enabled = true;
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbSaphir--;

                if (nbSaphir == 0)
                {
                    lblChoiceSaphir.Visible = false;
                }

                int value = Int32.Parse(lblSaphirCoin.Text);
                lblSaphirCoin.Text = (value + 1).ToString();
                lblChoiceSaphir.Text = nbSaphir + "\r\n";
            
        }
        /// <summary>
        /// Substraction of Onyx coin choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblChoiceOnyx_Click(object sender, EventArgs e)
        {

                //Enable the possibility to get coins
                lblChoiceOnyx.Enabled = true;
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbOnyx--;

                if (nbOnyx == 0)
                {
                    lblChoiceOnyx.Visible = false;
                }

                int value = Int32.Parse(lblOnyxCoin.Text);
                lblOnyxCoin.Text = (value + 1).ToString();
                lblChoiceOnyx.Text = nbOnyx + "\r\n";
            
        }
        /// <summary>
        /// Substraction of Emeraude coin choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblChoiceEmeraude_Click(object sender, EventArgs e)
        {
                //Enable the possibility to get coins
                lblChoiceEmeraude.Enabled = true;
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbEmeraude--;

                if (nbEmeraude == 0)
                {
                    lblChoiceEmeraude.Visible = false;
                }

                int value = Int32.Parse(lblEmeraudeCoin.Text);
                lblEmeraudeCoin.Text = (value + 1).ToString();
                lblChoiceEmeraude.Text = nbEmeraude + "\r\n";
            
        }
        /// <summary>
        /// Substraction of Diamond coin choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblChoiceDiamand_Click(object sender, EventArgs e)
        {

                //Enable the possibility to get coins
                lblChoiceDiamand.Enabled = true;
                lblRubisCoin.Enabled = true;
                lblSaphirCoin.Enabled = true;
                lblOnyxCoin.Enabled = true;
                lblEmeraudeCoin.Enabled = true;
                lblDiamandCoin.Enabled = true;
                nbDiamand--;

                if (nbDiamand == 0)
                {
                    lblChoiceDiamand.Visible = false;
                }

                int value = Int32.Parse(lblDiamandCoin.Text);
                lblDiamandCoin.Text = (value + 1).ToString();
                lblChoiceDiamand.Text = nbDiamand + "\r\n";
            
        }
    }
}
