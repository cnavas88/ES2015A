using UnityEngine;
using System.Collections;
using Storage;

public class GameInformation : MonoBehaviour {

    private Races playerRace;
    private GameObject currentHud = null;

    private static string pauseMenuPrefab;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadHUD()
    {
        if (currentHud) Destroy(currentHud);
        switch (playerRace)
        {
        case Races.ELVES:
            LoadElfHUD();
            break;
        case Races.MEN:
            LoadHumanHUD();
            break;
        }
    }

    private void LoadElfHUD()
    {

        currentHud = Instantiate((GameObject)Resources.Load ("HUD-Elf"));
        currentHud.name = "HUD";
        Instantiate((GameObject)Resources.Load ("HUD_EventSystem")).name = "HUD_EventSystem";
    }

    private void LoadHumanHUD()
    {
        currentHud = Instantiate((GameObject)Resources.Load("HUD-Human"));
        currentHud.name = "HUD";
        //Application.LoadLevelAdditive("globalHUDHuman");
        Instantiate((GameObject)Resources.Load ("HUD_EventSystem")).name = "HUD_EventSystem";
    }

    public void SetPlayerRace(int race)
    {
        playerRace = (Races) race;
        switch(playerRace)
        {
            case Races.ELVES:
                pauseMenuPrefab = "PauseMenu-Elf";
                break;
            case Races.MEN:
                pauseMenuPrefab = "PauseMenu-Human";
                break;
        }
    }

    public void SetPlayerRace(Races race)
    {
        playerRace = race;
        switch(playerRace)
        {
            case Races.ELVES:
                pauseMenuPrefab = "PauseMenu-Elf";
                break;
            case Races.MEN:
                pauseMenuPrefab = "PauseMenu-Human";
                break;
        }
    }

    public Races GetPlayerRace()
    {
        return playerRace;
    }

    public string GetPauseMenuPrefabPath()
    {
        return pauseMenuPrefab;
    }
}
