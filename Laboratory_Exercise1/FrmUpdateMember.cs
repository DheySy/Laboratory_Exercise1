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

        public FrmUpdateMember(int id)
        {
            InitializeComponent();
            memberID = id;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
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
                cmbProgram.SelectedItem = row["Program"].ToString();
            }

        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = memberID;
                long studentID = long.Parse(cbStudNum.Text);
                string firstName = txtFirstName.Text;
                string middleName = txtMiddleName.Text;
                string lastName = txtLastName.Text;
                int age = int.Parse(txtAge.Text);
                string Gender = cmbGender.SelectedItem.ToString();
                string prog = cmbProgram.SelectedItem.ToString();

                query.updateStudent(ID, studentID, firstName, middleName, lastName, age, Gender, prog);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
