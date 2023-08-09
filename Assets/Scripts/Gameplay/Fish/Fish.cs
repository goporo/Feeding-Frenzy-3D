using UnityEngine;
using System;

public class Fish : MonoBehaviour
{
    [SerializeField] private int size = 1;
    [SerializeField] private float damage = 0;
    [SerializeField] private float exp = 0;
    [SerializeField] protected float swimSpeed = 3f;
    [SerializeField] protected float sprintSpeed = 20f;
    [SerializeField] private FishSpawner fishSpawner;
    [SerializeField] private ParticleSystem bloodSplatterEffect;


    private float score = 0;
    private Animator fishAnimator;


    void Start()
    {
        fishAnimator = GetComponent<Animator>();
    }

    public void SetIsSwimming(bool value)
    {
        fishAnimator?.SetBool("isSwimming", value);
    }
    public void SetAnimationSpeed(float playSpeed = 1f)
    {
        fishAnimator.speed = playSpeed;
    }

    public void Eat(Fish otherFish)
    {
        if (!this.GetComponentInParent<Player>()) return;

        Player player = this.GetComponentInParent<Player>();
        player.PlayBiteSound();
        if (this.size > otherFish.size)
        // Player attack other fish
        {
            Instantiate(bloodSplatterEffect, this.transform.position, Quaternion.identity);
            fishSpawner.DeactivateFish(otherFish);

            player.exp += otherFish.exp;
            player.SetHealth(otherFish.exp * 6);
            player.GetExpUIController().OnPlayerEating(otherFish.exp);
            player.score += otherFish.exp * 10;
            if (player.exp >= player.maxExp)

            {
                player.LevelUp();
            }
        }

        // Player get attacked by other fish
        else
        {
            player.SetHealth(-otherFish.damage);
        }

    }
    public int GetSize()
    {
        return this.size;
    }
    public void SetSize(int size)
    {
        this.size = size;
    }
    public float GetExp()
    {
        return exp;
    }
    public float GetScore()
    {
        return score;
    }
}
