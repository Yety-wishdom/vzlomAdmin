using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IB_3
{
    public partial class Admin : Form
    {
        public string name;
        public string password;
        public Admin(string name, string password)
        {
            InitializeComponent();
            this.name = name;
            this.password = password;
        }
        public Admin()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";

            usersSet1.ReadXml(filePath);

            dataGridView1.DataSource = usersSet1;
            dataGridView1.DataMember = "users";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            PasswordChange form3 = new PasswordChange(name, password); 
            form3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";
            usersSet1.WriteXml(filePath);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            StartForm form3 = new StartForm();
            form3.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            AddNewUser form = new AddNewUser();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            SetPassLims form = new SetPassLims();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            Blocking form = new Blocking();
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Hide();
            Reliability form = new Reliability();
            form.Show();
        }
    }
}
