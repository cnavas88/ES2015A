﻿using UnityEngine;
using System.Collections;
using Storage;

public class Main_Game : MonoBehaviour {

	private GameInformation info;
	Transform strongholdTransform;

	public GameObject playerStronghold;
    public GameObject playerHero;

    private Player player;

    // Use this for initialization
    void Start () {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        player = gameController.GetComponent<Player>();

        strongholdTransform = GameObject.Find("PlayerStronghold").transform;
        playerHero = GameObject.Find("PlayerHero");
        if(GameObject.Find("GameInformationObject"))
		    info = (GameInformation) GameObject.Find("GameInformationObject").GetComponent("GameInformation");
		LoadPlayerStronghold();
        LoadPlayerUnits();
        if(info)info.LoadHUD();
    }

	private void LoadPlayerStronghold()
	{
        if (info)
        {
            // TODO Add stronghold reference to the player
            playerStronghold = Info.get.createBuilding(info.GetPlayerRace(),
                                                       BuildingTypes.STRONGHOLD,
                                                   strongholdTransform.position, strongholdTransform.rotation);

            IGameEntity entity = playerStronghold.GetComponent<IGameEntity>();
            player.addEntity(entity);
        }
	}

    private void LoadPlayerUnits()
    {
        if (info)
        {
            // TODO Must be able to load other kinds of units (both civilian and military)
            playerHero = Info.get.createUnit(info.GetPlayerRace(),
                                             UnitTypes.HERO, playerHero.transform.position,
                                         playerHero.transform.rotation);

            IGameEntity entity = playerHero.GetComponent<IGameEntity>();
            player.addEntity(entity);
        }
    }
}
