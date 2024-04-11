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
    public partial class PasswordForNew : Form
    {
        public string name;
        public string password;
        public string pass_lims;
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";
        public PasswordForNew(string name)
        {
            InitializeComponent();
            this.name = name;
            label1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load(filePath);

            if (textBox1.Text == textBox2.Text)
            {
                var tom = xdoc.Element("users_table")?
                .Elements("users")
                .FirstOrDefault(p => p.Element("name")?.Value == name);

                if (name != null)
                {
                    var pass = tom.Element("password");
                    if (pass != null) pass.Value = textBox1.Text;

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
