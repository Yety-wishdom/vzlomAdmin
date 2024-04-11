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
    public partial class StartForm : Form
    {

        public bool btn1 = false;
        public bool btn2 = false;
        public int trys = 0;
        public bool user = false;
        public string filePath = "E:\\3 курс\\ИБИЗИ\\IB_3\\IB_3\\users.xml";

        public char[] alphabet =
        {
            'a','b','c', 'd', 'e', 'f', 'g', 'h', 'i','j','k', 'l', 'm', 'n', 'o', 'p', 'q','r','s', 't', 'u', 'v', 'w', 'x', 'y','z',
            'A','B','C', 'D', 'E', 'F', 'G', 'H', 'I','J','K', 'L', 'M', 'N', 'O', 'P', 'Q','R','S', 'T', 'U', 'V', 'W', 'X', 'Y','Z',
            ' ','0','1','2', '3', '4', '5', '6', '7', '8','9',
            '.',',','!', '?', ':', ';', '@', '#', '$','%','&', '*', '(', ')', '[',']'
        };


        public StartForm()
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = comboBox1.SelectedIndex;
            if (selected == 0)
            {
                comboBox1.Enabled = false;
                user = false;
            }
            else
            {
                comboBox1.Enabled = true;
                user = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataSet1.ReadXml(filePath);
            DataTable dataTable = dataSet1.Tables["users"];

            btn1 = true;
            int i = 0;

            object[] names;
            string name = "";
            string password = "";
            string podbor = "";
            string block = "";
            string pass_lims = "";
            string length = "";

            while (i < dataTable.Rows.Count)
            {
                names = dataTable.Rows[i].ItemArray;
                if (comboBox1.Text == names[0].ToString())
                {
                    name = names[0].ToString();
                    password = names[1].ToString();
                    block = names[2].ToString();
                    pass_lims = names[3].ToString();
                    length = names[4].ToString();
                }
                i++;
            }
            if (comboBox1.Text == "")
            {
                var result =
                    MessageBox.Show(
                        "Вы не указали имя юзера!",
                         "Ошибка",
                         MessageBoxButtons.YesNo
                    );

                comboBox1.ResetText();
                textBox1.ResetText();
                if (result == DialogResult.No)
                {
                    Close();
                }
            }
            else if (textBox1.Text == password && password != "")
            {
                if (block != "on" && pass_lims == "-")
                {
                    if (comboBox1.Text == "ADMIN")
                    {
                        Hide();
                        Admin form3 = new Admin(name, password); // форма для админа
                        form3.ShowDialog();
                    }
                    else
                    {
                        Hide();
                        UserForm form4 = new UserForm(name, password); // форма для юзера
                        form4.ShowDialog();
                    }
                }
                else if (block == "on")
                {
                    MessageBox.Show(
                         "Ваша работа заблокирована!",
                         "БАН!",
                         MessageBoxButtons.OK
                    );
                    comboBox1.ResetText();
                    textBox1.ResetText();
                }
                // password limits
                else if (pass_lims != "-")
                {
                    var result2 =
                        MessageBox.Show(
                             "Вам нужно сменить пароль",
                             "В связи с ограничениями",
                             MessageBoxButtons.YesNoCancel
                        );
                    if (result2 == DialogResult.Yes)
                    {
                        Hide();
                        PassLimits limits = new PassLimits(name, password, pass_lims, length);
                        limits.Show();
                    }
                    else if (result2 == DialogResult.No)
                    {
                        Hide();
                        UserForm userForm = new UserForm(name, password);
                        userForm.Show();
                    }
                    else if (result2 == DialogResult.Cancel)
                    {
                        Close();
                    }
                }
            }
            else if (password == "" && pass_lims == "-")
            {
                var result1 = MessageBox.Show(
                         "Ваш пароль не установлен. Установите пароль!",
                         "Установить новый пароль",
                         MessageBoxButtons.YesNoCancel
                );
                if (result1 == DialogResult.Yes)
                {
                    Hide();
                    PasswordForNew form = new PasswordForNew(name);
                    form.Show();
                }
                else if (result1 == DialogResult.Cancel)
                {
                    Close();
                }
                else if (result1 == DialogResult.No)
                {
                    if (comboBox1.Text == "ADMIN")
                    {
                        Hide();
                        Admin admin = new Admin(name, password);
                        admin.Show();
                    }
                    else
                    {
                        Hide();
                        UserForm user = new UserForm(name, password);
                        user.Show();
                    }
                }
            }

            else if (password == "" && pass_lims != "-" && block != "on")
            {
                var result2 =
                        MessageBox.Show(
                             "Вам нужно сменить пароль",
                             "В связи с ограничениями",
                             MessageBoxButtons.YesNoCancel
                        );
                if (result2 == DialogResult.Yes)
                {
                    Hide();
                    PassLimits limits = new PassLimits(name, password, pass_lims, length);
                    limits.Show();
                }
                else if (result2 == DialogResult.No)
                {
                    Hide();

                    if (comboBox1.Text == "ADMIN")
                    {
                        Hide();
                        Admin form3 = new Admin(name, password); // форма для админа
                        form3.ShowDialog();
                    }
                    else
                    {
                        Hide();
                        UserForm form4 = new UserForm(name, password); // форма для юзера
                        form4.ShowDialog();
                    }
                }
                else if (result2 == DialogResult.Cancel)
                {
                    Close();
                }
            }
            else if (password == "" && pass_lims != "-" && block == "on")
            {
                MessageBox.Show(
                         "Ваша работа заблокирована!",
                         "БАН!",
                         MessageBoxButtons.OK
                    );
                comboBox1.ResetText();
                textBox1.ResetText();
            }

            else if (trys < 2)
            {
                MessageBox.Show(
                         "Повторите ввод!",
                         "Пароль не верный",
                         MessageBoxButtons.RetryCancel
                );
                textBox1.Clear();
                textBox1.Focus();
                trys++;
            }
            else if (trys == 2)
            {
                MessageBox.Show(
                    "Закончились попытки",
                    "Пароль не верный"
                );
                Hide();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Perebor_Words();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Perebor();
        }
        public void Perebor_Words()
        {
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
                        textBox1.Text = line;
                        textBox1.Refresh();
                        if (line.ToString() == password.ToString())
                        {
                            textBox1.Text = password;
                            textBox1.Refresh();
                            button1.BackColor = Color.LightGreen;
                            button1.Refresh();
                            Admin form = new Admin(comboBox1.Text, textBox1.Text);
                            form.Show();
                            break;
                        }
                        //MessageBox.Show("Неправильный пароль");
                        
                        System.Threading.Thread.Sleep(50);
                    }
                }
                i++;
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            textBox2.Text = String.Format("{0:00}: {1:00}: {2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            textBox2.Refresh();
            speed = (Convert.ToDouble(k / (ts.Minutes * 60 + ts.Seconds + ts.Milliseconds / 1000)));
            textBox3.Text = speed.ToString();
            textBox3.Refresh();
        }
        public void Perebor()
        {
            dataSet1.ReadXml(filePath);
            DataTable dataTable = dataSet1.Tables["users"];

            int i = 0, s = 0, two_symbols = 0, three_symbols = 0, four_symbols = 0, five_symbols = 0, six_symbols = 0;
            object[] names;
            string password = "", podbor = "";
            double speed;
            Stopwatch stopwatch = new Stopwatch();
            
            while (i < dataTable.Rows.Count)
            {
                names = dataTable.Rows[i].ItemArray;
                stopwatch.Start();
                if (comboBox1.Text == names[0].ToString())
                {
                    password = names[1].ToString();
                    stopwatch.Start();

                    for (int j = 0; j < 8; j++)
                    {
                        for (int k = 0; k < alphabet.Length; k++)
                        {
                            if (j == 0)
                            {
                                podbor = Convert.ToString(alphabet[k]);
                                textBox1.Text = podbor;
                                textBox1.Refresh();
                                s++;
                                System.Threading.Thread.Sleep(50);
                                if (podbor == password) { break; }
                            }
                            else if (j == 1)
                            {
                                for (two_symbols = 0; two_symbols < alphabet.Length; two_symbols++)
                                {
                                    for (k = 0; k < alphabet.Length; k++)
                                    {
                                        podbor = Convert.ToString(alphabet[two_symbols].ToString() + alphabet[k].ToString());
                                        textBox1.Text = podbor;
                                        textBox1.Refresh();
                                        s++;
                                        System.Threading.Thread.Sleep(50);
                                        if (podbor == password) { break; }
                                    } if (podbor == password) { break; }
                                } 
                                if (podbor == password) 
                                {
                                    Admin form = new Admin(comboBox1.Text, textBox1.Text);
                                    form.Show();
                                    break;
                                }
                            }
                            else if (j == 2)
                            {
                                for (three_symbols = 0; three_symbols < alphabet.Length; three_symbols++)
                                {
                                    for (two_symbols = 0; two_symbols < alphabet.Length; two_symbols++)
                                    {
                                        for (k = 0; k < alphabet.Length; k++)
                                        {
                                            podbor = Convert.ToString(alphabet[three_symbols].ToString() + alphabet[two_symbols].ToString() + alphabet[k].ToString());
                                            textBox1.Text = podbor;
                                            textBox1.Refresh();
                                            s++;
                                            System.Threading.Thread.Sleep(50);
                                            if (podbor == password) { break; }
                                        } if (podbor == password) { break; }
                                    } if (podbor == password) { break; }
                                }
                                if (podbor == password)
                                {
                                    Admin form = new Admin(comboBox1.Text, textBox1.Text);
                                    form.Show();
                                    break;
                                }
                            }
                            else if (j == 3)
                            {
                                for (four_symbols = 0; four_symbols < alphabet.Length; four_symbols++)
                                {
                                    for (three_symbols = 0; three_symbols < alphabet.Length; three_symbols++)
                                    {
                                        for (two_symbols = 0; two_symbols < alphabet.Length; two_symbols++)
                                        {
                                            for (k = 0; k < alphabet.Length; k++)
                                            {
                                                podbor = Convert.ToString(alphabet[four_symbols].ToString() + alphabet[three_symbols].ToString() + alphabet[two_symbols].ToString() + alphabet[k].ToString());
                                                textBox1.Text = podbor;
                                                textBox1.Refresh();
                                                s++;
                                                System.Threading.Thread.Sleep(50);
                                                if (podbor == password) { break; }
                                            } if (podbor == password) { break; }
                                        } if (podbor == password) { break; }
                                    } if (podbor == password) { break; }
                                }
                                if (podbor == password)
                                {
                                    Admin form = new Admin(comboBox1.Text, textBox1.Text);
                                    form.Show();
                                    break;
                                }
                            }
                            else if (j == 4)
                            {
                                for (five_symbols = 0; five_symbols < alphabet.Length; five_symbols++)
                                {
                                    for (four_symbols = 0; four_symbols < alphabet.Length; four_symbols++)
                                    {
                                        for (three_symbols = 0; three_symbols < alphabet.Length; three_symbols++)
                                        {
                                            for (two_symbols = 0; two_symbols < alphabet.Length; two_symbols++)
                                            {
                                                for (k = 0; k < alphabet.Length; k++)
                                                {
                                                    podbor = Convert.ToString(alphabet[five_symbols].ToString() + alphabet[four_symbols].ToString() + alphabet[three_symbols].ToString() + alphabet[two_symbols].ToString() + alphabet[k].ToString());
                                                    textBox1.Text = podbor;
                                                    textBox1.Refresh();
                                                    s++;
                                                    System.Threading.Thread.Sleep(50);
                                                    if (podbor == password) { break; }
                                                } if (podbor == password) { break; }
                                            } if (podbor == password) { break; }
                                        } if (podbor == password) { break; }
                                    } if (podbor == password) { break; }
                                }
                                if (podbor == password)
                                {
                                    Admin form = new Admin(comboBox1.Text, textBox1.Text);
                                    form.Show();
                                    break;
                                }
                            }
                            else if (j == 5)
                            {
                                for (six_symbols = 0; six_symbols < alphabet.Length; six_symbols++)
                                {
                                    for (five_symbols = 0; five_symbols < alphabet.Length; five_symbols++)
                                    {
                                        for (four_symbols = 0; four_symbols < alphabet.Length; four_symbols++)
                                        {
                                            for (three_symbols = 0; three_symbols < alphabet.Length; three_symbols++)
                                            {
                                                for (two_symbols = 0; two_symbols < alphabet.Length; two_symbols++)
                                                {
                                                    for (k = 0; k < alphabet.Length; k++)
                                                    {
                                                        podbor = Convert.ToString(alphabet[six_symbols].ToString() + alphabet[five_symbols].ToString() + alphabet[four_symbols].ToString() + alphabet[three_symbols].ToString() + alphabet[two_symbols].ToString() + alphabet[k].ToString());
                                                        textBox1.Text = podbor;
                                                        textBox1.Refresh();
                                                        s++;
                                                        System.Threading.Thread.Sleep(50);
                                                        if (podbor == password) { break; }
                                                    } if (podbor == password) { break; }
                                                } if (podbor == password) { break; }
                                            } if (podbor == password) { break; }
                                        } if (podbor == password) { break; }
                                    } if (podbor == password) { break; }
                                }
                                if (podbor == password)
                                {
                                    Admin form = new Admin(comboBox1.Text, textBox1.Text);
                                    form.Show();
                                    break;
                                }
                            }
                            System.Threading.Thread.Sleep(50);
                            if (podbor == password) { break; }
                        }
                        if (podbor == password)
                        {
                            button1.BackColor = Color.LightGreen;
                            button1.Refresh();
                            break;
                        }
                    }
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    textBox2.Text = String.Format("{0:00}: {1:00}: {2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    textBox2.Refresh();
                    speed = (Convert.ToDouble(s / (ts.Minutes * 60 + ts.Seconds + ts.Milliseconds / 1000)));
                    textBox3.Text = speed.ToString();
                    textBox3.Refresh();
                }
                i++;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


