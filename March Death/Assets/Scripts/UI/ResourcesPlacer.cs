﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Storage;
using System.Collections.Generic;
using Utils;

public class ResourcesPlacer : MonoBehaviour
{
    // attributes

    private readonly string[] txt_names = { "meat", "wood", "metal" };
    private WorldResources.Type[] t = { WorldResources.Type.FOOD, WorldResources.Type.WOOD, WorldResources.Type.METAL };
    private List<Text> res_amounts;
    private Player player;



    void Start()
    {
        res_amounts = new List<Text>();

        player = GameObject.Find("GameController").GetComponent<Player>();

        GameObject gameInformationObject = GameObject.Find("GameInformationObject");

        for (int i = 0; i < txt_names.Length; i++)
        {
            res_amounts.Add(GameObject.Find("HUD/resources/text_" + txt_names[i]).GetComponent<Text>());
        }

        setupText();
        updateAll();

        Subscriber<Selectable.Actions, Selectable>.get.registerForAll(Selectable.Actions.CREATED, onUnitCreated, new ActorSelector()
        {
            registerCondition = (checkRace) => checkRace.GetComponent<IGameEntity>().info.race == gameInformationObject.GetComponent<GameInformation>().GetPlayerRace()
        });

    }

    void Update(){ }

    // PUBLIC METHODS

    /// <summary>
    /// Substracts the costs for the unit from the players deposit.
    /// </summary>
    /// <param name="entity"></param>
    public void updateUnitCreated(IGameEntity entity)
    {
        Debug.Log(entity.info.entityType);
        player.resources.SubstractAmount(WorldResources.Type.FOOD, entity.info.resources.food);
        player.resources.SubstractAmount(WorldResources.Type.WOOD, entity.info.resources.wood);
        player.resources.SubstractAmount(WorldResources.Type.METAL, entity.info.resources.metal);

        updateAll();
    }

    public void insufficientFundsColor(IGameEntity entity)
    {
        if (entity.info.resources.food > player.resources.getAmount(WorldResources.Type.FOOD))
            res_amounts[0].color = Color.red;
        if (entity.info.resources.wood > player.resources.getAmount(WorldResources.Type.WOOD))
            res_amounts[1].color = Color.red;
        if (entity.info.resources.metal > player.resources.getAmount(WorldResources.Type.METAL))
            res_amounts[2].color = Color.red;
    }

    public void clearColor()
    {
        foreach (Text t in res_amounts)
            t.color = Color.white;
    }

    // PRIVATE METHODS

    /// <summary>
    /// Checks if the game object is a hero or a stronghold. (Improve)
    /// </summary>
    /// <param name="ige"></param>
    /// <returns></returns>
    private bool isStarter(IGameEntity ige)
    {
        if (ige.info.isBuilding)
        {
            if (ige.info.isBarrack) return true;
            return false;
        }
        else
        {
            if (ige.info.isCivil) return false;
            return true;
        }
    }


    private void updateAll()
    {
        for (int i = 0; i < txt_names.Length; i++)
        {
            res_amounts[i].text = "" + player.resources.getAmount(t[i]);
        }
    }

    private void setupText()
    {
        foreach (Text t in res_amounts) {
            t.color = Color.white;
            t.fontStyle = FontStyle.BoldAndItalic;
        }
    }


    // EVENTS

    void onUnitCreated(System.Object obj)
    {

        GameObject go = (GameObject)obj;


        if (go)
        {
            IGameEntity i_game = go.GetComponent<IGameEntity>();

            if (!isStarter(i_game))
                updateUnitCreated(go.GetComponent<IGameEntity>());
        }
    }
}