using System.Collections.Generic;
using Newtonsoft.Json;

namespace Storage
{

    /// <summary>
    /// Valid Races and Types for Buildings.
    /// Might be expanded in a future
    ///
    /// <remarks>
    /// Should something be added, append it as the last element, otherwise
    /// previously assigned gameobjects might get wrong types
    /// </remarks>
    /// </summary>
<<<<<<< HEAD
    public enum BuildingTypes { STRONGHOLD };
=======
    public enum BuildingTypes { FORTRESS, FARM, MINE, SAWMILL };
>>>>>>> origin/devel_d-resource_api

    public class BuildingInfo : EntityInfo
    {
        public BuildingTypes type = 0;

        [JsonConverter(typeof(BuildingAttributesDataConverter))]
        public override EntityAttributes attributes { get; set; }

        [JsonConverter(typeof(BuildingAttributesDataConverter))]
        public override List<EntityAction> actions { get; set; }

        public BuildingInfo()
        {
            actions = new List<EntityAction>();
        }
    }
}
