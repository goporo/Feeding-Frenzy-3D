using UnityEngine;
using System;

public class Fish : MonoBehaviour
{
    [SerializeField] private int size = 1;
    [SerializeField] private float damage = 0;
    [SerializeField] private float health = 0;
    [SerializeField] private float exp = 0;
    [SerializeField] private float maxExp = 0;
    [SerializeField] protected float swimSpeed = 3f;
    [SerializeField] protected float sprintSpeed = 20f;
    [SerializeField] private FishSpawner fishSpawner;
    private int level = 1;
    private float score = 0;
    private Animator fishAnimator;

    public event EventHandler onLevelUp;

    void Start()
    {
        fishAnimator = GetComponent<Animator>();
    }


    void Update()
    {

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
        if (this.GetComponentInParent<Player>())

        {
            if (otherFish && this.size > otherFish.size)
            // Player attack other fish
            {
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

            // Player get attacked by other fish
            else
            {
                // need some delay
                Debug.Log("Player get attacked -" + otherFish.damage + "HP");
                // decrease health instead
                // Endgame(this);
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
