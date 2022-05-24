using System;
using UnityEngine;

public class ClientApplication : MonoBehaviour
{
    private Client _client;
    private MessageHandler _handler;

    public Functionyren functionyren;

    private void Start()
    {
        Connect();
        functionyren.OnDataUpdated += SendData;
    }

    private void Connect()
    {
        // Create new client
        _client = new Client();
        _client.Connect(); // Connect to server

        // Add new message void handler
        _client.OnMessageRecieved += MessageReceived;


        // Create message handler for header
        _handler = new MessageHandler();
        _handler.AddHandler("function", FunctionUpdate);
    }

    private void FunctionUpdate(byte[] msg)
    {
        // Conert bytes to data
        var data = msg.Deserialize<FunctionData>();

        //Update data in functionyren
        functionyren.Data = data;
    }

    private void MessageReceived(byte[] msg)
    {
        // Call message handler 
        _handler.Handle(msg);
    }

    public void SendData()
    {

        // Get data
        var data = functionyren.Data;

        // Create message 
        var message = new Message
        {
            header = "function", // message header for handler
            body = data.Serialize() // convert our data to bytes
        };

        _client.SendMessage(message.Serialize()); // send message
    }
}
