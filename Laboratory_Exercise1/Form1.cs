using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Exercise1
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery = new ClubRegistrationQuery();
        private int count = 0;

        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
            cbPrograms.Items.AddRange(new string[] {
                "BS Information Technology",
                "BS Computer Science",
                "BS Psychology",
                "BS Business Administration",
                "BS Computer Engineering",
                "BS Tourism Management",
                "BS Education",
                "BS Accountancy"
            });

            cbGender.Items.AddRange(new string[] {
                "Male",
                "Female"
            });
        }

        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }

        private int RegistrationID()
        {
            count += 1;
            return count;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = RegistrationID();
                long studNo = Convert.ToInt64(txtStudentNo.Text);
                string firstN = txtFirstName.Text;
                string middleN = txtMiddleInitial.Text;
                string lastN = txtLastName.Text;
                int age = Convert.ToInt32(txtAge.Text);
                string gender = cbGender.SelectedItem.ToString();
                string program = cbPrograms.SelectedItem.ToString();

                clubRegistrationQuery.registerStudent(ID, studNo, firstN, middleN, lastN, age, gender, program);
                RefreshListOfClubMembers();
                ClearInputs();
            } 
            catch (Exception ex) 
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                FrmUpdateMember updateForm = new FrmUpdateMember(selectedID);
                updateForm.FormClosed += UpdateForm_FormClosed;
                updateForm.Show();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void UpdateForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshListOfClubMembers(); 
        }

        private void ClearInputs()
        {
            txtStudentNo.Clear();
            txtFirstName.Clear();
            txtMiddleInitial.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cbGender.SelectedIndex = -1;
            cbPrograms.SelectedIndex = -1;
        }
    }
}
