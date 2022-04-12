using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets.ServerPackets
{
    [ProtoContract, PacketId(1)]
    class WelcomePacket: IServerDataPacket
    {
        [ProtoMember(1)]
        public string Message { get; set; }
    }
}
