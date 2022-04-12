using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.NetworkPackets
{
    class NetworkPacketRegistry
    {
        public static Dictionary<int, Type> storage = new Dictionary<int, Type>();
    }
}
