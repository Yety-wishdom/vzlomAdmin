using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace IB_3
{
    public partial class Blocking : Form
    {
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";
        public Blocking()
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

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load(filePath);

            var T = xdoc.Element("users_table")?
                .Elements("users")
                .FirstOrDefault(p => p.Element("name")?.Value == comboBox1.Text);

            if (comboBox1.Text != null)
            {
                var block = T.Element("blocking");
                if (block != null && checkBox1.Checked == true) block.Value = "on";
                if (block != null && checkBox1.Checked == false) block.Value = "off";
                xdoc.Save(filePath);
            }

            Hide();
            Admin admin = new Admin();
            admin.Show();
        }
        private void button2exit(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Admin form3 = new Admin();
            form3.ShowDialog();
        }
    }
}
