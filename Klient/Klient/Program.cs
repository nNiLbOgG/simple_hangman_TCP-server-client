using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        string zprava = "";

        do
        {
            Console.WriteLine("Zadejte zprávu:");
            zprava = Console.ReadLine();

            byte[] kOdeslani = Encoding.UTF8.GetBytes(zprava);
            OdeslatZpravu(kOdeslani);
        }
        while (zprava != "KONEC5*");
    }

    private static void OdeslatZpravu(byte[] zprava)
    {
        try
        {
            TcpClient tcpClient = new TcpClient("192.168.0.107", 666);
            NetworkStream sitovyProud = tcpClient.GetStream();
            sitovyProud.Write(zprava, 0, zprava.Length);
            sitovyProud.Close();
            sitovyProud.Dispose();
        }
        catch (Exception chyba)
        {
            Console.WriteLine(chyba.Message);
        }
    }
}

