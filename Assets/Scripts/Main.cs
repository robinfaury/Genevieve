using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public Material overMat;
    public static Material staticOverMat;
    public Material overMatCloseEnough;
    public static Material staticOverMatCloseEnough;
    public Genevieve genevieve;
    public CameraController cameraController;
    void Start()
    {
        staticOverMat = overMat;
        staticOverMatCloseEnough = overMatCloseEnough;
        if (genevieve == null)
            Debug.LogError("Genevieve not set in Main");
        else if (cameraController == null)
            Debug.LogError("Camera not set in Main");
        else
        {
            genevieve.Init(cameraController);
            cameraController.Init(genevieve);
        }
    }

    void Update()
    {
        if (genevieve != null)
        {
            genevieve.UpdatePosition();
            cameraController.UpdateCamera();
            genevieve.UpdateAfterCamera();
            genevieve.UpdateAnims();
        }
    }
}
