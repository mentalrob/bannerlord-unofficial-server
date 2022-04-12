using BannerlordDedicatedServer.CustomAttributes;
using BannerlordDedicatedServer.Helpers;
using BannerlordDedicatedServer.NetworkPackets;
using BannerlordDedicatedServer.NetworkPackets.ClientPackets;
using BannerlordDedicatedServer.NetworkPackets.ServerPackets;
using BannerlordDedicatedServer.ServerState;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.PacketHandlers
{
    [Handles(typeof(WieldItemPacket))]
    class WieldItemHandler : IPacketHandler
    {
        public void Handle(int connectionId, IDataPacket packet)
        {
            WieldItemPacket wip = (WieldItemPacket)packet;
            if(State.CurrentState.agents[connectionId] != null)
            {
                State.CurrentState.agents[connectionId].offhandEquipmentIndex = wip.offHandEquipmentIndex;
                State.CurrentState.agents[connectionId].mainHandEquipmentIndex = wip.mainHandEquipmentIndex;
                SetWieldItemPacket swip = new SetWieldItemPacket()
                {
                    agentIndex = State.CurrentState.agents[connectionId].index,
                    mainHandIndex = State.CurrentState.agents[connectionId].mainHandEquipmentIndex,
                    offHandIndex = State.CurrentState.agents[connectionId].offhandEquipmentIndex
                };
                CommunicatorHelper.BroadCastPacket<SetWieldItemPacket>(swip, (cid) => cid != connectionId);
            }
        }
    }
}
