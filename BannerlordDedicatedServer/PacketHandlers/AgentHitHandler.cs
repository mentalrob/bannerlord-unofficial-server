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
using System.Runtime.InteropServices;
using System.Text;
using TaleWorlds.MountAndBlade;

namespace BannerlordDedicatedServer.PacketHandlers
{
    [Handles(typeof(AgentHitPacket))]
    class AgentHitHandler : IPacketHandler
    {
        private Blow fromBytes(byte[] array)
        {
            Blow b = new Blow();
            int size = Marshal.SizeOf(b);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(array, 0, ptr, size);

            b = (Blow)Marshal.PtrToStructure(ptr, b.GetType());
            Marshal.FreeHGlobal(ptr);
            return b;
        }
        public void Handle(int connectionId, IDataPacket packet)
        {
            AgentHitPacket ahp = (AgentHitPacket)packet;
            // Do some Hit validations here for AC
            //
            // ============================


            
            if(ahp.victimIndex >= 0)
            {
                Blow blow = fromBytes(ahp.blowData);
                StateModels.Agent targetAgent = State.CurrentState.agents.Values.ToList().Find((StateModels.Agent agent) => agent.index == ahp.victimIndex);
                if (targetAgent == null) return;
                targetAgent.health -= blow.InflictedDamage;
                if (targetAgent.health < 0) targetAgent.health = 0;

                ApplyAgentHitPacket aahp = new ApplyAgentHitPacket()
                {
                    attackerIndex = State.CurrentState.agents[connectionId].index,
                    blowData = ahp.blowData,
                    victimIndex = ahp.victimIndex
                };
                CommunicatorHelper.BroadCastPacket<ApplyAgentHitPacket>(aahp, (int _) => true);
            }
        }
    }
}
