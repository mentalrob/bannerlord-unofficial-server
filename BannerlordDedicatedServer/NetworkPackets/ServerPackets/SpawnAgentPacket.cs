using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace BannerlordDedicatedServer.NetworkPackets.ServerPackets
{
    [ProtoContract, PacketId(2)]
    class SpawnAgentPacket: IServerDataPacket
    {
        [ProtoMember(1)]
        public string CharacterId { get; set; }
        [ProtoMember(2)]
        public float[] Location { get; set; }
        [ProtoMember(3)]
        public Boolean Controlled { get; set; }
        [ProtoMember(4)]
        public EquipmentIndex mainHandEquipment { get; set; }
        [ProtoMember(5)]
        public EquipmentIndex offHandEquipment { get; set; }

        [ProtoMember(6)]
        public int agentIndex { get; set; }

        [ProtoMember(7)]
        public bool isPlayer { get; set; }
        [ProtoMember(8)]
        public int health { get; set; }
    }
}
