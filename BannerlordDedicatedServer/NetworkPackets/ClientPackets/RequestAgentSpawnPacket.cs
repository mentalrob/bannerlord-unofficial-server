using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets.ClientPackets
{
    [ProtoContract, PacketId(3)]
    class RequestAgentSpawnPacket: IDataPacket
    {
        [ProtoMember(1)]
        public string characterId { get; set; }
    }
}
