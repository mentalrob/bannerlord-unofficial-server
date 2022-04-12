# Non Official Bannerlord Dedicated Server

It uses telepathy as a TCP server. Packets can be found at NetworkPackets sub folder. Protobuf is used for serialization and deserialization.

## How to create a custom packet

### Client Packets

Client packets are packets that sent by client. In server side each client packet must have a `Handler` and also it should be represented with a unique `PacketId`.

For example;

```csharp
using BannerlordDedicatedServer.CustomAttributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets.ClientPackets
{
    [
     ProtoContract, // Mark this class as a Protobuf
     PacketId(1) // Unique packet id
    ]
    class JoinGamePacket: IDataPacket
    {
        [ProtoMember(1)]
        public string PlayerName { get; set; } // Content of the packet. (one of the attributes)
    }
}
```

To handle this client packet we need a handler.

```csharp
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
    [Handles(typeof(JoinGamePacket))] // Set handler target to JoinGamePacket so this handler will only handle packet JoinGamePacket
    class JoinGamePacketHandler: IPacketHandler
    {
        public void Handle(int connectionId, IDataPacket packet)
        {
            JoinGamePacket jgp = (JoinGamePacket)packet; // Convert packet to JoinGamePacket
            Player player = new Player() {
                AgentId = -1,
                ConnectionId = connectionId,
                PlayerName = jgp.PlayerName
            }; // Create a player object for to save on state
            State.CurrentState.connectedPlayers[connectionId] = player; // Save it on state with connectionId
            Console.WriteLine("New player joined with name " + jgp.PlayerName);
            WelcomePacket welcomePacket = new WelcomePacket()
            {
                Message = "Sunucuya hoşgeldin adamım " + jgp.PlayerName
            }; // Craft a server packet WelcomePacket

            CommunicatorHelper.ServerSendPacket<WelcomePacket>(connectionId, welcomePacket); // Send the packet to the client

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
                }; // Craft a spawn agent packet

                CommunicatorHelper.ServerSendPacket<SpawnAgentPacket>(connectionId, sap); // Send already spawned agents to the client
            }
        }
    }
}
```

## Server Packets

Server packets has same structure with client packets. If you want to create a server packet, you need to write a handler on Client side.

# Note: Currently this repo is not maintained.
