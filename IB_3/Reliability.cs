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
using System.IO;
using System.Diagnostics;

namespace IB_3
{
    public partial class Reliability : Form
    {
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";
        public char[] char_pass;
        public int pause = 0;
        public double s_pause = 0.001;
        public char[] alphabet =
        {
            'a','b','c', 'd', 'e', 'f', 'g', 'h', 'i','j','k', 'l', 'm', 'n', 'o', 'p', 'q','r','s', 't', 'u', 'v', 'w', 'x', 'y','z',
            'A','B','C', 'D', 'E', 'F', 'G', 'H', 'I','J','K', 'L', 'M', 'N', 'O', 'P', 'Q','R','S', 'T', 'U', 'V', 'W', 'X', 'Y','Z',
            ' ','0','1','2', '3', '4', '5', '6', '7', '8','9',
            '.',',','!', '?', ':', ';', '@', '#', '$','%','&', '*', '(', ')', '[',']'
        };
        public char[] dictionary =
        {
            'a','b','c', 'd', 'e', 'f', 'g', 'h', 'i','j','k', 'l', 'm', 'n', 'o', 'p', 'q','r','s', 't', 'u', 'v', 'w', 'x', 'y','z',
            'A','B','C', 'D', 'E', 'F', 'G', 'H', 'I','J','K', 'L', 'M', 'N', 'O', 'P', 'Q','R','S', 'T', 'U', 'V', 'W', 'X', 'Y','Z',
            ' ','0','1','2', '3', '4', '5', '6', '7', '8','9',
            '.',',','!', '?', ':', ';', '@', '#', '$','%','&', '*', '(', ')', '[',']'
        };
        public int safe = 0, s = 100, m = 3, v = 0;
        public Reliability()
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
        public Reliability(string name, int n)
        {
            comboBox1.Text = name;

            if (n == 0) { Perebor(); }
            if (n == 1) { Perebor_Words(); }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
             
        }

        private void Reliability_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0) { pause = 0; s_pause += 0; }
            if (listBox1.SelectedIndex == 1) { pause = 500; s_pause += 0.5; }
            if (listBox1.SelectedIndex == 2) { pause = 1000; s_pause += 1; }
            if (listBox1.SelectedIndex == 3) { pause = 1500; s_pause += 1.5; }
            if (listBox1.SelectedIndex == 4) { pause = 2000; s_pause += 2; }
            if (listBox1.SelectedIndex == 5) { pause = 2500; s_pause += 2.5; }
             
            if (radioButton2.Checked == true) { Perebor(); }
            if (radioButton1.Checked == true) { Perebor_Words(); }
        }
        public void Perebor()
        {
            textBox3.ResetText();
            dataSet1.ReadXml(filePath);
            DataTable dataTable = dataSet1.Tables["users"];

            int i = 0;
            object[] names;
            string password = "";
            string podbor = "";
            double[] speed = new double[password.Length];
            double s = 0;
            Stopwatch stopwatch = new Stopwatch();
            
            while (i < dataTable.Rows.Count)
            {
                names = dataTable.Rows[i].ItemArray;
                stopwatch.Start();
                if (comboBox1.Text == names[0].ToString())
                {
                    password = names[1].ToString();
                    speed = new double[password.Length];
                    
                    while (podbor != password)
                    {
                        password.ToCharArray(); 
                        for (int j = 0; j < password.Length; j++)
                        {
                            for (int k = 0; k < alphabet.Length; k++)
                            {
                                textBox6.Text = alphabet[k].ToString();
                                textBox6.Refresh();
                                if (alphabet[k] == password[j])
                                {
                                    speed[j] = Convert.ToDouble(((k + 1) * 1000) / (Convert.ToDouble(stopwatch.ElapsedMilliseconds)));
                                    podbor += alphabet[k];
                                    textBox3.Text += alphabet[k].ToString();
                                    textBox3.Refresh();
                                    break;
                                }
                                textBox6.ResetText();
                                System.Threading.Thread.Sleep(pause);
                            }
                        }
                    }
                    
                }
                i++;
            }

            for (int j = 0; j < speed.Length; j++)
            {
                s += Convert.ToDouble(speed[j]);
            }
             
            textBox2.Text = (Convert.ToDouble(s/speed.Length)).ToString();
            textBox2.Refresh();

            TimeSpan ts = stopwatch.Elapsed;
            textBox1.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            if (ts.Minutes < 1) { textBox9.Text = "Unsafe! Change your password"; }
            else { textBox9.Text = "Safe!"; }

            stopwatch.Stop();

            textBox4.Text = password.Length.ToString();
            textBox5.Text = N(password.ToCharArray());
            textBox7.Text = M(Convert.ToInt32(N(password.ToCharArray())), password.Length).ToString();

        }
        public void Perebor_Words()
        {
            textBox3.ResetText();
            dataSet1.ReadXml(filePath);
            DataTable dataTable = dataSet1.Tables["users"];

            int i = 0, k = 0;
            object[] names;
            Stopwatch stopwatch = new Stopwatch();
            double speed;

            string fileName = "E:\\3 курс\\ИБИЗИ\\IB_3\\dictionary_eng.txt";
            string password = "";
            string text = File.ReadAllText(fileName);
            string[] lines = text.Split();

            while (i < dataTable.Rows.Count)
            {
                names = dataTable.Rows[i].ItemArray;
                if (comboBox1.Text == names[0].ToString())
                {
                    password = names[1].ToString();
                    
                    stopwatch.Start();
                    foreach (string line in lines)
                    {
                        k += 1;
                        textBox6.Text = line;
                        textBox6.Refresh();
                        if (line.ToString() == password.ToString())
                        {
                            speed = (k * 1000)/(Convert.ToDouble(stopwatch.ElapsedMilliseconds));
                            textBox2.Text = speed.ToString();
                            textBox2.Refresh();
                            textBox3.Text = password;
                            textBox3.Refresh();
                            break;
                        }
                        textBox6.ResetText();
                        System.Threading.Thread.Sleep(pause);
                    }
                }
                i++;
            }

            TimeSpan ts = stopwatch.Elapsed;
            textBox1.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            if (ts.Minutes < 1) { textBox9.Text = "Unsafe! Change your password"; }
            else { textBox9.Text = "Safe!"; }

            stopwatch.Stop();

            textBox4.Text = password.Length.ToString();
            textBox5.Text = N(password.ToCharArray());
            textBox7.Text = M(Convert.ToInt32(N(password.ToCharArray())), password.Length).ToString();
        }
        static double M(int N, int l)
        {
            return Math.Pow(N, l);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Admin form3 = new Admin();
            form3.ShowDialog();
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
    }
}
