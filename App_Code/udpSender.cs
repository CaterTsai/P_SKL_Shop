using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// udpSender 的摘要描述
/// </summary>
public class udpSender
{
    private string _ip { get; set; }
    private int _port { get; set; }

    public udpSender(string ip, int port)
    {
        this._ip = ip;
        this._port = port;
    }

    public void send(string msg)
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(_ip), _port);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        server.SendTo(Encoding.UTF8.GetBytes(msg), ipep);

        server.Close();
    }
}