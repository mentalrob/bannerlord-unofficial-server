using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;
using static TaleWorlds.MountAndBlade.Agent;

namespace BannerlordDedicatedServer.StateModels
{
    class Agent
    {
        public string characterId { get; set; }
        public Player controllerPlayer { get; set; }
        public int connectionId { get; set; }
        public int index { get; set; }
        public float[] position { get; set; }
        public float[] lookPosition { get; set; }
        public MovementControlFlag movementControlFlag { get; set; }
        public EventControlFlag eventControlFlag { get; set; }
        public EquipmentIndex mainHandEquipmentIndex = EquipmentIndex.None;
        public EquipmentIndex offhandEquipmentIndex = EquipmentIndex.None;

        public int health { get; set; }

        public float[,] pastPositions { get; set; }

        public Boolean isPlayer { get; set; }
    }
}
