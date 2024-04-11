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
    public partial class PasswordChange : Form
    {
        public string name;
        public string password;
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";
        public PasswordChange(string name, string password)
        {
            InitializeComponent();
            this.name = name;
            this.password = password;
            label1.Text = name;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load(filePath);
            if (textBox2.Text == password && (textBox1.Text == textBox3.Text))
            {
                var tom = xdoc.Element("users_table")?
                .Elements("users")
                .FirstOrDefault(p => p.Element("name")?.Value == label1.Text);

                if (label1.Text != null)
                {
                    var pass = tom.Element("password");
                    if (pass != null) pass.Value = textBox1.Text;

                    var plim = tom.Element("password_limitations");
                    if (plim != null) plim.Value = "-";

                    var plen = tom.Element("password_length");
                    if (plen != null) plen.Value = "-";

                    xdoc.Save(filePath);
                }
                Hide();
                StartForm form = new StartForm();
                form.Show();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
