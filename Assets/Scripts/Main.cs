using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : Scene<TransitionData> {

    public List<Player> players { get; private set; }
    public Vector3[] playerSpawns;
    public Color[] playerColors;
    public LayerMask groundLayer;
    public float rightBoundary;
    public float leftBoundary;
    public float botBoundary;
    public float topOfScreen;

    internal override void OnEnter(TransitionData data)
    {
        InitializeServices();
        InitializePlayers();
        Services.MapManager.GetPlatforms();
        Services.MapManager.GenerateBuildings();
        Services.ResourceManager.GenerateInitialResources();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void InitializeServices()
    {
        Services.Main = this;
        Services.ResourceManager = GetComponentInChildren<ResourceManager>();
        Services.MapManager = GetComponentInChildren<MapManager>();
    }

    void InitializePlayers()
    {
        players = new List<Player>();
        for (int i = 0; i < Services.GameManager.numPlayers; i++)
            players.Add(InitializePlayer(i + 1));
    }

    Player InitializePlayer(int playerNum)
    {
        GameObject playerObj = Instantiate(Services.Prefabs.Player, Services.Main.transform);
        Player player = playerObj.GetComponent<Player>();
        player.Init(playerNum);
        return player;
    }

    public void GameWin(Player player)
    {
        Debug.Log("player " + player.playerNum + " has won");
    }
}
