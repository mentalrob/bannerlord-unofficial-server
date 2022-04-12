using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets.ServerPackets
{
    [ProtoContract, PacketId(6)]
    class PlayerDisconnectedPacket : IServerDataPacket
    {
        [ProtoMember(1)]
        public int agentIndex { get; set; }
    }
}
