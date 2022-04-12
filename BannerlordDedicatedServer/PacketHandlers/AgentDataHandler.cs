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
    [Handles(typeof(AgentDataPacket))]
    class AgentDataHandler : IPacketHandler
    {
        public void Handle(int connectionId, IDataPacket packet)
        {
            if(State.CurrentState.agents[connectionId] != null)
            {
                
                AgentDataPacket adp = (AgentDataPacket)packet;
                int currentIndex = adp.tick % Program.BUFFER_SIZE;
                State.CurrentState.agents[connectionId].lookPosition = adp.lookPosition;
                State.CurrentState.agents[connectionId].position = adp.position;
                State.CurrentState.agents[connectionId].movementControlFlag = adp.movementControlFlag;
                State.CurrentState.agents[connectionId].eventControlFlag = adp.eventControlFlag;
                State.CurrentState.agents[connectionId].pastPositions[currentIndex, 0] = adp.position[0];
                State.CurrentState.agents[connectionId].pastPositions[currentIndex, 1] = adp.position[1];
                State.CurrentState.agents[connectionId].pastPositions[currentIndex, 2] = adp.position[2];

                UpdateAgentPacket updateAgentPacket = new UpdateAgentPacket()
                {
                    eventControlFlag = adp.eventControlFlag,
                    index = State.CurrentState.agents[connectionId].index,
                    position = adp.position,
                    lookPosition = adp.lookPosition,
                    movementControlFlag = adp.movementControlFlag,
                    actionCodeType = adp.actionCodeType,
                    tick = adp.tick,
                    agentCrouching = adp.agentCrouching,
                    agentLeftStance = adp.agentLeftStance
                    
                };
                CommunicatorHelper.BroadCastPacket<UpdateAgentPacket>(updateAgentPacket, (cid) => true);
            }
        }
    }
}
