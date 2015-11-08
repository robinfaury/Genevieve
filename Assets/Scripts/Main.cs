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
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public bool pauseMenu = false;
    void Start()
    {
        staticOverMat = overMat;
        staticOverMatCloseEnough = overMatCloseEnough;
        gameManager = new GameManager();
        gameManager.Init();
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
        gameManager.Update(this);
        if(Input.GetKeyDown(KeyCode.Escape))
            pauseMenu = !pauseMenu;
        if (pauseMenu)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        gameManager.running = !gameManager.pauseGame && !pauseMenu;
        if (genevieve != null)
        {
            genevieve.UpdatePosition();
            cameraController.UpdateCamera();
            genevieve.UpdateAfterCamera();
            genevieve.UpdateAnims();
        }
    }
}



public class Timer
{
    private float t = Time.time;
    public Timer()
    {
        t = Time.time;
    }
    public void Reset()
    {
        t = Time.time;
    }
    public float Get()
    {
        return Time.time - t;
    }
    public void Set(float newT)
    {
        t = Time.time - newT;
    }
    public void Substract(float sec)
    {
        t += sec;
    }
}