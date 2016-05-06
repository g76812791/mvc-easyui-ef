using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace KCollector.Test
{


    public partial class Form1 : Form
    {
        public readonly static string APIURL = "http://localhost:22479/add?w=";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("请输入模拟请求次数");
                return;
            }

            int count = 0;
            try
            {
                count = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字");
                return;
            }

            //Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        WebRequest myre = WebRequest.Create(APIURL + i);  

            //        //WebClient webcl = new WebClient();
            //        //webcl.OpenReadAsync(new Uri(APIURL+i));
            //    }
            //});

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < count; i++)
            {
                //WebRequest myre = WebRequest.Create(APIURL + i);

                WebClient webcl = new WebClient();

                Stream st = webcl.OpenRead(new Uri(APIURL + i));
                StreamReader sr = new StreamReader(st);
                string res = sr.ReadToEnd();
                sr.Close();
                st.Close();

            }
            sw.Stop();
            label3.Text = sw.Elapsed.Milliseconds.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        static long _sentCount = 0;
        bool iscomplate = false;
        static void SendMessages(int count)
        {
            var sendAction = default(Action<int>);
            sendAction = index =>
            {
                WebClient webcl = new WebClient();

                Stream st = webcl.OpenRead(new Uri(APIURL + new Random().Next()));
                StreamReader sr = new StreamReader(st);
                string res = sr.ReadToEnd();
                sr.Close();
                st.Close();
                Interlocked.Increment(ref _sentCount);
            };
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        sendAction(0);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
        }
        Task task;
        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                count = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字");
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var actions = new List<Action>();

            for (var i = 0; i < count; i++)
            {
                actions.Add(() => SendMessages(i));
                //actions.Add(new Action<int>(i)=>{SendMessages(i.ToString());});
            }

            task = Task.Factory.StartNew(() => Parallel.Invoke(actions.ToArray()));
           
            sw.Stop();
            label3.Text = sw.Elapsed.Milliseconds.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        DateTime dtnow = DateTime.Now;
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool bl = true;
            if (task == null)
                return;
            lbisc.Text = task.IsCompleted.ToString();
            int zxcount = 1;
            lbzxcount.Text = zxcount.ToString();
            bl = task.IsCompleted;
            lbisc.Text = bl.ToString();
            if (bl)
            {
                //task.Start();
                lbisc.Text = task.IsCompleted.ToString();
                zxcount = zxcount + 1;
                lbzxcount.Text = zxcount.ToString();
            }
            if ((DateTime.Now - dtnow).TotalMinutes > 120)
                timer1.Stop();

        }
    }
}
