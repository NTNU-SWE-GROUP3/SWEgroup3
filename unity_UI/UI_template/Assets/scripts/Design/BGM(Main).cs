using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainAudioManager : MonoBehaviour
{
    public Image img;
    //private int count = 1;

    private AudioSource audioSource;
    public Slider slider;
    public AudioClip clip1;
    public AudioClip clip2;
    private AudioClip clip3 = null;
    private GameObject panelHolder;
    private float time1;
    private float time2;

    private bool stop = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        panelHolder = GameObject.Find("PanelHolder");
        time1 = 1;
        time2 = 0;
    }
    void Update()
    {
        if(stop == false)
        {
            audioSource.volume = slider.value;
        }

        float x = panelHolder.GetComponent<RectTransform>().offsetMax.x;

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
            else if (audioSource.clip == clip2)
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
