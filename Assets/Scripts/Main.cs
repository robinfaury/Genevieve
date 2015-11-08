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
    public UIManager uiManager;
    public HeartRateMonitor heartRateMonitor;
    [HideInInspector]
    public InterLevelPanel interLevelPanel;
    [HideInInspector]
    public Genevieve genevieve = null;
    [HideInInspector]
    public AudioSource salle = null;
    [HideInInspector]
    public AudioSource mamySource = null;
    private Rigidbody porteLargeRigidBody = null;
    private Dog dog = null;
    public AudioClip[] audioClips;
    public CameraController cameraController;
    [HideInInspector]
    public GameManager gameManager;
    private bool pauseMenu = true;
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
        {
            pauseMenu = !pauseMenu;
            if (pauseMenu)
                uiManager.Show();
            else
                uiManager.Hide();
        }
        if (uiManager.playClicked)
            pauseMenu = false;
        uiManager.playClicked = false;
        if (pauseMenu)
        {
            salle.Pause();
            mamySource.Pause();
            Time.timeScale = 0;
        }
        else
        {
            salle.UnPause();
            mamySource.UnPause();
            Time.timeScale = 1;
        }
        gameManager.Update(this);

        //if (Input.GetKeyDown(KeyCode.G))
            //gameManager.level = 5;

        if (gameManager.level != 4)
            porteLargeRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        else
            porteLargeRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (gameManager.levelState >= 2)
            dog.Rotate();

        gameManager.running = !gameManager.pauseGame && !pauseMenu;
        cursorGameObject.SetActive(gameManager.running);
        if (genevieve != null)
        {
            genevieve.UpdatePosition();
            cameraController.UpdateCamera();
            genevieve.UpdateAfterCamera();
            genevieve.UpdateAnims();
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
        genevieve = map.transform.Find("Genevieve").GetComponent<Genevieve>();
        salle = map.transform.Find("salle").GetComponent<AudioSource>();
        mamySource = map.transform.Find("mamySource").GetComponent<AudioSource>();
        porteLargeRigidBody = map.transform.Find("porte_large").GetComponent<Rigidbody>();
        dog = map.transform.Find("dog").GetComponent<Dog>();
        genevieve.Init(cameraController);
        genevieve.gameManager = gameManager;
        cameraController.Init(genevieve);
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
public class UnscaledTimer
{
    private float t = Time.unscaledTime;
    public UnscaledTimer()
    {
        t = Time.unscaledTime;
    }
    public void Reset()
    {
        t = Time.unscaledTime;
    }
    public float Get()
    {
        return Time.unscaledTime - t;
    }
    public void Set(float newT)
    {
        t = Time.unscaledTime - newT;
    }
    public void Substract(float sec)
    {
        t += sec;
    }
}