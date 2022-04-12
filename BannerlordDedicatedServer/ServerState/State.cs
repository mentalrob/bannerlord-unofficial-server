using BannerlordDedicatedServer.StateModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.ServerState
{
    class State
    {
        public int LatestAgentIndex = 0;
        public static State CurrentState;

        // public List<Player> connectedPlayers = new List<Player>();
        public Dictionary<int, Player> connectedPlayers = new Dictionary<int, Player>();
        public Dictionary<int, Agent> agents = new Dictionary<int, Agent>();
        public List<int> connectedIds = new List<int>();
        
        public State()
        {
            State.CurrentState = this;
        }


        public void DisconnectPlayer(Player p)
        {

        }
    }
}
