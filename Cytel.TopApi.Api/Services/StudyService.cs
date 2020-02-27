using Cytel.Top.Api.Interfaces;
using Cytel.Top.Api.Models;
using Cytel.Top.SQS;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Cytel.Top.Api.Services
{
    /// <summary>
    /// Study Repository class created for performing the database operations
    /// </summary>
    public class StudyService  : IStudyService
    {
        /// <summary>
        /// stores the connection string value
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Constructor that intializes the connection strting
        /// </summary>
        /// <param name="configuration"></param>
        public StudyService(IConfiguration configuration)
        {
            connectionString = "User ID=postgres;Password=Cytel1234;Host=cyteldb.c8owe0hgyui5.ap-south-1.rds.amazonaws.com;Port=5432;Database=CytelPOC;";
        }

       /// <summary>
       /// Initializes a new instance of NpgSQLConnection
       /// 
       /// </summary>
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        /// <summary>
        /// Add Method created to insert entries to the database
        /// </summary>
        /// <param name="item"></param>
        public void Add(Study item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO public.\"Inputs\"(\"StudyName\",\"StudyStartDate\",\"EstimatedCompletionDate\",\"ProtocolID\",\"StudyGroup\",\"Phase\",\"PrimaryIndication\",\"SecondaryIndication\") VALUES(@StudyName,@StudyStartDate,@EstimatedCompletionDate,@ProtocolID,@StudyGroup,@Phase, @PrimaryIndication, @SecondaryIndication);", item);
            }

            //Sends Message to SQS using SQSClient
            using(SQSClient sqsClient = new SQSClient())
            {
                sqsClient.SendMessageTOSQS(JsonConvert.SerializeObject(item),item.ProtocolID);
            }

        }

        /// <summary>
        /// Method created to Select all Data from the input table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Study> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Study>("SELECT * FROM public.\"Inputs\"");
            }
        }

        /// <summary>
        /// Method created to select a particular record from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Study FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Study>("SELECT * FROM public.\"Inputs\" WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Function to remove an entry from input table
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Inputs WHERE Id=@Id", new { Id = id });
            }
        }

        /// <summary>
        /// Function created to update an entry in the input table
        /// </summary>
        /// <param name="item"></param>
        public void Update(Study item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE Inputs SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }
    }
}
