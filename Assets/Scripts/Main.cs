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
    public GameManager gameManager;
    public bool pause = false;
    void Start()
    {
        staticOverMat = overMat;
        staticOverMatCloseEnough = overMatCloseEnough;
        gameManager = new GameManager();
        if (genevieve == null)
            Debug.LogError("Genevieve not set in Main");
        else if (cameraController == null)
            Debug.LogError("Camera not set in Main");
        else
        {
            genevieve.Init(cameraController);
            genevieve.gameManager = gameManager;
            cameraController.Init(genevieve);
            cameraController.gameManager = gameManager;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            gameManager.running = !pause;
        }
        if (genevieve != null)
        {
            genevieve.UpdatePosition();
            cameraController.UpdateCamera();
            genevieve.UpdateAfterCamera();
            genevieve.UpdateAnims();
        }
    }
}
