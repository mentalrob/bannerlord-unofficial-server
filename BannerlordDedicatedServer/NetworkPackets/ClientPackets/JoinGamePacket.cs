using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets.ClientPackets
{
    [ProtoContract, PacketId(1)]
    class JoinGamePacket: IDataPacket
    {
        [ProtoMember(1)]
        public string PlayerName { get; set; }
    }
}
