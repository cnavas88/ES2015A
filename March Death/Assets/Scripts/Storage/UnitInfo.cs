using System;
﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Storage
{

    /// <summary>
    /// Valid Races and Types for Units.
    /// Might be expanded in a future
    ///
    /// <remarks>
    /// Should something be added, append it as the last element, otherwise
    /// previously assigned gameobjects might get wrong types
    /// </remarks>
    /// </summary>
    public enum UnitTypes { FARMER, MINER, LUMBERJACK, HERO, LIGHT, HEAVY, THROWN, CAVALRY, MACHINE, SPECIAL };
    public enum PrefabUnitTypes { CIVIL_1, CIVIL_2, HERO, LIGHT, HEAVY, THROWN, CAVALRY, MACHINE, SPECIAL };

    public class UnitInfo : EntityInfo
    {
        public UnitTypes type = 0;

        [JsonConverter(typeof(UnitAttributesDataConverter))]
        public override EntityAttributes attributes { get; set; }

        [JsonConverter(typeof(UnitActionsDataConverter))]
        public override List<EntityAbility> abilities { get; set; }

        public override T getType<T>()
        {
            return (T)Convert.ChangeType(type, typeof(T));
        }

        public UnitInfo()
        {
            abilities = new List<EntityAbility>();
        }
    }
}
