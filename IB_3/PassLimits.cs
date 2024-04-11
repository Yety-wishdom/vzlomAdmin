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
    
    public partial class PassLimits : Form
    {
        public string name;
        public string password;
        public string pass_lims;
        public int length;
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";

        public PassLimits(string name, string password, string pass_lims, string length)
        {
            InitializeComponent();
            this.password = password;
            this.pass_lims = pass_lims;
            this.length = Convert.ToInt32(length);
            label1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load(filePath);
            char[] char_pass = textBox1.Text.ToCharArray(); 
            
            if (textBox2.Text == password && N(char_pass) == pass_lims && char_pass.Length == length && textBox1.Text == textBox3.Text)
            {
                var tom = xdoc.Element("users_table")?
                .Elements("users")
                .FirstOrDefault(p => p.Element("name")?.Value == label1.Text);

                if (label1.Text != null)
                {
                    var pass = tom.Element("password");
                    if (pass != null) pass.Value = textBox1.Text.ToString();

                    var pass_l = tom.Element("password_limitations");
                    if (pass_l != null) pass_l.Value = "-".ToString();

                    var pass_len = tom.Element("password_length");
                    if (pass_len != null) pass_len.Value = "-".ToString();

                    xdoc.Save(filePath);
                }

                Hide();
                StartForm form = new StartForm();
                form.Show();
            }
            else if (textBox2.Text != password && N(char_pass) != pass_lims || char_pass.Length != 8 || textBox2.Text != textBox3.Text)
            {
                MessageBox.Show(
                         "Your password doesn't satisfy limits! Try again",
                         "Error",
                         MessageBoxButtons.OK
                    );
                textBox1.ResetText();
                textBox2.ResetText();
                textBox3.ResetText();
            }
        }
        static string N(char[] pass)
        {
            int low = IsLowLetter(pass), up = IsUpLetter(pass), digit = IsDigit(pass), punct = IsPunctuation(pass);
            string N = "";
            
            if (digit == pass.Length)
            {
                N = "10";
            }
            else if (up == pass.Length || low == pass.Length)
            {
                N = "26";
            }
            else if (punct == pass.Length)
            {
                N = "33";
            }
            else if (up + low == pass.Length)
            {
                N = "52";
            }
            else if (up + digit == pass.Length || low + digit == pass.Length)
            {
                N = "36";
            }
            else if (up + low + digit == pass.Length)
            {
                N = "62";
            }
            else if (digit + punct == pass.Length)
            {
                N = "43";
            }
            else if (up + punct == pass.Length || low + punct == pass.Length)
            {
                N = "59";
            }
            else if (up + digit + punct == pass.Length || low + digit + punct == pass.Length)
            {
                N = "69";
            }
            else if (up + low + punct == pass.Length)
            {
                N = "85";
            }
            else if (up + low + digit + punct == pass.Length)
            {
                N = "95";
            }
            return N;
        }

        static int IsUpLetter(char[] pass)
        {
            int up = 0;
            foreach (char c in pass)
            {
                if (char.IsLetter(c) && char.IsUpper(c))
                {
                    up++;
                }
            }
            return up;
        }

        static int IsLowLetter(char[] pass)
        {
            int low = 0;
            foreach (char c in pass)
            {
                if (char.IsLetter(c) && char.IsLower(c))
                {
                    low++;
                }
            }
            return low;
        }

        static int IsDigit(char[] pass)
        {
            int digit = 0;
            foreach (char c in pass)
            {
                if (char.IsDigit(c))
                {
                    digit++;
                }
            }
            return digit;
        }
        static int IsPunctuation(char[] pass)
        {
            int punct = 0;
            foreach (char c in pass)
            {
                if (char.IsPunctuation(c) || char.IsSymbol(c))
                {
                    punct++;
                }
            }
            return punct;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
