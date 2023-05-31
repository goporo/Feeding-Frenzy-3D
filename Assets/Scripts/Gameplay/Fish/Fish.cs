using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fish : MonoBehaviour
{
    // [SerializeField] FishType 
    [SerializeField] private float size = 1;
    [SerializeField] protected float speed;
    [SerializeField] private float sprintSpeed; // speed when left shift
    [SerializeField] private float damage = 10;
    [SerializeField] private float health = 100;
    [SerializeField] private float exp = 0;
    [SerializeField] private float level = 1;
    [SerializeField] private float maxExp = 2;
    [SerializeField] public GameObject visualObject; // Object that hold the fish
    public event EventHandler onLevelUp;
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        // this.size += 0.1f * Time.deltaTime;
        // onEating?.Invoke(this,EventArgs.Empty);
    }
    public void Attack(Fish otherFish)
    {
        // otherFish.health -= this.damage;
        // if(otherFish.health <= 0){
        //     this.Eat(otherFish);
        // }
    }
    public void Eat(Fish otherFish)
    {
        Debug.Log("Fish ate");
        Debug.Log(otherFish);
        if (this.size >= otherFish.size)
        {
            otherFish.visualObject.SetActive(false);
            //     onEating?.Invoke(this, new FishEaten
            // {
            //     otherFish = otherFish
            // });
            FishSpawner.RespawnFish(otherFish);
            // Take exp from otherFish
            this.exp += 1;
            this.exp += otherFish.level;
            if (this.exp >= this.maxExp)
            {
                // if exp > Max exp in this level, Levelup this fish
                LevelUp(otherFish);
            }
        }
    }

    private void LevelUp(Fish otherFish)
    {
        this.exp = this.exp - this.maxExp;
        this.level += 1;
        this.maxExp = this.level * 2;
        this.size = this.level;
        Debug.Log("Level Up Fish");
        onLevelUp?.Invoke(this, EventArgs.Empty);
    }
    public float GetSize()
    {
        return this.size;
    }
}
