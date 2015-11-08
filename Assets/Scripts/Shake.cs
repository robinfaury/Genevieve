using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{
    public AudioSource audio;
    public float factor = 1.0f;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = audio.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
        if (spectrum[10] > 0.01)
        {
            if (audio.volume < 0.5f)
                this.transform.position = new Vector3(spectrum[10] * 5, spectrum[10] * 50, spectrum[10] * 5) * audio.volume / 2.0f;
            if (audio.volume > 0.5f)
                this.transform.position = new Vector3(spectrum[10] * 5, spectrum[10] * 50, spectrum[10] * 5) * audio.volume / 4.0f;
        }
        //Debug.Log(spectrum[100]);
    }
}
