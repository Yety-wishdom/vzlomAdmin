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
    public partial class AddNewUser : Form
    {
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";
        public AddNewUser()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string password = "";
            XDocument xdoc = XDocument.Load(filePath);
            XElement root = xdoc.Element("users_table");
            if (root != null)
            {
                root.Add(new XElement("users",
                            new XElement("name", name),
                            new XElement("password", password),
                            new XElement("blocking", "off"),
                            new XElement("password_limitations", "-"),
                            new XElement("password_length", "-")));
                xdoc.Save(filePath);
            }  
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            Hide();
            SetPassLims set = new SetPassLims(name);
            set.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Admin form = new Admin();
            form.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
