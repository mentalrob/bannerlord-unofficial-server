using System;
using System.Collections.Generic;
using System.Text;

namespace BannerlordDedicatedServer.CustomAttributes
{
    public class HandlesAttribute : Attribute
    {
        public Type handles { get; set; }
        public HandlesAttribute(Type _handles)
        {
            this.handles = _handles;
        }
    }
}
