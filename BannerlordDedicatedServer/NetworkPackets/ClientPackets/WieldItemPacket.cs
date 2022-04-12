using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;

namespace BannerlordDedicatedServer.NetworkPackets.ClientPackets
{
    [ProtoContract, PacketId(4)]
    class WieldItemPacket : IDataPacket
    {
        [ProtoMember(1)]
        public EquipmentIndex mainHandEquipmentIndex { get; set; }
        [ProtoMember(2)]
        public EquipmentIndex offHandEquipmentIndex { get; set; }
    }
}
