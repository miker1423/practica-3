using System;
using CoAP;
using System.Text;
using System.Threading;

namespace CoapTest
{
    class Program
    {
        private static string variable = "3ea6";
        private static string MAC_ADDRES = $"fd01::{variable}:260:37ff:fe00:fa5d";

        static void Main(string[] args)
        {
            Status();
            Thread.Sleep(500);
            IsOn(true);
            Thread.Sleep(500);
            Status();
            Thread.Sleep(500);
            IsOn(false);
            Thread.Sleep(500);
            Status();

            //Temp();
            //Humidity();
            //IsOn(false);
            //Mode(false);
            //Config(23, 12);
        }

        static void Status()
        {
            var client = Request.NewGet();
            client.URI = new Uri($"coap://[{MAC_ADDRES}]:5683/status");
            client.Send();
            var response = client.WaitForResponse();
            var value = response.Payload[0];
            Console.WriteLine($"Received {value}");
        }

        static void Temp()
        {
            var client = Request.NewGet();
            client.URI = new Uri($"coap://[{MAC_ADDRES}]:5683/temp");
            client.Send();
            var response = client.WaitForResponse();
            Console.WriteLine(response.ToString());
        }

        static void Humidity()
        {
            var client = Request.NewGet();
            client.URI = new Uri($"coap://[{MAC_ADDRES}]:5683/humidity");
            client.Send();
            var response = client.WaitForResponse();
            Console.WriteLine(response.ToString());
        }

        static void IsOn(bool turnOn)
        {
            var client = Request.NewPost();
            client.URI = new Uri($"coap://[{MAC_ADDRES}]:5683/toggle");
            var value = turnOn ? 0x01 : 0x00; 
            client.SetPayload(new byte[] { (byte)value }, 0);
            client.Send();
            var response = client.WaitForResponse();
            Console.WriteLine(response.ToString());
        }

        static void Config(int temp, int humidity)
        {
            var client = Request.NewPost();
            client.URI = new Uri($"coap://[{MAC_ADDRES}]:5683/config");   
            var data = new byte[2] { (byte)temp, (byte)humidity};
            client.SetPayload(data, 0);
            client.Send();
            var response = client.WaitForResponse();
            Console.WriteLine(response.ToString());
        }

        static void Mode(bool isAuto)
        {
            var client = Request.NewPost();
            client.URI = new Uri($"coap://[{MAC_ADDRES}]:5683/mode");
            var value = isAuto ? "AUTO" : "MANUAL";
            var data = Encoding.ASCII.GetBytes(value);
            client.SetPayload(data, 0);
            client.Send();
            var response = client.WaitForResponse();
            Console.WriteLine(response.ToString());
        }
    }
}
