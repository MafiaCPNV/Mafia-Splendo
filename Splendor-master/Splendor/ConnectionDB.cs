using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Splendor
{
    /// <summary>
    /// contains methods and attributes to connect and deal with the database
    /// TO DO : le modèle de données n'est pas super, à revoir!!!!
    /// </summary>
    class ConnectionDB
    {
        //connection to the database
        private SQLiteConnection m_dbConnection;

        /// <summary>
        /// constructor : creates the connection to the database SQLite
        /// </summary>
        public ConnectionDB()
        {

            SQLiteConnection.CreateFile("Splendor.sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=Splendor.sqlite;Version=3;");
            m_dbConnection.Open();

            //create and insert players
            CreateInsertPlayer();
            //Create and insert cards
            //TO DO
            CreateInsertCards();
            //Create and insert ressources
            //TO DO
            CreateInsertRessources();
        }


        public void ExecQuery(string Sql)
        {
            SQLiteCommand command = new SQLiteCommand(Sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// get the list of cards according to the level
        /// </summary>
        /// <returns>cards stack</returns>
        public Stack<Card> GetListCardAccordingToLevel(int level)
        {

            //Get all the data from card table selecting them according to the data
            string sql = "select * from card where level=" + level;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            //Create an object "Stack of Card"
            Stack<Card> listCard = new Stack<Card>();

            while (reader.Read())
            {
                Card card = new Card();
                card.Level = (int)reader["level"];
                card.Idcard = (int)reader["idcard"];
                card.PrestigePt = (int)reader["nbPtPrestige"];
                card.Ress = (Ressources)reader["fkRessource"];

                sql = "select * from cost where fkCard =" + card.Idcard;
                SQLiteCommand commande = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader Costreader = commande.ExecuteReader();

                int numtab = 0;

                while (Costreader.Read())
                {
                    numtab = (int)Costreader["fkRessource"];

                    card.Cout[numtab] = (int)Costreader["nbRessource"];
                }

                listCard.Push(card);



            }

            return listCard;
        }


        /// <summary>
        /// create the "player" table and insert data
        /// </summary>
        private void CreateInsertPlayer()
        {
            string sql = "CREATE TABLE player (id INT PRIMARY KEY, pseudo VARCHAR(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ExecQuery("insert into player (id, pseudo) values (0, 'Fred')");
            ExecQuery("insert into player (id, pseudo) values (1, 'Harry')");
            ExecQuery("insert into player (id, pseudo) values (2, 'Sam')");

        }


        /// <summary>
        /// get the name of the player according to his id
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns></returns>
        public string GetPlayerName(int id)
        {
            string sql = "select pseudo from player where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader["pseudo"].ToString();
            }
            return name;
        }

        /// <summary>
        /// create the table "ressources" and insert data
        /// </summary>
        private void CreateInsertRessources()
        {
            string sql = "CREATE TABLE Ressource (IdRessource INT PRIMARY KEY)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ExecQuery("insert into Ressource (IdRessource) values (0)");
            ExecQuery("insert into Ressource (IdRessource) values (1)");
            ExecQuery("insert into Ressource (IdRessource) values (2)");
            ExecQuery("insert into Ressource (IdRessource) values (3)");
            ExecQuery("insert into Ressource (IdRessource) values (4)");
            ExecQuery("insert into Ressource (IdRessource) values (5)");


        }

        /// <summary>
        ///  create tables "cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        {
            string sql = "CREATE TABLE Card (IdCard INT PRIMARY KEY, FkRessource INT, Level INT, NbPtPrestige INT, FkPlayer)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE Cost (IdCost INTEGER PRIMARY KEY AUTOINCREMENT, FkCard INT, FkRessource INT, NbRessource INT)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (2, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (3, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (4, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (5, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (6, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (7, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (8, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (9, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (10, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (11, 0,4,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (12, 0,3,5)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (13, 0,3,5)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (14, 0,3,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (15, 0,3,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (16, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (17, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (18, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (19, 0,3,5)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (20, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (21, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (22, 0,3,5)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (23, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (24, 0,3,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (25, 0,3,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (26, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (27, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (28, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (29, 0,3,5)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (30, 0,3,4)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (31, 0,3,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (32, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (33, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (34, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (35, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (36, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (37, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (38, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (39, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (40, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (41, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (42, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (43, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (44, 0,2,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (45, 0,2,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (46, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (47, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (48, 0,2,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (49, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (50, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (51, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (52, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (53, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (54, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (55, 0,2,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (56, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (57, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (58, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (59, 0,2,2)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (60, 0,2,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (61, 0,2,3)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (62, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (63, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (64, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (65, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (66, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (67, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (68, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (69, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (70, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (71, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (72, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (73, 0,1,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (74, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (75, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (76, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (77, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (78, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (79, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (80, 0,1,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (81, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (82, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (83, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (84, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (85, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (86, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (87, 0,1,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (88, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (89, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (90, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (91, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (92, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (93, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (94, 0,1,1)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (95, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (96, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (97, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (98, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (99, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (100, 0,1,0)");
            ExecQuery("insert into card(idcard, fkRessource, level, nbPtPrestige) values (101, 0,1,1)");

            // TABLE COST

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (3,0,4)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (6,0,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (7,0,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (9,0,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (11,0,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (13,0,7)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (14,0,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (15,0,5)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (16,0,3)");






            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (23,0,7)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (25,0,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (27,0,6)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (29,0,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (30,0,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (31,0,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (32,0,5)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (33,0,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (34,0,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (35,0,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (36,0,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (38,0,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (39,0,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (42,0,3)");





            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (48,0,6)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (51,0,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (53,0,2)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (57,0,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (59,0,5)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (62,0,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (63,0,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (64,0,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (66,0,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (67,0,2)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (70,0,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (72,0,1)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (76,0,2)");




            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (81,0,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (84,0,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (85,0,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (86,0,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (88,0,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (91,0,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (93,0,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (94,0,4)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (96,0,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (97,0,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (98,0,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (100,0,1)");
            //
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (2,1,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (3,1,4)");




            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (8,1,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (9,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (11,1,3)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (15,1,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (16,1,6)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (17,1,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (20,1,7)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (22,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (24,1,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (25,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (27,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (29,1,7)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (31,1,5)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (34,1,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (35,1,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (37,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (39,1,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (41,1,5)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (42,1,5)");




            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (47,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (49,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (51,1,2)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (55,1,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (57,1,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (58,1,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (60,1,6)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (62,1,1)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (66,1,1)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (70,1,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (71,1,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (72,1,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (73,1,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (74,1,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (77,1,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (78,1,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (79,1,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (82,1,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (83,1,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (84,1,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (85,1,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (86,1,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (88,1,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (91,1,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (92,1,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (93,1,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (95,1,1)");




            
            //
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (5,2,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (6,2,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (7,2,3)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (11,2,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (13,2,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (14,2,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (15,2,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (18,2,7)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (19,2,7)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (21,2,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (24,2,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (25,2,5)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (27,2,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (30,2,6)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (33,2,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (34,2,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (35,2,2)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (38,2,4)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (40,2,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (43,2,5)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (46,2,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (47,2,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (49,2,3)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (53,2,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (54,2,5)");




            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (59,2,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (61,2,6)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (62,2,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (64,2,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (65,2,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (66,2,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (67,2,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (68,2,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (70,2,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (71,2,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (72,2,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (74,2,1)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (78,2,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (79,2,1)");








            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (88,2,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (89,2,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (90,2,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (92,2,2)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (96,2,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (97,2,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (100,2,2)");
            //
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (2,3,4)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (4,3,3)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (8,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (9,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (10,3,4)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (12,3,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (14,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (15,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (16,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (17,3,6)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (21,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (22,3,7)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (24,3,5)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (26,3,7)");




            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (31,3,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (33,3,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (36,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (37,3,5)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (39,3,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (40,3,2)");




            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (45,3,6)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (46,3,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (49,3,2)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (52,3,5)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (55,3,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (56,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (57,3,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (58,3,4)");






            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (65,3,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (68,3,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (69,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (70,3,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (71,3,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (72,3,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (74,3,1)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (77,3,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (79,3,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (81,3,2)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (85,3,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (86,3,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (87,3,4)");





            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (93,3,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (95,3,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (96,3,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (97,3,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (98,3,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (99,3,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (100,3,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (101,3,4)");
            //
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (4,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (5,4,4)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (7,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (8,4,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (10,4,4)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (12,4,7)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (14,4,5)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (17,4,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (19,4,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (21,4,6)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (24,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (25,4,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (28,4,7)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (30,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (31,4,3)");




            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (36,4,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (38,4,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (40,4,4)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (43,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (44,4,6)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (46,4,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (47,4,3)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (50,4,5)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (51,4,3)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (53,4,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (55,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (56,4,5)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (58,4,1)");





            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (64,4,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (65,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (66,4,1)");







            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (74,4,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (75,4,3)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (76,4,2)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (78,4,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (79,4,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (80,4,4)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (81,4,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (82,4,2)");


            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (85,4,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (86,4,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (88,4,1)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (89,4,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (91,4,1)");



            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (95,4,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (97,4,1)");

            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (99,4,2)");
            ExecQuery("insert into Cost(fkCard,fkRessource, nbRessource) values (100,4,1)");
            







        }

    }
}
