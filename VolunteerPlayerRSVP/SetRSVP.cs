//------------------------------------------------------------------------------
// <copyright file="CSSqlTrigger.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Text;
using System.Net;
using System.IO;

public partial class Triggers
{
    // Enter existing table or view for the target and uncomment the attribute line
    [Microsoft.SqlServer.Server.SqlTrigger(Name = "SetRSVP", Target = "TeamPersonnel", Event = "FOR INSERT, DELETE")]
    public static void SetRSVP()
    {
        SqlContext.Pipe.Send("Trigger Executed");
        
        SqlCommand command;
        SqlTriggerContext triggContext = SqlContext.TriggerContext;
        SqlPipe pipe = SqlContext.Pipe;
        SqlDataReader reader;

        switch (triggContext.TriggerAction)
        {
            case TriggerAction.Insert:
                // Retrieve the connection that the trigger is using
                using (SqlConnection connection = new SqlConnection(@"context connection=true"))
                {
                    connection.Open();
                    command = new SqlCommand(@"SELECT * FROM INSERTED;", connection);
                    reader = command.ExecuteReader();
                    reader.Read();
                    var teamId = reader[1];
                    var volunteerId = reader[2];
                    var userId = reader[3];
                    reader.Close();

                    string download = "";
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("content-type", "application/json");

                        var dataToPost = Encoding.Default.GetBytes("{\"EntityId\": " + volunteerId + ",\"EntityName\": \"Volunteer\",\"UserId\": " + userId + ",\"TeamId\": " + teamId + "}");
                        byte[] arr = client.UploadData("http://localhost:4264/api/RSVP/AllocateVolunteer", "POST", dataToPost);

                        //byte[] arr = client.DownloadData("http://localhost:4264/api/RSVP/Get");

                        download = Encoding.ASCII.GetString(arr);

                    }
                    pipe.Send("You inserted: TeamId:" + teamId + " VolunteerId:" + volunteerId + " UserId:" + userId + " API AllocateVolunteer:" + download);
                }

                break;
            case TriggerAction.Delete:
                // Retrieve the connection that the trigger is using
                using (SqlConnection connection = new SqlConnection(@"context connection=true"))
                {
                    connection.Open();
                    command = new SqlCommand(@"SELECT * FROM DELETED;", connection);
                    reader = command.ExecuteReader();
                    reader.Read();
                    var teamId = reader[1];
                    var volunteerId = reader[2];
                    var userId = reader[3];
                    reader.Close();

                    string download = "";
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("content-type", "application/json");

                        var dataToPost = Encoding.Default.GetBytes("{\"EntityId\": " + volunteerId + ",\"EntityName\": \"Volunteer\",\"UserId\": " + userId + ",\"TeamId\": " + teamId + "}");
                        byte[] arr = client.UploadData("http://localhost:4264/api/RSVP/UnAllocateVolunteer", "POST", dataToPost);

                        //byte[] arr = client.DownloadData("http://localhost:4264/api/RSVP/Get");
                        
                        download = Encoding.ASCII.`(arr);

                    }
                    pipe.Send("You deleted: TeamId:" + teamId + " VolunteerId:" + volunteerId + " UserId:" + userId + " API UnAllocateVolunteer:" + download);
                }

                break;
        }
    }
}

