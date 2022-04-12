using BannerlordDedicatedServer.PacketHandlers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using BannerlordDedicatedServer.CustomAttributes;
using BannerlordDedicatedServer.NetworkPackets;

namespace BannerlordDedicatedServer
{
    class Application
    {
        public static Telepathy.Server server;


        public static void LoadHandlers() {
            Console.WriteLine("[!] Loading packet handlers");
            foreach(Type packetHandlerType in Assembly.GetExecutingAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IPacketHandler))))
            {
                HandlesAttribute attribute = packetHandlerType.GetCustomAttribute<HandlesAttribute>();
                HandlerRegistry.storage[attribute.handles] = (IPacketHandler)Activator.CreateInstance(packetHandlerType);
                Console.WriteLine("[+] Loaded handler " + packetHandlerType.Name);
            }
        }

        public static void LoadNetworkPackets() {
            Console.WriteLine("[!] Loading network packets");
            foreach (Type networkPacketType in Assembly.GetExecutingAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IDataPacket))))
            {
                PacketIdAttribute attribute = networkPacketType.GetCustomAttribute<PacketIdAttribute>();
                NetworkPacketRegistry.storage[attribute.id] = networkPacketType;
                Console.WriteLine("[+] Loaded packet " + networkPacketType.Name);
            }
        }
    }
}
