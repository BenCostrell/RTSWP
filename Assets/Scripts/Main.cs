using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : Scene<TransitionData> {

    public List<Player> players { get; private set; }
    public Vector3[] playerSpawns;
    public Color[] playerColors;
    [SerializeField]
    private int numPlatforms;
    [SerializeField]
    private Vector3 platformBasePos;
    [SerializeField]
    private Vector3 platformSpacing;
    public LayerMask groundLayer;
    public float rightBoundary;
    public float leftBoundary;
    public float botBoundary;
    public float topOfScreen;

    internal override void OnEnter(TransitionData data)
    {
        InitializeServices();
        InitializePlayers();
        GenerateMap();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void InitializeServices()
    {
        Services.Main = this;
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

    void GenerateMap()
    {
        //for (int i = 0; i < numPlatforms; i++)
        //{
        //    Instantiate(Services.Prefabs.Platform,
        //        platformBasePos + i * platformSpacing,
        //        Quaternion.identity,
        //        Services.Main.transform);
        //}
    }
}
