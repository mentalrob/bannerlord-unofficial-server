using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.PacketHandlers
{
    class HandlerRegistry
    {
        public static Dictionary<Type, IPacketHandler> storage = new Dictionary<Type, IPacketHandler>();
    }
}
