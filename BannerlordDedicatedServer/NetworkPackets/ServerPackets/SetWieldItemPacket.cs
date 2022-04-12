using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;

namespace BannerlordDedicatedServer.NetworkPackets.ServerPackets
{
    [ProtoContract, PacketId(4)]
    class SetWieldItemPacket: IServerDataPacket
    {
        [ProtoMember(1)]
        public EquipmentIndex mainHandIndex { get; set; }

        [ProtoMember(2)]
        public EquipmentIndex offHandIndex { get; set; }
        
        [ProtoMember(3)]
        public int agentIndex { get; set; }

    }
}
