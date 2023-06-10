using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private float speed = 7;
    [SerializeField] private float sprintSpeedMultiplier = 10f; // Additional speed when sprinting
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private EndGameCamera endGameCamera;
    private Fish currentFish;
    [SerializeField] private GameObject Fish1;
    [SerializeField] private GameObject Fish2;
    [SerializeField] private GameObject Fish3;




    // Start is called before the first frame update
    void Start()
    {
        Fish1.SetActive(true);
        currentFish = Fish1.GetComponent<Fish>();
        this.currentFish.onLevelUp += GrowUp;
    }


    // Update is called once per frame
    void Update()
    {
        // test leveling up by click
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Fish1.SetActive(false);
        //     Fish2.SetActive(true);
        //     currentFish = Fish2.GetComponent<Fish>();
        // }

        Vector3 inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector += playerCamera.cameraDirection;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputVector -= playerCamera.cameraDirection;
        }


        inputVector = inputVector.normalized;

        // Check if the Shift key is held down to sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputVector *= speed * sprintSpeedMultiplier;
        }
        else
        {
            inputVector *= speed;
        }

        currentFish.SetIsSwimming(inputVector.magnitude > 0);

        // if (Input.GetKey(KeyCode.Z))
        // {
        //     this.currentFish.Eat(this.currentFish);
        // }
        if (!((transform.position.y - this.GetComponentInChildren<UnderWaterScript>().GetWaterHeight()) < 0))
        {
            transform.position -= new Vector3(0, 15 * Time.deltaTime, 0);
        }
        transform.position += inputVector * Time.deltaTime;


    }

    private void GrowUp(object sender, EventArgs e)
    {
        // suspected of causing bug
        if (currentFish.GetLevel() > 5)
        {
            Fish3.SetActive(true);
            currentFish = Fish3.GetComponent<Fish>();
            Fish2.SetActive(false);
        }
        else if (currentFish.GetLevel() > 3)
        {
            Fish2.SetActive(true);
            currentFish = Fish2.GetComponent<Fish>();
            Fish1.SetActive(false);
        }
    }
    public float getExp()
    {
        return this.currentFish.GetExp();
    }
    public float getLevel()
    {
        return this.currentFish.GetLevel();
    }
    public float getMaxExp()
    {
        return this.currentFish.GetMaxExp();
    }
    public float GetScore()
    {
        return this.currentFish.GetScore();
    }
    public EndGameCamera GetEndGameCamera()
    {
        return this.endGameCamera;
    }
}
