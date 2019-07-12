using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Project1
{
    class Class1
    {
        public static void Main (string[] args)
        {
            string name = (args.Length < 1) ? Dns.GetHostName() : args[0];
            try
            {
                IPAddress[] addrs = Dns.GetHostEntry(name).AddressList;
                foreach (IPAddress addr in addrs)
                {
                    Console.WriteLine("{0}/{1}", name, addr); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey(false);
        }
    }
}
