using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System;

public class NetManager : MonoBehaviour
{
    private string ip = "127.0.0.1";
    private int port = 5555;
    private Socket socket;
    private Dictionary<string, string> msg;

    public string user_id;
    public string response_str;//返回数据
    public int frame = 0;

    void Start()
    {
        user_id = "1P";
        msg = new Dictionary<string, string>();
        
        CreateConnetion();
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    void CreateConnetion()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//TCP协议
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);//IP+端口
            socket.Connect(point);
            Debug.Log(user_id + "连接成功！");

            StartCoroutine(SendMsg());//开启协程用于发送消息
            Debug.Log("协程开启");
            //StartCoroutine(ReceiveMsg());//开启协程用于接收消息
        }
        catch(Exception)
        {
            Debug.Log("未连接成功...");
        }

    }

    IEnumerator SendMsg()
    {
        Debug.Log("进协程了");
        while(true)
        {
            msg.Clear();
            msg["id"] = user_id;
            msg["frame"] = "" + frame;

            Debug.Log(msg["id"] + " " + msg["frame"]);

            string json = JsonUtility.ToJson(msg);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            

            try
            {
                socket.Send(bytes);
            }
            catch (Exception)
            {
                Debug.Log("发送失败...");
            }

            yield return new WaitForFixedUpdate();

            frame++;
        }       
    }

    IEnumerator ReceiveMsg()
    {
        while(true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 3];

                //实际接收到的有效字节数
                int len = socket.Receive(buffer);
                if (len == 0) break;//如果收到的是空就break
                string str = Encoding.UTF8.GetString(buffer, 0, len);
                Debug.Log("客户端打印：" + socket.RemoteEndPoint + ":" + str);
            }
            catch(Exception)
            {
                Debug.Log("接收失败...");
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
