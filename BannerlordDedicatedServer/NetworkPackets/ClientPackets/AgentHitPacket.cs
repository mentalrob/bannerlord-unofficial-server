using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets.ClientPackets
{
    [ProtoContract, PacketId(6)]
    class AgentHitPacket : IDataPacket
    {
        [ProtoMember(1)]
        public byte[] blowData { get; set; }
        [ProtoMember(2)]
        public int victimIndex { get; set; }
    }
}
