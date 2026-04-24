using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    private Material skyboxMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {


        float rotation = skyboxMaterial.GetFloat("_Rotation");
        float exposure = skyboxMaterial.GetFloat("_Exposure");
        rotation += Time.deltaTime * 10f;
        exposure -= Time.deltaTime * 0.1f;
        skyboxMaterial.SetFloat("_Rotation", rotation);
        skyboxMaterial.SetFloat("_Exposure", exposure);
    }
}