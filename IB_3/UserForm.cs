using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB_3
{
    public partial class UserForm : Form
    {
        
        public bool btn1 = false;
        public bool btn2 = false;
        public int trys = 0;
        public bool user = false;
        public string name;
        public string password;
        public UserForm(string name, string password)
        {
            InitializeComponent();
            this.name = name;
            this.password = password;
            label1.Text = name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btn2 = true;
            if (btn2 == true)
            {
                Hide();
                PasswordChange form2 = new PasswordChange(name, password);
                form2.ShowDialog();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new StartForm();
            form.Show();
        }
    }
}
