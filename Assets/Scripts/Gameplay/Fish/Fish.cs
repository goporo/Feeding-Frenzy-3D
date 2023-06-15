using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fish : MonoBehaviour
{
    // [SerializeField] FishType 
    [SerializeField] private int size = 1;
    [SerializeField] private float sprintSpeed; // speed when left shift
    [SerializeField] private float damage = 10;
    [SerializeField] private float health = 100;
    [SerializeField] private float exp = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private float maxExp = 3;
    [SerializeField] public float swimSpeed = 3f;
    [SerializeField] private float score = 0;
    [SerializeField] public GameObject visualObject;
    [SerializeField] public FishSpawner fishSpawner;
    private Animator fishAnimator;

    public event EventHandler onLevelUp;
    // public event EventHandler<onEatingObject> onEating;
    // public class onEatingObject: EventArgs{
    //     public Fish fish;
    // }
    // Start is called before the first frame update
    void Start()
    {
        fishAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // this.size += 0.1f * Time.deltaTime;
        // onEating?.Invoke(this,EventArgs.Empty);
    }
    public void SetIsSwimming(bool value)
    {
        fishAnimator?.SetBool("isSwimming", value);
    }
    public void SetAnimationSpeed(float playSpeed = 1f)
    {
        fishAnimator.speed = playSpeed;
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
        // Debug.Log("Fish ate" + otherFish.name + " EXP got: " + otherFish.exp);

        if (otherFish && this.size > otherFish.size)
        {
            if (otherFish.GetComponentInParent<Player>())
            {
                // decrease health instead
                // Endgame(otherFish);
            }

            // Disable fish and then respawn them
            fishSpawner.DeactivateFish(otherFish);

            // Take exp from otherFish
            this.exp += otherFish.exp;
            this.score += 10 + otherFish.level * 10;
            if (this.exp >= this.maxExp)
            {
                // if exp > Max exp in this level, Levelup this fish
                LevelUp();
            }
        }
    }
    public void TakePresent()
    {
        this.swimSpeed *= 2;
    }
    private void Endgame(Fish otherFish)
    {
        Time.timeScale = 0;
        otherFish.GetComponentInParent<Player>().GetEndGameCamera().OpenEndGameMenu();
        otherFish.GetComponentInParent<Player>().gameObject.SetActive(false);
    }
    public void LevelUp()
    {
        this.exp = this.exp - this.maxExp;
        this.level += 1;
        this.maxExp = this.level * 2;
        // this.size = this.level;
        // Debug.Log("Level Up Fish");
        onLevelUp?.Invoke(this, EventArgs.Empty);
    }
    public int GetSize()
    {
        return this.size;
    }
    public void SetSize(int size)
    {
        this.size = size;
    }
    public int GetLevel()
    {
        return this.level;
    }
    public void SetLevel(int level)
    {
        this.level = level;
    }
    public float GetExp()
    {
        return this.exp;
    }
    public float GetMaxExp()
    {
        return this.maxExp;
    }
    public float GetScore()
    {
        return this.score;
    }
}
