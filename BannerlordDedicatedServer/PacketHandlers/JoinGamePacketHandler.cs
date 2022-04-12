using BannerlordDedicatedServer.CustomAttributes;
using BannerlordDedicatedServer.Helpers;
using BannerlordDedicatedServer.NetworkPackets;
using BannerlordDedicatedServer.NetworkPackets.ClientPackets;
using BannerlordDedicatedServer.NetworkPackets.ServerPackets;
using BannerlordDedicatedServer.ServerState;
using BannerlordDedicatedServer.StateModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.PacketHandlers
{
    [Handles(typeof(JoinGamePacket))]
    class JoinGamePacketHandler: IPacketHandler
    {
        public void Handle(int connectionId, IDataPacket packet)
        {
            JoinGamePacket jgp = (JoinGamePacket)packet;
            Player player = new Player() { 
                AgentId = -1,
                ConnectionId = connectionId,
                PlayerName = jgp.PlayerName
            };
            State.CurrentState.connectedPlayers[connectionId] = player;
            Console.WriteLine("New player joined with name " + jgp.PlayerName);
            WelcomePacket welcomePacket = new WelcomePacket()
            {
                Message = "Sunucuya hoşgeldin adamım " + jgp.PlayerName
            };

            CommunicatorHelper.ServerSendPacket<WelcomePacket>(connectionId, welcomePacket);

            Console.WriteLine("Agent adedi "+State.CurrentState.agents.Count.ToString());

            foreach(Agent agent in State.CurrentState.agents.Values)
            {
                SpawnAgentPacket sap = new SpawnAgentPacket()
                {
                    CharacterId = agent.characterId,
                    Controlled = false,
                    Location = agent.position,
                    mainHandEquipment = agent.mainHandEquipmentIndex,
                    offHandEquipment = agent.offhandEquipmentIndex,
                    agentIndex = agent.index,
                    isPlayer = agent.isPlayer,
                    health = agent.health
                };

                CommunicatorHelper.ServerSendPacket<SpawnAgentPacket>(connectionId, sap);
            }
        }
    }
}
