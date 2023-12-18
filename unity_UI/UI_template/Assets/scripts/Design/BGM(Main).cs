using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private Slider MusicSlider;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3 = null;
    private GameObject panelHolder;
    private float time1;
    private float time2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        MusicSlider = GameObject.Find("Slider").GetComponent<Slider>();
        panelHolder = GameObject.Find("PanelHolder");
        time1 = 1;
        time2 = 0;
    }

    private void Update()
    {
        audioSource.volume = MusicSlider.value;
        float x = panelHolder.GetComponent<RectTransform>().offsetMax.x;
        Debug.Log(x);
        
        if (x <= -879 && x >= -1281)
        {
            if (audioSource.clip != clip2)
            {
                audioSource.clip = clip2;
                audioSource.time = time2;
                audioSource.Play();
            }
        }
        else if ((x > -1958 && x < -1281) || (x <= -200 && x > -879))
        {
            if (audioSource.clip == clip1)
            {
                time1 = audioSource.time;
                audioSource.clip = clip3;
                audioSource.Play();
            }
            else if(audioSource.clip == clip2)
            {
                time2 = audioSource.time;
                audioSource.clip = clip3;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != clip1)
            {
                audioSource.clip = clip1;
                audioSource.time = time1;
                audioSource.Play();
            }
        }
    }
}
