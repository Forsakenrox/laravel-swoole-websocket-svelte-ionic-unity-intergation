using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CubeBehaviour : MonoBehaviour
{
    private const float RotationSpeed = 50;
    // Start is called before the first frame update
    public SocketIOUnity socket;
    public Text ReceivedText;
    void Start()
    {
        var uri = new Uri("http://192.168.46.130");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 3
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };
        socket.OnPing += (sender, e) =>
        {
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
        ////

        Debug.Log("Connecting...");
        socket.Connect();

        socket.OnUnityThread("message", (data) =>
        {
        Debug.Log(data.GetValue().GetProperty("x"));
            string x = data.GetValue().GetProperty("x").ToString();
            string y = data.GetValue().GetProperty("y").ToString();
            string z = data.GetValue().GetProperty("z").ToString();
            float xf = float.Parse(x, CultureInfo.InvariantCulture);
            float yf = float.Parse(y, CultureInfo.InvariantCulture);
            float zf = float.Parse(z, CultureInfo.InvariantCulture);
            //transform.eulerAngles = new Vector3(0, 0, xf);
            transform.rotation = Quaternion.Euler(0, xf, 0);
        });

        //ReceivedText.text = "";
        socket.OnAnyInUnityThread((name, response) =>
        {
           // ReceivedText.text += "Received On " + name + " : " + response.GetValue().GetRawText() + "\n";
        });
        Debug.Log("after connect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
           // transform.Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0));
           // transform.Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0));
            transform.eulerAngles = new Vector3(11, 60, 33);
            Debug.Log("Hello: 123");
        }        
    }   
}
