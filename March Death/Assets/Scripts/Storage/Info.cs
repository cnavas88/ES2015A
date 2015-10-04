﻿using UnityEngine;

using System;
using System.Collections.Generic;
using System.IO;

using Utils;
using Newtonsoft.Json;

namespace Storage
{
    /// <summary>
    /// Info singleton class might be used to query information of a given
    /// unit race and type.
    /// It automatically parses all units on Assets/Units and stores it.
    /// </summary>
    sealed class Info : Singleton<Info>
    {
        private Dictionary<Tuple<Races, UnitTypes>, UnitInfo> unitStore = new Dictionary<Tuple<Races, UnitTypes>, UnitInfo>();

        /// <summary>
        /// Private constructor, singleton access only
        /// <remarks>Use Info.get instead</remarks>
        /// </summary>
        private Info()
        {
            parseUnitFiles();
        }

        /// <summary>
        /// Parses all unit files on "Assets/Units".
        /// <exception cref="System.FileLoadException">
        /// Thrown when a unit file is not valid or has already been added
        /// </exception>
        /// </summary>
        private void parseUnitFiles()
        {
            DirectoryInfo info = new DirectoryInfo("Assets/Data/Units");
            var fileInfo = info.GetFiles();
            foreach (var file in fileInfo)
            {
                if (file.FullName.Length > 4 && file.FullName.Substring(file.FullName.Length - 4).Equals("json"))
                {
                    try
                    {
                        string json = File.ReadAllText(file.FullName);
                        UnitInfo unitInfo = JsonConvert.DeserializeObject<UnitInfo>(json);
                        unitInfo.entityType = EntityType.UNIT;

                        Tuple<Races, UnitTypes> key = new Tuple<Races, UnitTypes>(unitInfo.race, unitInfo.type);

                        if (unitStore.ContainsKey(key))
                        {
                            throw new FileLoadException("Unit info '" + file.Name + "' (" + file.FullName + ") already exists");
                        }

                        unitStore.Add(key, unitInfo);
                    }
                    catch (JsonException e)
                    {
                        throw new FileLoadException("Unit info '" + file.Name + "' (" + file.FullName + ") is invalid\n\t" + e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Gathers information for a race and type.
        /// </summary>
        /// <param name="race">Race to look for</param>
        /// <param name="type">Type to look for</param>
        /// <exception cref="System.NotImplementedException">Thrown when a race/type combination is not found</exception>
        /// <returns>The UnitInfo object of that race/type combination</returns>
        public UnitInfo of(Races race, UnitTypes type)
        {
            Tuple<Races, UnitTypes> key = new Tuple<Races, UnitTypes>(race, type);

            if (!unitStore.ContainsKey(key))
            {
                throw new NotImplementedException("Race (" + race + ") and Type (" + type + ") does not exist");
            }

            return unitStore[key];
        }

        /// <summary>
        /// Creates a Unit of a given race and type from a prefab
        /// </summary>
        /// <param name="prefab">Prefab path, usually "Media/UsingPrefabs/NAME"</param>
        /// <param name="race">Race of the Unit</param>
        /// <param name="type">Type of the Unit</param>
        /// <returns>The created GameObject</returns>
        public GameObject createUnit(String prefab, Races race, UnitTypes type)
        {
            GameObject gob = UnityEngine.Object.Instantiate((GameObject)Resources.Load(prefab, typeof(GameObject)));
            return postCreateUnit(gob, race, type);
        }

        /// <summary>
        /// Creates a Unit of a given race and type from a prefab in a certain position and rotation
        /// </summary>
        /// <param name="prefab">Prefab path, usually "Media/UsingPrefabs/NAME"</param>
        /// <param name="race">Race of the Unit</param>
        /// <param name="type">Type of the Unit</param>
        /// <param name="position">Unit position</param>
        /// <param name="rotation">Unit rotation</param>
        /// <returns>The created GameObject</returns>
        public GameObject createUnit(String prefab, Races race, UnitTypes type, Vector3 position, Quaternion rotation)
        {
            UnityEngine.Object gob = UnityEngine.Object.Instantiate((GameObject)Resources.Load(prefab, typeof(GameObject)), position, rotation);
            return postCreateUnit((GameObject)gob, race, type);
        }

        /// <summary>
        /// Sets the race and type to a GameObject which has not yet been
        /// placed on scene and, thus, its Start hasn't been called yet
        /// </summary>
        /// <param name="gob">GameObject</param>
        /// <param name="race">Race of the Unit</param>
        /// <param name="type">Type of the Unit</param>
        /// <returns>The set GameObject</returns>
        private GameObject postCreateUnit(GameObject gob, Races race, UnitTypes type)
        {
            gob.GetComponent<Unit>().race = race;
            gob.GetComponent<Unit>().type = type;
            return gob;
        }
    }
}