using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    public Material skybox1;
    public Material skybox2;
    public Material skybox3;
    private List<Material> skyboxes = new List<Material>();

    private int index = 0;

    void Start()
    {
        RenderSettings.skybox = skybox1;
        skyboxes.Add(skybox1);
        skyboxes.Add(skybox2);
        skyboxes.Add(skybox3);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeTheme()
    {
        index = (index + 1) % 3;
        RenderSettings.skybox = skyboxes[index];

    }
}
