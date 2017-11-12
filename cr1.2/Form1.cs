using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace cr1._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Dictionary<char, double> dict1 = new Dictionary<char, double>();
        Dictionary<char, double> dict2 = new Dictionary<char, double>();        
        public string alfavit = "1234567890";
        static StreamReader sr = new StreamReader("text.txt");
        static string text = sr.ReadToEnd();

        private void button1_Click(object sender, EventArgs e)
        {
            FreqOne(text, alfavit);

        }
        public void FreqOne(string text, string alfavit)
        {
            char lowCh;
            foreach (char ch in text)
            {
                lowCh = char.ToLower(ch);
                for (int i = 0; i < alfavit.Length; i++)
                {
                    if (dict1.ContainsKey(lowCh))
                        dict1[lowCh]++;
                    else
                        if (ch == alfavit[i])
                            dict1.Add(lowCh, 1);
                }
            }

            foreach (var item in dict1)
                for (int i = 0; i < alfavit.Length; i++)
                    if (item.Key == alfavit[i])
                        listView1.Items.Add(new ListViewItem(new string[] { item.Key.ToString(), (item.Value / text.Length).ToString() }));
        }

        public void FreqTwo(string text, string alfavit)
        {
            int count = richTextBox1.Text.Length;
            listView2.Items.Clear();
            char lowCh;
            foreach (char ch in text)
            {
                lowCh = char.ToLower(ch);
                for (int i = 0; i < alfavit.Length; i++)
                {
                    if (dict2.ContainsKey(lowCh))
                        dict2[lowCh]++;
                    else
                        if (ch == alfavit[i])
                        dict2.Add(lowCh, 1);
                }
            }

            foreach (var item in dict2)
                for (int i = 0; i < alfavit.Length; i++)
                    if (item.Key == alfavit[i])
                        listView2.Items.Add(new ListViewItem(new string[] { item.Key.ToString(), (item.Value / count).ToString() }));
        }        

        public void CryptoAnalysis(Dictionary<char, double> dict1, Dictionary<char, double> dict2)
        {
            double value1 = dict1.Values.Max();
            double value2 = dict2.Values.Max();
            char a, b;
            int index1 = 0;
            int index2 = 0;
            foreach (var item1 in dict1)
                if (item1.Value == value1)
                {
                    a = item1.Key;
                    index1 = alfavit.IndexOf(a);
                }


            foreach (var item2 in dict2)
                if (item2.Value == value2)
                {
                    b = item2.Key;
                    index2 = alfavit.IndexOf(b);
                }

            comboBox2.Text = ((index2 - index1 + alfavit.Length) % alfavit.Length).ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {

            int key = Convert.ToInt32(comboBox1.Text);
            StreamReader sr = new StreamReader("text2.txt");
            string nums = sr.ReadToEnd();
            string enc = "";
            for (int i = 0; i < nums.Length; i++)
            {
                bool changed = false;
                for (int j = 0; j < alfavit.Length; j++)
                {
                    if (nums[i] == alfavit[j])
                    {
                        int temp = (j + key) % alfavit.Length;
                        enc = enc + alfavit[temp];
                        changed = true;
                        break;
                    }
                }
                if (!changed)
                    enc = enc + nums[i];
            }            
            richTextBox1.Text = enc;
            FreqTwo(richTextBox1.Text, alfavit);
            CryptoAnalysis(dict1, dict2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int key = Convert.ToInt32(comboBox2.Text);
            string nums = richTextBox1.Text;
            string enc = "";
             for (int i = 0; i < nums.Length; i++)
            {
                bool changed = false;
                for (int j = 0; j < alfavit.Length; j++)
                {
                    if (nums[i] == alfavit[j])
                    {
                        int temp = (j - key + alfavit.Length) % alfavit.Length;
                        enc = enc + alfavit[temp];
                        changed = true;
                        break;
                    }
                }
                if (!changed)
                    enc = enc + nums[i];
            }                        
            textBox1.Text = enc;                       
        }
    }
}