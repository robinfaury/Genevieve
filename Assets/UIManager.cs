using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIManager : MonoBehaviour
{

    public Button playButton;
    public Button quitButton;
    public Color colorHover;
    public Color colorDisabled;
    public Color colorClicked;
    private UnscaledTimer timer;
    private bool waitPlay = false;
    private bool waitQuit = false;
    public bool playClicked = false;

    // Use this for initialization
    void Start()
    {
        timer = new UnscaledTimer();
        playButton.onClick.AddListener(() => OnClickPlayButton());
        quitButton.onClick.AddListener(() => OnClickQuitButton());

        SetEvent(playButton.gameObject, EventTriggerType.PointerEnter, (o) => OnEnterPlayButton());
        SetEvent(playButton.gameObject, EventTriggerType.PointerExit, (o) => OnExitPlayButton());
        SetEvent(quitButton.gameObject, EventTriggerType.PointerEnter, (o) => OnEnterQuitButton());
        SetEvent(quitButton.gameObject, EventTriggerType.PointerExit, (o) => OnExitQuitButton());
        SetEvent(playButton.gameObject, EventTriggerType.PointerUp, (o) => OnUpPlayButton());
        SetEvent(quitButton.gameObject, EventTriggerType.PointerUp, (o) => OnUpQuitButton());
    }

    // Update is called once per frame
    void Update()
    {
        if (waitPlay && timer.Get() >= 0.1f)
        {
            OnUpPlayButton();
            Hide();
            playClicked = true;
        }
        if (waitQuit && timer.Get() >= 0.1f)
        {
            OnUpQuitButton();
            Application.Quit();
            waitQuit = false;
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        waitPlay = false;
        waitQuit = false;
        playClicked = false;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        waitPlay = false;
        waitQuit = false;
        playClicked = false;
    }
    public void OnClickPlayButton()
    {
        playButton.GetComponentInChildren<Text>().color = colorClicked;
        playButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 60);

        timer.Reset();
        waitPlay = true;
    }

    private void OnClickQuitButton()
    {
        quitButton.GetComponentInChildren<Text>().color = colorClicked;
        quitButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -15);

        timer.Reset();
        waitQuit = true;
    }

    private void OnEnterPlayButton()
    {
        playButton.GetComponentInChildren<Text>().color = colorHover;
    }

    private void OnEnterQuitButton()
    {
        quitButton.GetComponentInChildren<Text>().color = colorHover;
    }

    private void OnExitPlayButton()
    {
        playButton.GetComponentInChildren<Text>().color = colorDisabled;
    }

    private void OnExitQuitButton()
    {
        quitButton.GetComponentInChildren<Text>().color = colorDisabled;
    }

    private void OnUpPlayButton()
    {
        playButton.GetComponentInChildren<Text>().color = colorHover;
        playButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 55);
    }

    private void OnUpQuitButton()
    {
        quitButton.GetComponentInChildren<Text>().color = colorHover;
        quitButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20);
    }

    private void SetEvent(GameObject gameObject, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> functionToCall)
    {
        if (gameObject.GetComponent<EventTrigger>() == null)
            gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback.AddListener(functionToCall);

        gameObject.GetComponent<EventTrigger>().triggers.Add(entry);
    }
}
