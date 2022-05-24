using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Client 
{
	private TcpClient socketConnection;
	private Thread clientReceiveThread;
	public event Action<byte[]> OnMessageRecieved;
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	public void Connect()
	{
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}

	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient("localhost", 8052);
			Byte[] bytes = new Byte[1024];
			while (true)
			{			
				using (NetworkStream stream = socketConnection.GetStream())
				{
					int length;			
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);

						OnMessageRecieved?.Invoke(incommingData);
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	public void SendMessage(byte[] msg)
	{
		if (socketConnection == null)
		{
			return;
		}
		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite)
			{           
				stream.Write(msg, 0, msg.Length);
				Debug.Log("Client sent his message - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}