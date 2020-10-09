using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnthonyLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        string ip = "127.0.0.1";
        string port = "30120";
        private void Form1_Load(object sender, EventArgs e)
        {
           
            try
            {            
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(Convert.ToString(ip), Convert.ToInt32(port));
                WebClient getsv1 = new WebClient();
                string svinfo = getsv1.DownloadString("http://" + Convert.ToString(ip) + ":" + Convert.ToString(port) + "/info.json");
                dynamic data = JsonConvert.DeserializeObject(svinfo);
                bunifuPictureBox1.Image = Base64ToImage(Convert.ToString(data.icon));          
                bunifuLabel3.Text = "Online";
                bunifuLabel3.ForeColor = Color.FromArgb(0, 153, 51);
                WebClient pinfo = new WebClient();
                string getpinfo = pinfo.DownloadString("http://" + Convert.ToString(ip) + ":" + Convert.ToString(port) + "/players.json");
                dynamic data2 = JsonConvert.DeserializeObject(getpinfo);
                var array = data2;
                var totalLength = 0;
                foreach (var s in array)
                {
                    totalLength++;
                }
                bunifuLabel5.Text = Convert.ToString(totalLength + " / " + data.vars.sv_maxClients);
                bunifuFlatButton2.Enabled = true;
                bunifuTransition1.Show(panel2);
            }
            catch (Exception)
            {
                bunifuTransition1.Show(panel2);
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(Convert.ToString(ip), Convert.ToInt32(port));
                WebClient getsv1 = new WebClient();
                string svinfo = getsv1.DownloadString("http://" + Convert.ToString(ip) + ":" + Convert.ToString(port) + "/info.json");
                dynamic data = JsonConvert.DeserializeObject(svinfo);
                bunifuPictureBox1.Image = Base64ToImage(Convert.ToString(data.icon));
                bunifuLabel3.Text = "Online";
                bunifuLabel3.ForeColor = Color.FromArgb(0, 153, 51);
                WebClient pinfo = new WebClient();
                string getpinfo = pinfo.DownloadString("http://" + Convert.ToString(ip) + ":" + Convert.ToString(port) + "/players.json");
                dynamic data2 = JsonConvert.DeserializeObject(getpinfo);           
                var array = data2;
                var totalLength = 0;
                foreach (var s in array)
                {
                    totalLength++;
                }
                bunifuLabel5.Text = Convert.ToString(totalLength + " / " + data.vars.sv_maxClients);
                bunifuFlatButton2.Enabled = true;
                bunifuTransition1.Show(panel2);
            }
            catch (Exception)
            {
                bunifuTransition1.Show(panel2);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
           
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\CitizenFX\\FiveM");
            string text = registryKey.GetValue("Last Run Location").ToString();
            int length = registryKey.GetValue("Last Run Location").ToString().Length;
            string text2 = text.Substring(0, length - 10) + "FiveM.exe";
            if (File.Exists(text2))
            {
               string cip = Convert.ToString(ip + ":" + port);
                Process.Start(text2, "+connect " + cip);
            }
            else
            {
                MessageBox.Show("I think you do not have Fivem!\nPlease go to fivem.net", ":|", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            Application.Exit();
        }
    }
}
