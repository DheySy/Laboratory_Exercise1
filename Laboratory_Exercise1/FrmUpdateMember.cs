using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Exercise1
{
    public partial class FrmUpdateMember : Form
    {
        private int memberID;
        private ClubRegistrationQuery query = new ClubRegistrationQuery();
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;
        private SqlConnection sqlConnect ;

        private string connectionString =
    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Public\Documents\Visual Studio\repos\Laboratory_Exercise1\ClubDB.mdf;Integrated Security=True;";

        public FrmUpdateMember(int id)
        {
            InitializeComponent();
            memberID = id;
        }

        private void LoadStudentIDs()
        {
            cbStudNum.Items.Clear();
            sqlConnect.Open();
            sqlCommand = new SqlCommand("SELECT StudentID FROM ClubMembers", sqlConnect);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                cbStudNum.Items.Add(sqlDataReader["StudentID"].ToString());
            }
            sqlDataReader.Close();
            sqlConnect.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            cmbProgram.Items.AddRange(new string[] {
                "BS Information Technology",
                "BS Computer Science",
                "BS Psychology",
                "BS Business Administration",
                "BS Computer Engineering",
                "BS Tourism Management",
                "BS Education",
                "BS Accountancy"
            });

            cmbGender.Items.AddRange(new string[] {
                "Male",
                "Female"
            });

            sqlConnect = new SqlConnection(connectionString);
            LoadStudentIDs();
            query.DisplayList();
            DataTable dt = query.dataTable;
            DataRow[] rows = dt.Select("ID = " + memberID);
            if (rows.Length > 0)
            {
                DataRow row = rows[0];
                cbStudNum.Text = row["StudentID"].ToString();
                txtFirstName.Text = row["FirstName"].ToString();
                txtMiddleName.Text = row["MiddleName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtAge.Text = row["Age"].ToString();
                cmbGender.SelectedItem = row["Gender"].ToString();
                var programValue = row["Program"]?.ToString()?.Trim();
                int idx = cmbProgram.Items.IndexOf(programValue);
                if (idx >= 0)
                    cmbProgram.SelectedIndex = idx;
                else
                    cmbProgram.Text = programValue;
            }

        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = memberID;

                if (!long.TryParse(cbStudNum.Text?.Trim(), out long studentID))
                {
                    MessageBox.Show("Invalid student number.");
                    return;
                }

                string firstName = txtFirstName.Text?.Trim() ?? string.Empty;
                string middleName = txtMiddleName.Text?.Trim() ?? string.Empty;
                string lastName = txtLastName.Text?.Trim() ?? string.Empty;

                if (!int.TryParse(txtAge.Text?.Trim(), out int age))
                {
                    MessageBox.Show("Invalid age.");
                    return;
                }

                
                string gender = (cmbGender.SelectedItem?.ToString())?.Trim() ?? cmbGender.Text?.Trim() ?? string.Empty;
                string prog = (cmbProgram.SelectedItem?.ToString())?.Trim() ?? cmbProgram.Text?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(prog))
                {
                    MessageBox.Show("Please select or enter a program.");
                    return;
                }

                query.updateStudent(ID, studentID, firstName, middleName, lastName, age, gender, prog);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
