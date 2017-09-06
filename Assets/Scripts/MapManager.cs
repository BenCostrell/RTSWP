using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public List<GameObject> platforms { get; private set; }
    public List<Building> buildings { get; private set; }
    [SerializeField]
    private int mineCount;
    [SerializeField]
    private int speedBoosterCount;
    [SerializeField]
    private int marketCount;
    [SerializeField]
    private float minSeparationDist;
    [SerializeField]
    private float maxTriesProcGen;
    [SerializeField]
    private Vector2 spawnPosOffset;
    [SerializeField]
    private Vector2 nexusSpawnPos;

	public void GetPlatforms()
    {
        platforms = new List<GameObject>();
        PlatformEffector2D[] platformEffectors = 
            Services.Main.GetComponentsInChildren<PlatformEffector2D>();
        foreach(PlatformEffector2D effector in platformEffectors)
        {
            platforms.Add(effector.gameObject);
        }
    }

    public void GenerateBuildings()
    {
        buildings = new List<Building>();
        SpawnBuilding(Building.BuildingType.Nexus);
        for (int i = 0; i < speedBoosterCount; i++)
        {
            SpawnBuilding(Building.BuildingType.SpeedBoost);
        }
        for (int i = 0; i < marketCount; i++)
        {
            SpawnBuilding(Building.BuildingType.Market);
        }
        for (int i = 0; i < mineCount; i++)
        {
            SpawnBuilding(Building.BuildingType.Mine);
        }
    }

    void SpawnBuilding(Building.BuildingType buildingType)
    {
        Building building = null;
        Vector2 pos = GenerateValidPosition();
        if (pos == Vector2.zero) return;
        switch (buildingType)
        {
            case Building.BuildingType.Nexus:
                building = Instantiate(Services.Prefabs.Nexus, Services.Main.transform).
                    GetComponent<Building>();
                pos = nexusSpawnPos;
                break;
            case Building.BuildingType.Mine:
                building = Instantiate(Services.Prefabs.Mine, Services.Main.transform).
                    GetComponent<Building>();
                break;
            case Building.BuildingType.SpeedBoost:
                building = Instantiate(Services.Prefabs.SpeedBooster, Services.Main.transform).
                    GetComponent<Building>();
                break;
            case Building.BuildingType.Market:
                building = Instantiate(Services.Prefabs.Market, Services.Main.transform).
                    GetComponent<Building>();
                break;
            default:
                break;
        }
        building.Init(pos);
        buildings.Add(building);
    }

    void GenerateMine(Vector2 pos)
    {
        Mine newMine = Instantiate(Services.Prefabs.Mine, Services.Main.transform).
            GetComponent<Mine>();
        newMine.Init(pos);
        buildings.Add(newMine);
    }

    Vector2 GenerateValidPosition()
    {
        Vector2 pos;
        bool isValid;
        for (int i = 0; i < maxTriesProcGen; i++)
        {
            pos = GenerateRandomPosition();
            isValid = IsPositionValid(pos);
            if (isValid) return pos;
        }
        return Vector2.zero;
    }

    Vector2 GenerateRandomPosition()
    {
        Vector2 pos;
        int platformIndex = Random.Range(0, platforms.Count);
        GameObject plat = platforms[platformIndex];
        float xPos = Random.Range(
            Services.Main.rightBoundary + 1,
            Services.Main.leftBoundary - 1);
        pos = new Vector2(xPos, plat.transform.position.y) + spawnPosOffset;

        return pos;
    }

    bool IsPositionValid(Vector2 pos)
    {
        for (int i = 0; i < Services.GameManager.numPlayers; i++)
        {
            if (Vector2.Distance(Services.Main.players[i].transform.position, pos)
                < minSeparationDist)
                return false;
        }
        for (int i = 0; i < buildings.Count; i++)
        {
            if (Vector2.Distance(buildings[i].transform.position, pos) < minSeparationDist)
                return false;
        }
        return true;
    }
}
