using BannerlordDedicatedServer.Helpers;
using BannerlordDedicatedServer.NetworkPackets;
using BannerlordDedicatedServer.NetworkPackets.ServerPackets;
using BannerlordDedicatedServer.PacketHandlers;
using BannerlordDedicatedServer.ServerState;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace BannerlordDedicatedServer.Server
{
    class Handler : IHandler
    {
        public void OnConnection(int connectionId)
        {
            Console.WriteLine("Connection from " + connectionId);
            State.CurrentState.connectedIds.Add(connectionId);
        }

        public void OnData(int connectionId, ArraySegment<byte> message)
        {
            byte[] serializedPacket =  message.Slice(message.Offset, message.Count).ToArray();
            Packet packet = ProtobufHelper.ProtoDeserialize<Packet>(serializedPacket);
            Type packetType = NetworkPacketRegistry.storage[packet.Id];
            // Console.WriteLine("Data from " + connectionId + " packet is "+ packetType.Name);
            MethodInfo methodInfo = typeof(ProtobufHelper).GetMethod("ProtoDeserialize", BindingFlags.Public | BindingFlags.Static);
            IDataPacket dataPacket = (IDataPacket) methodInfo.MakeGenericMethod(packetType).Invoke(null, new object[] { packet.Data });
            HandlerRegistry.storage[packetType].Handle(connectionId, dataPacket);
        }

        public void OnDisconnect(int connectionId)
        {
            Console.WriteLine("Disconnected " + connectionId);
            // TODO: Send a disconnect packet

            State.CurrentState.connectedIds.Remove(connectionId);
            State.CurrentState.connectedPlayers.Remove(connectionId);
            if (State.CurrentState.agents.ContainsKey(connectionId)) {
                PlayerDisconnectedPacket pdp = new PlayerDisconnectedPacket()
                {
                    agentIndex = State.CurrentState.agents[connectionId].index
                };
                CommunicatorHelper.BroadCastPacket<PlayerDisconnectedPacket>(pdp, (int _) => true);
                State.CurrentState.agents.Remove(connectionId);
            }
        }
    }
}
