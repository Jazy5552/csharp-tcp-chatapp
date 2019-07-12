/*
 * Created by SharpDevelop.
 * User: Jayan Nair
 * Date: 7/13/2004
 * Time: 2:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace DefaultNamespace
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Forms;

    /// <summary>
    /// Description of SocketServer.
    /// </summary>
    public class SocketServer : System.Windows.Forms.Form
    {
        /// <summary>
        /// Defines the label3
        /// </summary>
        private System.Windows.Forms.Label label3;

        /// <summary>
        /// Defines the label2
        /// </summary>
        private System.Windows.Forms.Label label2;

        /// <summary>
        /// Defines the richTextBoxReceivedMsg
        /// </summary>
        private System.Windows.Forms.RichTextBox richTextBoxReceivedMsg;

        /// <summary>
        /// Defines the textBoxPort
        /// </summary>
        private System.Windows.Forms.TextBox textBoxPort;

        /// <summary>
        /// Defines the label5
        /// </summary>
        private System.Windows.Forms.Label label5;

        /// <summary>
        /// Defines the label4
        /// </summary>
        private System.Windows.Forms.Label label4;

        /// <summary>
        /// Defines the textBoxMsg
        /// </summary>
        private System.Windows.Forms.TextBox textBoxMsg;

        /// <summary>
        /// Defines the buttonStopListen
        /// </summary>
        private System.Windows.Forms.Button buttonStopListen;

        /// <summary>
        /// Defines the label1
        /// </summary>
        private System.Windows.Forms.Label label1;

        /// <summary>
        /// Defines the richTextBoxSendMsg
        /// </summary>
        private System.Windows.Forms.RichTextBox richTextBoxSendMsg;

        /// <summary>
        /// Defines the textBoxIP
        /// </summary>
        private System.Windows.Forms.TextBox textBoxIP;

        /// <summary>
        /// Defines the buttonStartListen
        /// </summary>
        private System.Windows.Forms.Button buttonStartListen;

        /// <summary>
        /// Defines the buttonSendMsg
        /// </summary>
        private System.Windows.Forms.Button buttonSendMsg;

        /// <summary>
        /// Defines the buttonClose
        /// </summary>
        private System.Windows.Forms.Button buttonClose;

        /// <summary>
        /// Defines the MAX_CLIENTS
        /// </summary>
        internal const int MAX_CLIENTS = 10;

        /// <summary>
        /// Defines the pfnWorkerCallBack
        /// </summary>
        public AsyncCallback pfnWorkerCallBack ;

        /// <summary>
        /// Defines the m_mainSocket
        /// </summary>
        private  Socket m_mainSocket;

        /// <summary>
        /// Defines the m_workerSocket
        /// </summary>
        private  Socket [] m_workerSocket = new Socket[30];// Hard limit of 30 TOTAL CONNECTIONS!!

        /// <summary>
        /// Defines the m_clientCount
        /// </summary>
        private int m_clientCount = 0;

        /// <summary>
        /// Defines the broadcastCheckbox
        /// </summary>
        private CheckBox broadcastCheckbox;

        /// <summary>
        /// Defines the broadcastIncomingMessages
        /// </summary>
        private bool broadcastIncomingMessages = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketServer"/> class.
        /// </summary>
        public SocketServer()
        {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// Display the local IP address on the GUI
			textBoxIP.Text = GetIP();
        }

        /// <summary>
        /// The Main
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/></param>
        [STAThread]
		public static void Main(string[] args)
        {
			Application.Run(new SocketServer());
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()         {
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSendMsg = new System.Windows.Forms.Button();
            this.buttonStartListen = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.richTextBoxSendMsg = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStopListen = new System.Windows.Forms.Button();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.richTextBoxReceivedMsg = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.broadcastCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(321, 253);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(88, 24);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // buttonSendMsg
            // 
            this.buttonSendMsg.Location = new System.Drawing.Point(12, 197);
            this.buttonSendMsg.Name = "buttonSendMsg";
            this.buttonSendMsg.Size = new System.Drawing.Size(192, 24);
            this.buttonSendMsg.TabIndex = 7;
            this.buttonSendMsg.Text = "Send Message";
            this.buttonSendMsg.Click += new System.EventHandler(this.ButtonSendMsgClick);
            // 
            // buttonStartListen
            // 
            this.buttonStartListen.BackColor = System.Drawing.Color.Blue;
            this.buttonStartListen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartListen.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStartListen.Location = new System.Drawing.Point(227, 16);
            this.buttonStartListen.Name = "buttonStartListen";
            this.buttonStartListen.Size = new System.Drawing.Size(88, 40);
            this.buttonStartListen.TabIndex = 4;
            this.buttonStartListen.Text = "Start Listening";
            this.buttonStartListen.UseVisualStyleBackColor = false;
            this.buttonStartListen.Click += new System.EventHandler(this.ButtonStartListenClick);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(88, 16);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxIP.TabIndex = 12;
            // 
            // richTextBoxSendMsg
            // 
            this.richTextBoxSendMsg.Location = new System.Drawing.Point(12, 87);
            this.richTextBoxSendMsg.Name = "richTextBoxSendMsg";
            this.richTextBoxSendMsg.Size = new System.Drawing.Size(192, 104);
            this.richTextBoxSendMsg.TabIndex = 6;
            this.richTextBoxSendMsg.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // buttonStopListen
            // 
            this.buttonStopListen.BackColor = System.Drawing.Color.Red;
            this.buttonStopListen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopListen.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStopListen.Location = new System.Drawing.Point(321, 16);
            this.buttonStopListen.Name = "buttonStopListen";
            this.buttonStopListen.Size = new System.Drawing.Size(88, 40);
            this.buttonStopListen.TabIndex = 5;
            this.buttonStopListen.Text = "Stop Listening";
            this.buttonStopListen.UseVisualStyleBackColor = false;
            this.buttonStopListen.Click += new System.EventHandler(this.ButtonStopListenClick);
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMsg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBoxMsg.Location = new System.Drawing.Point(118, 259);
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.ReadOnly = true;
            this.textBoxMsg.Size = new System.Drawing.Size(192, 13);
            this.textBoxMsg.TabIndex = 14;
            this.textBoxMsg.Text = "None";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Broadcast Message To Clients";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(217, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Message Received From Clients";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(88, 40);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(40, 20);
            this.textBoxPort.TabIndex = 0;
            this.textBoxPort.Text = "8000";
            // 
            // richTextBoxReceivedMsg
            // 
            this.richTextBoxReceivedMsg.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.richTextBoxReceivedMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.richTextBoxReceivedMsg.Location = new System.Drawing.Point(217, 87);
            this.richTextBoxReceivedMsg.Name = "richTextBoxReceivedMsg";
            this.richTextBoxReceivedMsg.ReadOnly = true;
            this.richTextBoxReceivedMsg.Size = new System.Drawing.Size(192, 129);
            this.richTextBoxReceivedMsg.TabIndex = 9;
            this.richTextBoxReceivedMsg.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server IP";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Status Message:";
            // 
            // broadcastCheckbox
            // 
            this.broadcastCheckbox.AccessibleName = "BroadcastCheckbox";
            this.broadcastCheckbox.AutoSize = true;
            this.broadcastCheckbox.Location = new System.Drawing.Point(19, 227);
            this.broadcastCheckbox.Name = "broadcastCheckbox";
            this.broadcastCheckbox.Size = new System.Drawing.Size(171, 17);
            this.broadcastCheckbox.TabIndex = 15;
            this.broadcastCheckbox.Text = "Broadcast Incoming Messages";
            this.broadcastCheckbox.UseVisualStyleBackColor = true;
            this.broadcastCheckbox.CheckedChanged += new System.EventHandler(this.BroadcastCheckbox_CheckedChanged);
            // 
            // SocketServer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(424, 284);
            this.Controls.Add(this.broadcastCheckbox);
            this.Controls.Add(this.textBoxMsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBoxReceivedMsg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonSendMsg);
            this.Controls.Add(this.richTextBoxSendMsg);
            this.Controls.Add(this.buttonStopListen);
            this.Controls.Add(this.buttonStartListen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPort);
            this.Name = "SocketServer";
            this.Text = "SocketServer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// The ButtonStartListenClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        internal void ButtonStartListenClick(object sender, System.EventArgs e)
        {
			try
			{
				// Check the port value
				if(textBoxPort.Text == ""){
					MessageBox.Show("Please enter a Port Number");
					return;
				}
				string portStr = textBoxPort.Text;
				int port = System.Convert.ToInt32(portStr);
				// Create the listening socket...
				m_mainSocket = new Socket(AddressFamily.InterNetwork, 
				                          SocketType.Stream, 
				                          ProtocolType.Tcp);
				IPEndPoint ipLocal = new IPEndPoint (IPAddress.Any, port);
				// Bind to local IP Address...
				m_mainSocket.Bind( ipLocal );
				// Start listening...
				m_mainSocket.Listen (4);
				// Create the call back for any client connections...
				m_mainSocket.BeginAccept(new AsyncCallback (OnClientConnect), null);
				
				UpdateControls(true);
				
			}
			catch(SocketException se)
			{
				MessageBox.Show ( se.Message );
			}
        }

        /// <summary>
        /// The UpdateControls
        /// </summary>
        /// <param name="listening">The listening<see cref="bool"/></param>
        private void UpdateControls( bool listening ) 
        {
			buttonStartListen.Enabled 	= !listening;
			buttonStopListen.Enabled 	= listening;
        }

        // This is the call back function, which will be invoked when a client is connected
        /// <summary>
        /// The OnClientConnect
        /// </summary>
        /// <param name="asyn">The asyn<see cref="IAsyncResult"/></param>
        public void OnClientConnect(IAsyncResult asyn)
        {
			try
			{
				// Here we complete/end the BeginAccept() asynchronous call
				// by calling EndAccept() - which returns the reference to
				// a new Socket object
				m_workerSocket[m_clientCount] = m_mainSocket.EndAccept (asyn);

				// Display this client connection as a status message on the GUI	
				String str = String.Format("Client # {0} connected", m_clientCount);
                SetFormStatus(str);
                LogIncomingMessageToForm(str);
                if (broadcastIncomingMessages)
                {
                    SendMsgToAll(str);
                }

                // Send the client their ID (First thing the server sends!)
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(m_clientCount.ToString());
                m_workerSocket[m_clientCount].Send(byData);

				// Let the worker Socket do the further processing for the 
				// just connected client
				WaitForData(m_clientCount);
								
				// Now increment the client count
				++m_clientCount;
				// Since the main Socket is now free, it can go back and wait for
				// other clients who are attempting to connect
				m_mainSocket.BeginAccept(new AsyncCallback ( OnClientConnect ),null);
			}
			catch(ObjectDisposedException)
			{
				System.Diagnostics.Debugger.Log(0,"1","\n OnClientConnection: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				MessageBox.Show ( se.Message );
			}
        }

        /// <summary>
        /// Defines the <see cref="SocketPacket" />
        /// </summary>
        public class SocketPacket
        {
            /// <summary>
            /// Defines the m_currentSocket
            /// </summary>
            public System.Net.Sockets.Socket m_currentSocket;

            /// <summary>
            /// Defines the dataBuffer
            /// </summary>
            public byte[] dataBuffer = new byte[255];

            /// <summary>
            /// Defines the id
            /// </summary>
            public int id;
        }

        // Start waiting for data from the client
        /// <summary>
        /// The WaitForData
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        public void WaitForData(int id)
        {
            Socket soc = m_workerSocket[id];
			try
			{
				if  ( pfnWorkerCallBack == null ){		
					// Specify the call back function which is to be 
					// invoked when there is any write activity by the 
					// connected client
					pfnWorkerCallBack = new AsyncCallback (OnDataReceived);
				}
				SocketPacket theSocPkt = new SocketPacket ();
				theSocPkt.m_currentSocket = soc;
                theSocPkt.id = id;
				// Start receiving any data written by the connected client
				// asynchronously
				soc.BeginReceive (theSocPkt.dataBuffer, 0, 
				                   theSocPkt.dataBuffer.Length,
				                   SocketFlags.None,
				                   pfnWorkerCallBack,
				                   theSocPkt);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
        }

        // This the call back function which will be invoked when the socket
		// detects any client writing of data on the stream
        /// <summary>
        /// The OnDataReceived
        /// </summary>
        /// <param name="asyn">The asyn<see cref="IAsyncResult"/></param>
        public  void OnDataReceived(IAsyncResult asyn)
        {
			try
			{
				SocketPacket socketData = (SocketPacket)asyn.AsyncState ;

				int iRx  = 0 ;
				// Complete the BeginReceive() asynchronous call by EndReceive() method
				// which will return the number of characters written to the stream 
				// by the client
				iRx = socketData.m_currentSocket.EndReceive (asyn);
				char[] chars = new char[iRx +  1];
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(socketData.dataBuffer, 
				                         0, iRx, chars, 0);
				System.String szData = new System.String(chars);
                LogIncomingMessageToForm(socketData.id.ToString(), szData);
                if (broadcastIncomingMessages)
                {
                    SendMsgToAll(socketData.id.ToString(), szData);
                }
	
				// Continue the waiting for data on the Socket
				WaitForData( socketData.id );
			}
			catch (ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log(0,"1","\nOnDataReceived: Socket has been closed\n");
			}
			catch(SocketException se)
			{
                MessageBox.Show(se.Message);
			}
        }

        /// <summary>
        /// The ButtonSendMsgClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        internal void ButtonSendMsgClick(object sender, System.EventArgs e)
        {
            String msg = richTextBoxSendMsg.Text;
            SendMsgToAll(msg);
        }

        // Adds timestamp and ID to message before being sent
        /// <summary>
        /// The SendMsgToAll
        /// </summary>
        /// <param name="msg">The msg<see cref="String"/></param>
        internal void SendMsgToAll(String msg)
        {
            SendToAll(AddMetaToMessage("", msg));
        }

        /// <summary>
        /// The SendMsgToAll
        /// </summary>
        /// <param name="id">The id<see cref="String"/></param>
        /// <param name="msg">The msg<see cref="String"/></param>
        internal void SendMsgToAll(String id, String msg)
        {
            SendToAll(AddMetaToMessage(id, msg));
        }

        // Sends message as is to all clients
        /// <summary>
        /// The SendToAll
        /// </summary>
        /// <param name="message">The message<see cref="String"/></param>
        internal void SendToAll(String message)
        {
			try
			{
				byte[] byData = System.Text.Encoding.ASCII.GetBytes(message);
				for(int i = 0; i < m_clientCount; i++){
					if(m_workerSocket[i] != null){
						if(m_workerSocket[i].Connected){
							m_workerSocket[i].Send (byData);
						}
					}
				}
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
        }

        // Returns the string with meta data appended to it (Timestamp, ID)
        /// <summary>
        /// The AddMetaToMessage
        /// </summary>
        /// <param name="id">The id<see cref="String"/></param>
        /// <param name="msg">The msg<see cref="String"/></param>
        /// <returns>The <see cref="String"/></returns>
        internal String AddMetaToMessage(String id, String msg)
        {
            id = id == "" ? "S" : id;
            String message = String.Format("{0}#{1} - {2}", GetTimeStamp(DateTime.Now), id, msg);
            return message;
        }

        /// <summary>
        /// The ButtonStopListenClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        internal void ButtonStopListenClick(object sender, System.EventArgs e)
        {
			CloseSockets();			
			UpdateControls(false);
        }

        /// <summary>
        /// The LogIncomingMessageToForm
        /// </summary>
        /// <param name="msg">The msg<see cref="String"/></param>
        internal void LogIncomingMessageToForm(String msg)
        {
            LogIncomingMessageToForm("", msg);
        }

        /// <summary>
        /// The LogIncomingMessageToForm
        /// </summary>
        /// <param name="clientId">The clientId<see cref="String"/></param>
        /// <param name="msg">The msg<see cref="String"/></param>
        internal void LogIncomingMessageToForm(String clientId, String msg)
        {
            String message = AddMetaToMessage(clientId, msg);
            Invoke(new Action(() =>
            {
                richTextBoxReceivedMsg.AppendText(message);
                richTextBoxReceivedMsg.AppendText(Environment.NewLine); // Only works as a seperate append...
                richTextBoxReceivedMsg.ScrollToCaret();
            }));
        }

        /// <summary>
        /// The GetTimeStamp
        /// </summary>
        /// <param name="value">The value<see cref="DateTime"/></param>
        /// <returns>The <see cref="String"/></returns>
        internal String GetTimeStamp(DateTime value)
        {
            return value.ToString("hh:mm");
        }

        /// <summary>
        /// The SetFormStatus
        /// </summary>
        /// <param name="msg">The msg<see cref="String"/></param>
        internal void SetFormStatus(String msg)
        {
            Invoke(new Action(() =>
            {
                textBoxMsg.Text = msg;
            }));
        }

        /// <summary>
        /// The GetIP
        /// </summary>
        /// <returns>The <see cref="String"/></returns>
        internal String GetIP()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endpoint = socket.LocalEndPoint as IPEndPoint;
                return endpoint.Address.ToString();
            }
        }

        /// <summary>
        /// The ButtonCloseClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        internal void ButtonCloseClick(object sender, System.EventArgs e)
        {
	   		CloseSockets();
	   		Close();
        }

        /// <summary>
        /// The CloseSockets
        /// </summary>
        internal void CloseSockets()
        {
	   		if(m_mainSocket != null){
	   			m_mainSocket.Close();
	   		}
			for(int i = 0; i < m_clientCount; i++){
				if(m_workerSocket[i] != null){
					m_workerSocket[i].Close();
					m_workerSocket[i] = null;
				}
			}	
        }

        /// <summary>
        /// The BroadcastCheckbox_CheckedChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void BroadcastCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            broadcastIncomingMessages = checkbox.Checked;
        }
    }
}
