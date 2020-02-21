using Cytel.Top.Model;
using Cytel.Top.SQS;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Cytel.Top.Repository
{
    public class StudyRepository  : IRepository<Study>
    {
        private string connectionString;
        public StudyRepository(IConfiguration configuration)
        {
           // connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            connectionString = "User ID=postgres;Password=Cytel1234;Host=cyteldb.c8owe0hgyui5.ap-south-1.rds.amazonaws.com;Port=5432;Database=CytelPOC;";
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Study item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                // dbConnection.Execute("INSERT INTO customer (name,phone,email,address) VALUES(@Name,@Phone,@Email,@Address)", item);
                dbConnection.Execute("INSERT INTO public.\"Inputs\"(\"StudyName\",\"StudyStartDate\",\"EstimatedCompletionDate\",\"ProtocolID\",\"StudyGroup\",\"Phase\",\"PrimaryIndication\",\"SecondaryIndication\") VALUES(@StudyName,@StudyStartDate,@EstimatedCompletionDate,@ProtocolID,@StudyGroup,@Phase, @PrimaryIndication, @SecondaryIndication);", item);
            }

            using(SQSClient sqsClient = new SQSClient())
            {
                sqsClient.SendMessageTOSQS(JsonConvert.SerializeObject(item),item.ProtocolID);
            }

        }

        public IEnumerable<Study> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Study>("SELECT * FROM public.\"Inputs\"");
            }
        }

        public Study FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Study>("SELECT * FROM public.\"Inputs\" WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM customer WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Study item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }
    }
}
