using UnityEngine;
using System.Collections;

public class UnderWaterScript : MonoBehaviour
{
    [SerializeField] private GameObject waterPlane;
    private float waterHeight;

    private bool isUnderwater;
    private Color normalColor = new Color(0.5f, 0.5f, 0.5f, 0f);
    private Color underwaterColor = new Color(0.12f, 0.3f, 0.45f);

    private float fogStrength = 0.007f;
    private void Awake()
    {
        waterHeight = waterPlane.transform.position.y;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y < waterHeight) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterHeight;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }


    }

    void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0f;

    }

    void SetUnderwater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = fogStrength;
    }
    public bool GetUnderwater()
    {
        return isUnderwater;
    }
    public float GetWaterHeight()
    {
        return waterHeight;
    }
}