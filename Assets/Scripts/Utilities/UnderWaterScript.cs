using UnityEngine;

public class UnderWaterScript : MonoBehaviour
{
    [SerializeField] private GameObject waterPlane;
    private float waterHeight;
    private float waterInnerRadius;

    private bool isUnderwater;
    private Color normalColor = new Color(0.5f, 0.5f, 0.5f, 0f);
    private Color underwaterColor = new Color(0.12f, 0.3f, 0.45f);

    private float fogStrength = 0.007f;

    private void Awake()
    {
        waterHeight = waterPlane.transform.position.y;
        CalculateWaterInnerRadius();
    }

    private void CalculateWaterInnerRadius()
    {
        MeshFilter meshFilter = waterPlane.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Vector3[] vertices = meshFilter.sharedMesh.vertices;
            Vector3 center = waterPlane.transform.position;

            float maxDistance = 0f;
            foreach (Vector3 vertex in vertices)
            {
                float distance = Vector3.Distance(center, waterPlane.transform.TransformPoint(vertex));
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            waterInnerRadius = maxDistance;
        }
        else
        {
            Collider collider = waterPlane.GetComponent<Collider>();
            if (collider != null)
            {
                waterInnerRadius = collider.bounds.extents.magnitude;
            }
            else
            {
                Debug.LogWarning("No MeshFilter or Collider found on the water plane.");
                waterInnerRadius = 0f;
            }
        }
    }

    private void Update()
    {
        if ((transform.position.y < waterHeight) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterHeight;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }
    }

    private void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0f;
    }

    private void SetUnderwater()
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

    public float GetWaterInnerRadius()
    {
        return waterInnerRadius;
    }
    public Vector3 GetWaterCenterPoint()
    {
        return waterPlane.transform.position;
    }
}
