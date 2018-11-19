using LibraryDataAgent.Interfaces;
using LibraryDataAgent.Models;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;

namespace LibraryDataAgent
{
    public class BooksDataAgent : IBooksDataAgent
    {
        class DBConnect
        {
            private MySqlConnection connection;

            public DBConnect()
            {
                InitializeDB();
            }

            public string Server { get; set; }
            public string User_id { get; set; }
            public string Database { get; set; }
            public string User_password { get; set; }

            private void InitializeDB()
            {
                Server = "localhost;";
                Database = "library;";
                User_id = "root;";
                User_password = "root;";

                string connectionString = $"SERVER={Server};DATABASE={Database};USERID={User_id};PASSWORD={User_password};";

                connection = new MySqlConnection(connectionString);
                connection.Open();
            }

            public List<string[]> Select(string query)
            {
                List<string[]> livro = new List<string[]>();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string[] row = new string[dataReader.FieldCount];
                    for (int i = 0; i < row.Length; i++)
                    {
                        row[i] = dataReader[i].ToString();
                    }
                    livro.Add(row);
                }
                return livro;
            }
        }
    }
}