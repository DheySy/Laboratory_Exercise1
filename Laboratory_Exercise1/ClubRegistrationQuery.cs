using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Exercise1
{
    public class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;

        public DataTable dataTable = new DataTable();
        public BindingSource bindingSource = new BindingSource();

        private string connectionString;

        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;

        public ClubRegistrationQuery()
        {
            connectionString = @"Data Source=(LocalDB)MSSQLLocalDB;AttachedFilename=D:\Visual Studio\Laboratory_Exercise1\ClubDB.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            string clubMembersView = "SELECT StudnetID, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";
            sqlAdapter = new SqlDataAdapter(clubMembersView, sqlConnect);
            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            return true;
        }

        public bool registerStudent(int ID, long StudentID, string FirstName, string MiddleName,  string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("INSERT INTO ClubMembers (ID, StudentID, FirstName, MiddleName, LastName, Age, Gender, Program) " +
                "VALUES (@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.NVarChar, 50).Value = Program;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }

        public bool updateStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("UPDATE ClubMembers SET StudentID=@StudentID, FirstName=@FirstName, MiddleName=@MiddleName," +
                " LastName=@LastName, Age=@Age, Gender=@Gender, Program=@Program WHERE ID=@ID", sqlConnect);
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.NVarChar, 50).Value = Program;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }
    }
}
