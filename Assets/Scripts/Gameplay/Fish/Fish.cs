using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // [SerializeField] FishType 
    [SerializeField] private float size = 1;
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed; // speed when left shift
    [SerializeField] private float damage = 10;
    [SerializeField] private float health = 100;
    [SerializeField] private float exp = 0;
    [SerializeField] private float level = 1;
    [SerializeField] private GameObject visualObject; // Object that hold the fish



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        otherFish.visualObject.SetActive(false);
        this.exp += 1;
        this.exp += otherFish.level;
        if(this.exp >= this.level*2){
            this.exp = this.exp - this.level*2;
            this.level+=1;
        }
    }

    // Check collision for player and fish 
    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Eat");
    //     if (other.gameObject.tag == "Fish")
    //     {
    //         Debug.Log(other.GetComponent<Fish>());
    //     }
    // }
}
