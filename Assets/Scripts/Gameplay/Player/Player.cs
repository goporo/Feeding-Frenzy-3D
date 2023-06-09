using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private float speed = 7;
    [SerializeField] private float sprintSpeedMultiplier = 10f; // Additional speed when sprinting
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private EndGameCamera endGameCamera;
    [SerializeField] private Fish playerFish;



    // Start is called before the first frame update
    void Start()
    {
        this.playerFish.onLevelUp += GrowUp;
    }

    private void Awake()
    {
        playerFish.swimSpeed = speed;
    }
    // Update is called once per frame
    void Update()
    {
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
            inputVector *= playerFish.swimSpeed * sprintSpeedMultiplier;
        }
        else
        {
            inputVector *= playerFish.swimSpeed;
        }

        playerFish.SetIsSwimming(inputVector.magnitude > 0);

        // if (Input.GetKey(KeyCode.Z))
        // {
        //     this.playerFish.Eat(this.playerFish);
        // }
        if (!((transform.position.y - this.GetComponentInChildren<UnderWaterScript>().GetWaterHeight()) < 0))
        {
            transform.position -= new Vector3(0, 15 * Time.deltaTime, 0);
        }
        transform.position += inputVector * Time.deltaTime;
    }

    private void GrowUp(object sender, EventArgs e)
    {
        // Debug.Log("Level Up Player");
        // transform.localScale = new Vector3(playerFish.GetSize()*2,playerFish.GetSize()*2,playerFish.GetSize()*2);
        if (playerFish.GetLevel() > 5)
        {
            // playerFish.GetComponentInChildren<MeshFilter>().mesh = level2Mesh;
        }
        else if (playerFish.GetLevel() > 3)
        {
            // playerFish.GetComponentInChildren<MeshFilter>().mesh = level3Mesh;
        }
    }
    public float getExp()
    {
        return this.playerFish.GetExp();
    }
    public float getLevel()
    {
        return this.playerFish.GetLevel();
    }
    public float getMaxExp()
    {
        return this.playerFish.GetMaxExp();
    }
    public float GetScore()
    {
        return this.playerFish.GetScore();
    }
    public EndGameCamera GetEndGameCamera()
    {
        return this.endGameCamera;
    }
}
