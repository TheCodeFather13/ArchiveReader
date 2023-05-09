using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Sql.Repositories.Repositories
{
    public class ArchiveRepository
    {
        private readonly ArchiveDbContext _archiveDbContext;

        public ArchiveRepository(ArchiveDbContext archiveDbContext)
        {
            _archiveDbContext = archiveDbContext;
        }

        public async void InsertFileintoSqlDatabase()
        {
            string filePath = @"C:\Logs.txt";

            await using (SqlConnection sqlconnection = new SqlConnection(@"Data Source=Michael\Michael;Initial Catalog=MorganDB; Integrated Security=True;Encrypt=False;"))
            {
                sqlconnection.Open();

                // create table if not exists 
                string createTableQuery = @"Create Table [MyTable](ID int, [FileData] varbinary(max))";
                SqlCommand command = new SqlCommand(createTableQuery, sqlconnection);
                command.ExecuteNonQuery();

                // Converts text file(.txt) into byte[]
                byte[] fileData = File.ReadAllBytes(filePath);

                string insertQuery = @"Insert Into [MyTable] (ID,[FileData]) Values(1,@FileData)";

                // Insert text file Value into Sql Table by SqlParameter
                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlconnection);
                SqlParameter sqlParam = insertCommand.Parameters.AddWithValue("@FileData", fileData);
                sqlParam.DbType = DbType.Binary;
                insertCommand.ExecuteNonQuery();
            }
        }
    }
}
