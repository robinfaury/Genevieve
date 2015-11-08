using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Button playButton;
    public Button quitButton;
    public Color colorHover;
    public Color colorDisabled;
    public Color colorClicked;

	// Use this for initialization
	void Start () {
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
	void Update () {
	
	}

    public void OnClickPlayButton()
    {
        playButton.GetComponentInChildren<Text>().color = colorClicked;
        playButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 60);

        StartCoroutine(WaitForPlay());
        
        //
    }

    public IEnumerator WaitForPlay()
    {
        print(Time.time);
        yield return new WaitForSeconds(0.1f);
        OnUpPlayButton();
        print(Time.time);
        gameObject.SetActive(false);
    }

    public IEnumerator WaitForQuit()
    {
        print(Time.time);
        yield return new WaitForSeconds(0.1f);
        OnUpQuitButton();
        Application.Quit();
        print(Time.time);
    }

    private void OnClickQuitButton()
    {
        quitButton.GetComponentInChildren<Text>().color = colorClicked;
        quitButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -15);

        StartCoroutine(WaitForQuit());
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
