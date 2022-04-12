using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.CustomAttributes
{
    public class PacketIdAttribute : Attribute
    {
        public int id{ get; set; }
        public PacketIdAttribute(int _id)
        {
            this.id = _id;
        }
    }
}
