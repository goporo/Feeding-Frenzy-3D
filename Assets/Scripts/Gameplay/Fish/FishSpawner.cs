// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fishV0Prefab;
    [SerializeField] private GameObject fishV1Prefab;
    [SerializeField] private GameObject fishV2Prefab;
    [SerializeField] private GameObject fishV3Prefab;
    [SerializeField] private GameObject fishV4Prefab;
    [SerializeField] private GameObject sharkPrefab;
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
            fishSpawner[i]=ChooseRandomFish(Random.Range(0,5));
            fish = fishSpawner[i].GetComponent<Fish>();
            fish.transform.localScale = new Vector3(fish.GetSize()*2,fish.GetSize()*2,fish.GetSize()*2);
        }
    }
    private void Start()
    {
        for (int i = 0; i < TOTAL_FISH; i++)
        {
            fishSpawner[i]= Instantiate(fishSpawner[i], new Vector3(Random.Range(-15,15),Random.Range(-15,15), Random.Range(-15,15)), Quaternion.identity);
        }
    }
    // public static void RespawnFishSchedule(Fish otherFish){
    //     Invoke("RespawnFish",20f);
    // }
    public static void RespawnFish(Fish otherFish)
    {
        // otherFish.visualObject=sharkPrefab;
        otherFish.transform.position=new Vector3(Random.Range(-15,15),Random.Range(-15,15),Random.Range(-15,15));
        otherFish.visualObject.SetActive(true);
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
}
