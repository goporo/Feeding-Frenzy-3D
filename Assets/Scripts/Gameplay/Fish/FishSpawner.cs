using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishV0Prefab;
    [SerializeField] private GameObject fishV1Prefab;
    [SerializeField] private GameObject fishV2Prefab;
    [SerializeField] private GameObject fishV3Prefab;
    [SerializeField] private Player player;

    private const int TOTAL_FISH = 500;
    private const int XZ_BOUNDARY = 450;
    private const int Y_BOUNDARY = 70;

    private const int MAX_ACTIVE_FISH = 500;

    private List<Fish> fishPool;
    private List<Fish> activeFish;

    private void Awake()
    {
        fishPool = new List<Fish>();
        activeFish = new List<Fish>();

        for (int i = 0; i < TOTAL_FISH; i++)
        {
            GameObject fishPrefab = ChooseRandomFish();
            Fish fish = InstantiateFish(fishPrefab);
            fish.gameObject.SetActive(false);
            fishPool.Add(fish);
        }
    }

    private void Start()
    {
        for (int i = 0; i < MAX_ACTIVE_FISH; i++)
        {
            ActivateFish();
        }
    }

    private void ActivateFish()
    {
        if (activeFish.Count >= MAX_ACTIVE_FISH)
            return;

        Fish fish = GetInactiveFishFromPool();
        if (fish != null)
        {
            fish.transform.position = GetRandomFishPosition();
            fish.gameObject.SetActive(true);
            activeFish.Add(fish);
        }
    }

    private Vector3 GetRandomFishPosition()
    {
        Vector3 position;
        float minDistanceSquared = 20f * 20f; // Minimum squared distance to player

        while (true)
        {
            position = new Vector3(
                UnityEngine.Random.Range(-XZ_BOUNDARY, XZ_BOUNDARY),
                UnityEngine.Random.Range(-Y_BOUNDARY, Y_BOUNDARY),
                UnityEngine.Random.Range(-XZ_BOUNDARY, XZ_BOUNDARY)
            );

            // Sample the terrain height at the position
            float terrainHeight = Terrain.activeTerrain.SampleHeight(position);
            // Ensure the fish is spawned above the terrain
            if (position.y > terrainHeight + Terrain.activeTerrain.transform.position.y)
            {
                // Calculate squared distance between position and player position
                float distanceSquared = (position - player.transform.position).sqrMagnitude;

                if (distanceSquared >= minDistanceSquared)
                {
                    // Valid position found, break the loop
                    break;
                }
            }
        }

        return position;
    }


    private Fish GetInactiveFishFromPool()
    {
        foreach (Fish fish in fishPool)
        {
            if (!fish.gameObject.activeSelf)
            {
                return fish;
            }
        }
        return null;
    }

    private GameObject ChooseRandomFish()
    {
        float choice = UnityEngine.Random.value;
        if (choice <= 0.6f)
            return fishV0Prefab;
        else if (choice <= 0.85f)
            return fishV1Prefab;
        else if (choice <= 0.96f)
            return fishV2Prefab;
        else
            return fishV3Prefab;
    }

    private Fish InstantiateFish(GameObject fishPrefab)
    {
        GameObject fishObject = Instantiate(fishPrefab, transform);
        Fish fish = fishObject.GetComponent<Fish>();
        return fish;
    }

    public void DeactivateFish(Fish fish)
    {
        fish.gameObject.SetActive(false);
        activeFish.Remove(fish);
        ActivateFish();
    }
}
