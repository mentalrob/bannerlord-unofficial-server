using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.Server
{
    interface IHandler
    {
        public void OnConnection(int connectionId);

        public void OnData(int connectionId, ArraySegment<byte> message);

        public void OnDisconnect(int connectionId);
    }
}
