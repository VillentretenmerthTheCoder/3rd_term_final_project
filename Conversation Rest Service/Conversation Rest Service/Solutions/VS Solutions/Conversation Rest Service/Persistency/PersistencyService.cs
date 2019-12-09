using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace ConversationRESTService
{
    public static class PersistencyService
    {
        public const string GET_ALL = "Select * from Conversations";       
        public const string CONNECTION_STRING = @"Data Source=sensorrestservice20191117043106dbserver.database.windows.net;Initial Catalog=ConversationRestService20191209111353_db;User ID=bart0446;Password=Niwobiruf_34;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Get method that SQL can understand
        public static Conversation ReadNextElement(SqlDataReader reader)
        {
            Conversation conversation = new Conversation(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            return conversation;
        }

        // Get
        public static IEnumerable<Conversation> Get()
        {
            List<Conversation> conversations = new List<Conversation>();
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = GET_ALL;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                conversations.Add(ReadNextElement(reader));
                            }
                        }
                    }
                }

                return conversations;
            }
        }

        // Get a conversation by ID
        public static Conversation Get(string conversationID)
        {
            Conversation conversationReturned = null;
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = GET_ALL;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (ReadNextElement(reader).ID.ToString() == conversationID)
                                {
                                    conversationReturned = ReadNextElement(reader);
                                }
                            }
                        }
                    }
                }

                return conversationReturned; 
            }
        }

        // Post a conversation to the DB
        public static void Post(Conversation conversationAdded)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Insert into Conversations Values (@Param2, @Param3)";
                        cmd.Parameters.AddWithValue(parameterName: "@param2", conversationAdded.Question);
                        cmd.Parameters.AddWithValue(parameterName: "@param3", conversationAdded.TimeOfConversation);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Put (update) a conversation's info in the DB
        public static void Put(string conversationID, Conversation newConversationData)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open && newConversationData != null)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText =
                            "UPDATE Conversations SET ID = @param1, Question = @param2, TimeOfConversation = @param3 WHERE ID = @param4";

                        cmd.Parameters.AddWithValue(parameterName: "@param1", newConversationData.ID);
                        cmd.Parameters.AddWithValue(parameterName: "@param2", newConversationData.Question);
                        cmd.Parameters.AddWithValue(parameterName: "@param3", newConversationData.TimeOfConversation);

                        cmd.Parameters.AddWithValue(parameterName: "@param4", conversationID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Delete a conversation from the DB
        public static void Delete(string conversationID)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Conversations WHERE ID = @param1";

                        cmd.Parameters.AddWithValue(parameterName: "@param1", conversationID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
