

using ArchiveReader;
using ArchiveReader.Model;
using Microsoft.Data.SqlClient;


    SqlConnection sqlConnection;
    string connectionString = @"Data Source=Michael\MICHAEL;Initial Catalog=FileReader;Integrated Security=True;Encrypt=False;";

	try
	{
		sqlConnection = new SqlConnection(connectionString);
        FilePackageReader filePackageReader = new FilePackageReader();
        FilePackage filePackage = new FilePackage();
		sqlConnection.Open();
        Console.Write("Succes!");

        Console.WriteLine("Type the version of info");
        filePackage.Version = Console.ReadLine();
        string info = filePackageReader.ReadZip();       
        filePackage.DateTime = DateTime.Now;
        string insertQuery = "INSERT INTO FilePackage(Version, Info, Created_At) Values('" + filePackage.Version + "','" + info + "','" + filePackage.DateTime + "')";
        SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
        insertCommand.ExecuteNonQuery();
        Console.WriteLine("Data is succesfully inserted into table!");
        sqlConnection.Close();
    }
	catch (Exception e)
	{
        Console.WriteLine("Error " + e.Message);
    }
    Console.ReadLine();
   


