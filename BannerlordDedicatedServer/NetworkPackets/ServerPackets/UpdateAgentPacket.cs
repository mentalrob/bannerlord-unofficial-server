using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;
using static TaleWorlds.MountAndBlade.Agent;

namespace BannerlordDedicatedServer.NetworkPackets.ServerPackets
{
    [ProtoContract, PacketId(3)]
    class UpdateAgentPacket: IServerDataPacket
    {
        [ProtoMember(1)]
        public int index { get; set; }
        [ProtoMember(2)]
        public float[] position { get; set; }
        [ProtoMember(3)]
        public float[] lookPosition { get; set; }
        [ProtoMember(4)]
        public MovementControlFlag movementControlFlag { get; set; }
        [ProtoMember(5)]
        public EventControlFlag eventControlFlag { get; set; }
        [ProtoMember(6)]
        public ActionCodeType actionCodeType { get; set; }
        [ProtoMember(7)]
        public int tick { get; set; }
        [ProtoMember(8)]
        public bool agentCrouching { get; set; }
        [ProtoMember(9)]
        public bool agentLeftStance { get; set; }

    }
}