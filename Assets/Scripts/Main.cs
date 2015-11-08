using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public Material overMat;
    public static Material staticOverMat;
    public Material overMatCloseEnough;
    public static Material staticOverMatCloseEnough;
    public GameObject interLevelPanelGameObject;
    public GameObject cursorGameObject;
    public GameObject map;
    public GameObject mapPrefab;
    [HideInInspector]
    public InterLevelPanel interLevelPanel;
    private Genevieve genevieve2 = null;
    public CameraController cameraController;
    [HideInInspector]
    public GameManager gameManager;
    private bool pauseMenu = false;
    void Start()
    {
        staticOverMat = overMat;
        staticOverMatCloseEnough = overMatCloseEnough;
        gameManager = new GameManager();
        gameManager.Init();
        interLevelPanel = new InterLevelPanel();
        interLevelPanel.Init(interLevelPanelGameObject);

        ResetMap();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            pauseMenu = !pauseMenu;
        if (pauseMenu)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        gameManager.Update(this);
        gameManager.running = !gameManager.pauseGame && !pauseMenu;
        cursorGameObject.SetActive(gameManager.running);
        if (genevieve2 != null)
        {
            genevieve2.UpdatePosition();
            cameraController.UpdateCamera();
            genevieve2.UpdateAfterCamera();
            genevieve2.UpdateAnims();
        }
        interLevelPanel.Update();
    }

    public void ResetMap()
    {
        if (map != null)
        {
            Vector3 mapPos = map.transform.position;
            Quaternion mapRot = map.transform.rotation;
            GameObject.Destroy(map);
            map = GameObject.Instantiate(mapPrefab);
            map.transform.position = mapPos;
            map.transform.rotation = mapRot;
        }
        else
            map = GameObject.Instantiate(mapPrefab);
        genevieve2 = map.transform.Find("Genevieve").GetComponent<Genevieve>();
        genevieve2.Init(cameraController);
        genevieve2.gameManager = gameManager;
        cameraController.Init(genevieve2);
        cameraController.gameManager = gameManager;
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