using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Sockets;
using System.Net;  

namespace NoDiskSystem
{
    class ClientWakeUp
    {
        public static void WakeUp(byte[] mac)
        {
            UdpClient client = new UdpClient();
            client.Connect(IPAddress.Broadcast, 9090);
            byte[] packet = new byte[17 * 6];
            for (int i = 0; i < 6; i++)
                packet[i] = 0xFF;
            for (int i = 1; i <= 16; i++)
                for (int j = 0; j < 6; j++)
                    packet[i * 6 + j] = mac[j];
            int result = client.Send(packet, packet.Length);
        }


    }
}
