using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Policy;

namespace sift.SocketApp
{
    public partial class ServerService : Form
    {
        private static AppServer appServer { get; set; }
        private static ServerService serverService { get; set; }

        public ServerService()
        {
            InitializeComponent();
            serverService = this;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            appServer = new AppServer();

            //Setup the appServer
            int listenPort = int.Parse(listenPortText.Text);
            if (!appServer.Setup(listenPort)) //Setup with listening port
            {
                updateMessageBox("Failed to setup!");
                return;
            }

            //Try to start the appServer
            if (!appServer.Start())
            {
                updateMessageBox("Failed to start!");
                return;
            }


            updateMessageBox("The server started successfully");

            //1.
            appServer.NewSessionConnected += new SessionHandler<AppSession>(appServer_NewSessionConnected);
            appServer.SessionClosed += appServer_NewSessionClosed;

            //2.
            appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRequestReceived);
        }

        //1.
        static void appServer_NewSessionConnected(AppSession session)
        {
            updateMessageBox("服务端得到来自客户端的连接成功");
            var count = appServer.GetAllSessions().Count();
            updateMessageBox("~~" + count);

            session.Send("Welcome to SuperSocket Telnet Server");
        }

        static void appServer_NewSessionClosed(AppSession session, SuperSocket.SocketBase.CloseReason aaa)
        {
            updateMessageBox("服务端 失去 来自客户端的连接" + session.SessionID + aaa.ToString());
            int count = appServer.GetAllSessions().Count();
            updateMessageBox(count.ToString());
        }

        //2.
        static void appServer_NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
            updateMessageBox(requestInfo.Key);
            session.Send(requestInfo.Body);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            appServer.Stop();
            updateMessageBox("The server was stopped!");
        }

        private void ServerService_FormClosed(object sender, FormClosedEventArgs e)
        {
            appServer.Stop();
        }

        private static void updateMessageBox(String msg) {
            serverService.MessageTxtBox.AppendText(msg + "\r\n");
        }
    }
}
