using BannerlordDedicatedServer.NetworkPackets;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.PacketHandlers
{
    interface IPacketHandler
    {
        public void Handle(int connectionId, IDataPacket packet);

    }
}
