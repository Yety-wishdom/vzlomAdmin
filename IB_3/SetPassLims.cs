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
using System.Xml;


namespace IB_3
{
    public partial class SetPassLims : Form
    {
        public int N = 0;
        public int length = 0;
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";
        public SetPassLims()
        {
            InitializeComponent();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filePath);
            XmlElement xroot = xdoc.DocumentElement;
            if (xroot != null)
            {
                foreach (XmlElement xnode in xroot)
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "name")
                        {
                            comboBox1.Items.Add(childnode.InnerText);
                        }
                    }
                }
            }
        }
        public SetPassLims(string name)
        {
            InitializeComponent();
            comboBox1.Text = name;
            comboBox1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load(filePath);
            length = Convert.ToInt32(listBox1.SelectedItem.ToString());
            var T = xdoc.Element("users_table")?
                .Elements("users")
                .FirstOrDefault(p => p.Element("name")?.Value == comboBox1.Text);
            if (comboBox1.Text != null)
            {
                var limits = T.Element("password_limitations");
                if (limits != null) limits.Value = N.ToString();
                var len = T.Element("password_length");
                if (len != null) len.Value = length.ToString();

                xdoc.Save(filePath);
            }
            Hide();
            Admin admin = new Admin();
            admin.Show();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                N += 26;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.CheckState == CheckState.Checked)
            {
                N += 52;
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked)
            {
                N += 10;
            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                N += 33;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
