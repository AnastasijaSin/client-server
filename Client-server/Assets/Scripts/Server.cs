using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server 
{
	#region private members 	
	/// <summary> 	
	/// TCPListener to listen for incomming TCP connection 	
	/// requests. 	
	/// </summary> 	
	private TcpListener tcpListener;
	/// <summary> 
	/// Background thread for TcpServer workload. 	
	/// </summary> 	
	private Thread tcpListenerThread;
	/// <summary> 	
	/// Create handle to connected tcp client. 	
	/// </summary> 	
	private TcpClient connectedTcpClient;
	#endregion

	public event Action<byte[]> OnMessageReceived;

	// Use this for initialization
	public void Start()
	{
		// Start TcpServer background thread 		
		tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
		tcpListenerThread.IsBackground = true;
		tcpListenerThread.Start();
	}

	/// <summary> 	
	/// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
	/// </summary> 	
	private void ListenForIncommingRequests()
	{
		try
		{
			// Create listener on localhost port 8052. 			
			tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8052);
			tcpListener.Start();
			Debug.Log("Server is listening");
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				using (connectedTcpClient = tcpListener.AcceptTcpClient())
				{
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream())
					{
						int length;
						// Read incomming stream into byte arrary. 						
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
						{
							var incommingData = new byte[length];
							Array.Copy(bytes, 0, incommingData, 0, length);
							OnMessageReceived?.Invoke(incommingData);
						}
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("SocketException " + socketException.ToString());
		}
	}

	/// <summary> 	
	/// Send message to client using socket connection. 	
	/// </summary> 	
	public void SendMessage(byte[] msg)
	{
		if (connectedTcpClient == null)
		{
			return;
		}

		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = connectedTcpClient.GetStream();
			if (stream.CanWrite)
			{          
				stream.Write(msg, 0, msg.Length);
				Debug.Log("Server sent his message - should be received by client");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}