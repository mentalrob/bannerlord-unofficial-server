using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets.ServerPackets
{
    [ProtoContract, PacketId(5)]
    class ApplyAgentHitPacket : IServerDataPacket
    {
        [ProtoMember(1)]
        public int attackerIndex { get; set; }
        [ProtoMember(2)]
        public int victimIndex { get; set; }
        [ProtoMember(3)]
        public byte[] blowData { get; set; }
    }
}
