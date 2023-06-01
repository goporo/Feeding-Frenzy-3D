// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishV0Prefab;
    [SerializeField] private GameObject fishV1Prefab;
    [SerializeField] private GameObject fishV2Prefab;
    [SerializeField] private GameObject fishV3Prefab;
    [SerializeField] private GameObject fishV4Prefab;
    [SerializeField] private GameObject sharkPrefab;
    [SerializeField] private Player player;
    [SerializeField] private static Vector3 playerPosition;
    Fish fish;

    // private int numberOfFishInReSpawner = 0;
    private const int TOTAL_FISH = 20;
    [SerializeField] private GameObject[] fishSpawner;
    private void Awake()
    {
        fishSpawner = new GameObject[TOTAL_FISH];
        Debug.Log(fishSpawner.Length);
        for (int i = 0; i < TOTAL_FISH; i++)
        {
            fishSpawner[i]=ChooseRandomFish(UnityEngine.Random.Range(0,5));
            fish = fishSpawner[i].GetComponent<Fish>();
            // fish.onEating+=Test;
            fish.transform.localScale = new Vector3(fish.GetSize(),fish.GetSize(),fish.GetSize());
        }
    }
    private void Start()
    {
        for (int i = 0; i < TOTAL_FISH; i++)
        {
            fishSpawner[i]= Instantiate(fishSpawner[i], new Vector3(UnityEngine.Random.Range(-30,30),UnityEngine.Random.Range(-25,25), UnityEngine.Random.Range(-200,200)), Quaternion.identity);
        }
    }
    public static void RespawnFish(Fish otherFish)
    {
        // otherFish.visualObject=sharkPrefab;
        int newSize = UnityEngine.Random.Range(1,5);
        otherFish.SetSize(newSize);
        otherFish.transform.localScale = new Vector3(otherFish.GetSize(),otherFish.GetSize(),otherFish.GetSize());
        otherFish.transform.position=new Vector3(GetX(),UnityEngine.Random.Range(-25,25),GetZ());
        Debug.Log("Respawn at " + otherFish.transform.position);
        otherFish.visualObject.SetActive(true);
    }
    private static float GetX(){
        float x = UnityEngine.Random.Range(-30,30);
        // Debug.Log(x);
        while(Math.Abs(x-playerPosition.x)<20){
            x = UnityEngine.Random.Range(-30,30);
        }
        return x;
    }
    private static float GetZ(){
        float z = UnityEngine.Random.Range(-200,-200);
        while(Math.Abs(z-playerPosition.z)<20){
            z = UnityEngine.Random.Range(-200,-200);
        }
        return z;
    }
    private GameObject ChooseRandomFish(int index){
        switch (index)
        {
            case 0: return fishV0Prefab; 
            case 1: return fishV1Prefab; 
            case 2: return fishV2Prefab; 
            case 3: return fishV3Prefab; 
            case 4: return fishV4Prefab; 
            default: return sharkPrefab;
        }
    }
    private void Update() {
        playerPosition = player.transform.position;
    }
}
