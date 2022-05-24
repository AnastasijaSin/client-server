using UnityEngine;

public class ServerApplication : MonoBehaviour
{
    private Server _server;
    private MessageHandler _handler;

    private int port = 7777;

    public Functionyren functionyren;

    private void Start()
    {
        _server = new Server();
        _server.Start();
        _server.OnMessageReceived += OnMessage;

        _handler = new MessageHandler();
        _handler.AddHandler("function", FunctionUpdate);

        functionyren.OnDataUpdated += SendData;
    }

    private void OnMessage(byte[] data)
    {
        _handler.Handle(data);
    }


    private void FunctionUpdate(byte[] msg)
    {
        // Converting our from bytes to class
        var data = msg.Deserialize<FunctionData>();

        //Assigning data to a frunctionyren
        functionyren.Data = data;
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

        _server.SendMessage(message.Serialize()); // send message
    }

}
