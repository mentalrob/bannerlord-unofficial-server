using BannerlordDedicatedServer.CustomAttributes;
using BannerlordDedicatedServer.Helpers;
using BannerlordDedicatedServer.NetworkPackets;
using BannerlordDedicatedServer.NetworkPackets.ClientPackets;
using BannerlordDedicatedServer.NetworkPackets.ServerPackets;
using BannerlordDedicatedServer.ServerState;
using BannerlordDedicatedServer.StateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BannerlordDedicatedServer.PacketHandlers
{
    [Handles(typeof(RequestAgentSpawnPacket))]
    class RequestAgentSpawnHandler : IPacketHandler
    {
        public void Handle(int connectionId, IDataPacket packet)
        {
            RequestAgentSpawnPacket rasp = (RequestAgentSpawnPacket)packet;
            // Do some checks here later
            Agent newAgent = new Agent() {
                connectionId = connectionId,
                characterId = rasp.characterId,
                controllerPlayer = State.CurrentState.connectedPlayers[connectionId],
                lookPosition = new float[3] { 0, 0, 0 },
                position = new float[3] { 143.470f, 140.706f, -0.541f },
                pastPositions = new float[Program.BUFFER_SIZE, 3]
            };
            newAgent.pastPositions[0, 0] = 143.470f;
            newAgent.pastPositions[0, 1] = 140.706f;
            newAgent.pastPositions[0, 2] = -0.541f;
            State.CurrentState.agents[connectionId] = newAgent;
            State.CurrentState.agents[connectionId].index = State.CurrentState.LatestAgentIndex;
            State.CurrentState.agents[connectionId].isPlayer = true;
            State.CurrentState.agents[connectionId].health = 100;
            State.CurrentState.LatestAgentIndex++;
            foreach (int oConnectionId in State.CurrentState.connectedIds)
            {
                SpawnAgentPacket sap = new SpawnAgentPacket()
                {
                    CharacterId = rasp.characterId,
                    Controlled = oConnectionId == connectionId,
                    Location = newAgent.position,
                    mainHandEquipment = TaleWorlds.Core.EquipmentIndex.None,
                    offHandEquipment = TaleWorlds.Core.EquipmentIndex.None,
                    isPlayer = true,
                    agentIndex = State.CurrentState.agents[connectionId].index,
                    health = State.CurrentState.agents[connectionId].health
                };

                CommunicatorHelper.ServerSendPacket<SpawnAgentPacket>(oConnectionId, sap);
            }
            
        }
    }
}
