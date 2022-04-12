using BannerlordDedicatedServer.NetworkPackets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BannerlordDedicatedServer.CustomAttributes;
using BannerlordDedicatedServer.ServerState;
using System.Linq;

namespace BannerlordDedicatedServer.Helpers
{
    class CommunicatorHelper
    {
        public static void ServerSendPacket<T>(int connectionId,T dataPacket)
        {
            MethodInfo mi = typeof(ProtobufHelper).GetMethod("ProtoSerialize", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(dataPacket.GetType());
            byte[] content = (byte[])mi.Invoke(null, new object[] { dataPacket });
            Packet packet = new Packet
            {
                Id = dataPacket.GetType().GetCustomAttribute<PacketIdAttribute>().id,
                Data = content
            };
            // Console.WriteLine("Sended packet Content " + BitConverter.ToString(ProtobufHelper.ProtoSerialize<Packet>(packet)));
            Application.server.Send(connectionId, ProtobufHelper.ProtoSerialize<Packet>(packet));
        }

        public static void BroadCastPacket<T>(T dataPacket, Func<int,Boolean> predicate)
        {
            
            foreach(int connectionId in State.CurrentState.connectedIds.Where(predicate))
            {
                MethodInfo mi = typeof(ProtobufHelper).GetMethod("ProtoSerialize", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(dataPacket.GetType());
                byte[] content = (byte[])mi.Invoke(null, new object[] { dataPacket });
                Packet packet = new Packet
                {
                    Id = dataPacket.GetType().GetCustomAttribute<PacketIdAttribute>().id,
                    Data = content
                };
                // Console.WriteLine("Sended packet Content " + BitConverter.ToString(ProtobufHelper.ProtoSerialize<Packet>(packet)));
                Application.server.Send(connectionId, ProtobufHelper.ProtoSerialize<Packet>(packet));
            }
        }
    }
}
