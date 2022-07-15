using System.Net;

public class Server
{
    public static string data = null;

    public static void StartListening()
    {
        byte[] bytes = new byte[1024];

        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 1235);
    }
}