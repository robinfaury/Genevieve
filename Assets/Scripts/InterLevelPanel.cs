using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InterLevelPanel
{
    private Image interLevelImage;
    private Text text;
    private float fadeDuration = 0;
    private bool fadeIn = false;
    private bool fadeOut = false;

    public void Init(GameObject interLevelPanel)
    {
        interLevelImage = interLevelPanel.GetComponent<Image>();
        text = interLevelPanel.transform.GetChild(0).GetComponent<Text>();
    }
    public void Update()
    {
        if (fadeIn)
        {
            interLevelImage.color = new Color(interLevelImage.color.r, interLevelImage.color.g, interLevelImage.color.b, Mathf.Clamp01(interLevelImage.color.a + Time.deltaTime / fadeDuration));
        }
        else if (fadeOut)
        {
            interLevelImage.color = new Color(interLevelImage.color.r, interLevelImage.color.g, interLevelImage.color.b, Mathf.Clamp01(interLevelImage.color.a - Time.deltaTime / fadeDuration));
        }
        interLevelImage.gameObject.SetActive(interLevelImage.color.a > 0);
    }
    public void Set(string panelText, float alpha)
    {
        text.text = panelText;
        interLevelImage.color = new Color(interLevelImage.color.r, interLevelImage.color.g, interLevelImage.color.b, Mathf.Clamp01(alpha));
        fadeIn = false;
        fadeOut = false;
    }
    public void FadeIn(string panelText, float duration)
    {
        text.text = panelText;
        fadeDuration = duration;
        fadeIn = true;
        fadeOut = false;
    }
    public void FadeOut(string panelText, float duration)
    {
        text.text = panelText;
        fadeDuration = duration;
        fadeIn = false;
        fadeOut = true;
    }
}
