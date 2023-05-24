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
using Emgu.CV.Util;
using System.IO;
using System.Runtime.InteropServices;

namespace sift.SocketApp
{
    public partial class ClientService : Form
    {
        public static Socket socketClient { get; set; }
        public static ClientService clientService { get; set; }

        // 接收文件
        public Thread reciveThead;
        private static bool isReceivingFile = false;
        private static FileStream fileStream;
        private static string fileName = null;
        private static int msgTypeNum = 4;
        private static int msgNum = 1024 * 8;

        public ClientService()
        {
            InitializeComponent();
            clientService = this;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void OpenConnectBtn_Click(object sender, EventArgs e)
        {
            //创建实例
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse(IpText.Text);
            IPEndPoint point = new IPEndPoint(ip, int.Parse(PortText.Text));
            //进行连接
            socketClient.Connect(point);
            // 将超时值设置为零，表示不使用超时
            socketClient.ReceiveTimeout = 0;
            socketClient.ReceiveBufferSize = 1024 * 1024;
            // 将 Socket 设置为阻塞模式
            socketClient.Blocking = true;

            //不停的接收服务器端发送的消息
            reciveThead = new Thread(Recive);
            reciveThead.IsBackground = true;
            reciveThead.Start();
        }

        static void Recive()
        {
            while (true)
            {
                //获取发送过来的消息
                byte[] buffer = new byte[msgNum];
                int effective = socketClient.Receive(buffer);

                if (isReceivingFile)
                {
                    string str = Encoding.UTF8.GetString(buffer, 0, msgTypeNum);
                    // 判断是否文件传输结束
                    if ("F002".Equals(str))
                    {
                        fileStream.Write(buffer, msgTypeNum, effective - msgTypeNum);
                        fileStream.Flush();
                        fileStream.Close();
                        isReceivingFile = false;
                        continue;
                    }
                    // 判断是否收到指定大小的数据
                    while (effective < msgNum)
                    {
                        int bytes = socketClient.Receive(buffer, effective, msgNum - effective, SocketFlags.None);
                        effective += bytes;
                        if (effective == msgNum)
                        {
                            break;
                        }
                    }
                    if ("F001".Equals(str))
                    {
                        fileStream.Write(buffer, msgTypeNum, effective - msgTypeNum);
                    }
                    updateMessageBox("来自服务器 --- " + str);
                }
                else {
                    string str = Encoding.UTF8.GetString(buffer, 0, effective);
                    updateMessageBox("来自服务器 --- " + str);
                }
 
            }
        }

        private void SendMsgBtn_Click(object sender, EventArgs e)
        {
            byte[] buffter = Encoding.UTF8.GetBytes(SendMsgBtnText.Text);
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

        private void recvFile_Click(object sender, EventArgs e)
        {
            if (isReceivingFile) {
                Console.WriteLine("目前还在接收文件中");
                return;
            }
                
            byte[] buffter = Encoding.UTF8.GetBytes("F000");
            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }
            fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            socketClient.Send(buffter);
            isReceivingFile = true;
        }

        private void ClientService_Load(object sender, EventArgs e)
        {
            IpText.Text = "192.168.1.106";
            fileName = "test.ply";
        }
    }
}
