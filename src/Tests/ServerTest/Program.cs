using DotNetMQ;
using System;
using DotNetMQ.Threading;
using DotNetMQ.Storage;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var server = new MDSServer(StorageManagerFactory.CreateStorageManager());
                server.Start();

                Console.WriteLine("DotNetMQ server has started.");
                Console.WriteLine("Press enter to stop...");
                Console.ReadLine();

                server.Stop(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
