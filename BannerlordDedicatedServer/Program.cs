using BannerlordDedicatedServer.Helpers;
using BannerlordDedicatedServer.NetworkPackets;
using BannerlordDedicatedServer.Server;
using System;
using System.Text;
using System.Threading;
using BannerlordDedicatedServer.ServerState;
using BannerlordDedicatedServer.NetworkPackets.ServerPackets;
using BannerlordDedicatedServer.NetworkPackets.ClientPackets;
using static TaleWorlds.MountAndBlade.Agent;
using BannerlordDedicatedServer.StateModels;

namespace BannerlordDedicatedServer
{

    class Program
    {
        private const int SERVER_TICK_RATE = 60;
        private const int PROCESS_PACKET_SIZE = 100;
        public static int BUFFER_SIZE = 1024;

        static void processMessages(Telepathy.Server server)
        {
            while (true)
            {
                server.Tick(PROCESS_PACKET_SIZE);
                System.Threading.Thread.Sleep(1000/SERVER_TICK_RATE); // her 16 ms de 100 paket işliyor
            }
        }
        static void Main(string[] args)
        {
            Application.LoadHandlers();
            Application.LoadNetworkPackets();
            Handler serverHandler = new Handler();
            State serverState = new State();
            Console.WriteLine("Starting server at port 1337");

            Telepathy.Server server = new Telepathy.Server(1024 * 1024);
            server.OnConnected = serverHandler.OnConnection;
            server.OnData = serverHandler.OnData;
            server.OnDisconnected = serverHandler.OnDisconnect;

            server.Start(1337);
            Application.server = server;
            System.Threading.Thread.Sleep(100);
            Thread thr = new Thread(() => Program.processMessages(server));
            thr.Start();
            
            while (true)
            {
                Console.Write(" > ");
                String message = Console.ReadLine();
                String[] arguments = message.Split(' ');
            }
        }
    }
}
