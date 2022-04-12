using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using static TaleWorlds.MountAndBlade.Agent;

namespace BannerlordDedicatedServer.NetworkPackets.ClientPackets
{
    [ProtoContract, PacketId(2)]
    class AgentDataPacket: IDataPacket
    {
        [ProtoMember(1)]
        public int agentId { get; set; }
        [ProtoMember(2)]
        public MovementControlFlag movementControlFlag { get; set; }
        [ProtoMember(3)]
        public EventControlFlag eventControlFlag { get; set; }
        [ProtoMember(4)]
        public ActionCodeType actionCodeType { get; set; }
        [ProtoMember(5)]
        public float[] position { get; set; }
        [ProtoMember(6)]
        public float[] lookPosition { get; set; }
        [ProtoMember(7)]
        public int tick { get; set; }

        [ProtoMember(8)]
        public bool agentCrouching { get; set; }
        [ProtoMember(9)]
        public bool agentLeftStance { get; set; }
    }
}
