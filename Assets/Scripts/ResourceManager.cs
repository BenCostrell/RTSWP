using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    private List<Resource> resources;
    [SerializeField]
    private int initialResourceCount;
    [SerializeField]
    private float resourceSpawnRate;
    [SerializeField]
    private Vector2 spawnPosOffset;
    [SerializeField]
    private float minSeparationDist;
    [SerializeField]
    private int maxTriesProcGen;
    private float timeSinceResourceSpawned;

    private void Update()
    {
        timeSinceResourceSpawned += Time.deltaTime;
        if (timeSinceResourceSpawned >= resourceSpawnRate)
        {
            bool succesfulSpawn = SpawnResource();
            if (succesfulSpawn)
            {
                timeSinceResourceSpawned = 0;
            }
        }
    }

    public void GenerateInitialResources()
    {
        resources = new List<Resource>();
        for (int i = 0; i < initialResourceCount; i++)
        {
            bool successfulSpawn = SpawnResource();
            if (!successfulSpawn) break;
        }
    }

    bool SpawnResource()
    {
        Vector2 pos = GenerateValidPosition();
        if (pos == Vector2.zero) return false;
        else
        {
            CreateResource(pos);
            return true;
        }
    }

    void CreateResource(Vector2 pos)
    {
        Resource newResource = 
            GameObject.Instantiate(Services.Prefabs.Resource, Services.Main.transform)
            .GetComponent<Resource>();
        newResource.Init(1, pos);
        resources.Add(newResource);
    }

    public void DestroyResource(Resource resource)
    {
        resources.Remove(resource);
        GameObject.Destroy(resource.gameObject);
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
        int platformIndex = Random.Range(0, Services.MapManager.platforms.Count);
        GameObject plat = Services.MapManager.platforms[platformIndex];
        float xPos = Random.Range(
            Services.Main.rightBoundary + 1,
            Services.Main.leftBoundary - 1);
        pos = new Vector2(xPos, plat.transform.position.y) + spawnPosOffset;

        return pos;
    }

    bool IsPositionValid(Vector2 pos)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            if (Vector2.Distance(resources[i].transform.position, pos) < minSeparationDist)
                return false;
        }
        for (int i = 0; i < Services.GameManager.numPlayers; i++)
        {
            if (Vector2.Distance(Services.Main.players[i].transform.position, pos) 
                < minSeparationDist)
                return false;
        }
        for (int i = 0; i < Services.MapManager.buildings.Count; i++)
        {
            if (Vector2.Distance(Services.MapManager.buildings[i].transform.position, pos) 
                < minSeparationDist)
                return false;
        }
        return true;
    }
}
