using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace sift.SocketApp
{
    public partial class ClientService : Form
    {
        public static Socket socketClient { get; set; }
        public static ClientService clientService { get; set; }

        public Thread reciveThead;

        public ClientService()
        {
            InitializeComponent();
            clientService = this;
        }

        private void OpenConnectBtn_Click(object sender, EventArgs e)
        {
            //创建实例
            socketClient = new Socket(SocketType.Stream, ProtocolType.Tcp);
            
            IPAddress ip = IPAddress.Parse(IpText.Text);
            IPEndPoint point = new IPEndPoint(ip, int.Parse(PortText.Text));
            //进行连接
            socketClient.Connect(point);

            //不停的接收服务器端发送的消息
            reciveThead = new Thread(Recive);
            reciveThead.IsBackground = true;
            reciveThead.Start();
        }

        static void Recive()
        {
            //  为什么用telnet客户端可以，但这个就不行。
            while (true)
            {
                //获取发送过来的消息
                byte[] buffer = new byte[1024 * 1024 * 2];
                int effective = socketClient.Receive(buffer);
                if (effective == 0)
                {
                    continue;
                }
                var str = Encoding.UTF8.GetString(buffer, 0, effective);
                updateMessageBox("来自服务器 --- " + str);
            }
        }

        private void SendMsgBtn_Click(object sender, EventArgs e)
        {
            var buffter = Encoding.UTF8.GetBytes(SendMsgBtnText.Text + "/r/n");
            int num = socketClient.Send(buffter);
            updateMessageBox("发送字节数 --- " + num);
        }


        private static void updateMessageBox(String msg)
        {
            clientService.MessageTxtBox.AppendText(msg + "\r\n");
        }

        private void CloseConnectBtn_Click(object sender, EventArgs e)
        {
            close();
        }
        private void ClientService_FormClosed(object sender, FormClosedEventArgs e)
        {
            close();
        }

        public void close() {
            if (socketClient != null) {
                socketClient.Close();
            }
            if (reciveThead != null) { 
                reciveThead.Abort(); 
            }
        }


    }
}
