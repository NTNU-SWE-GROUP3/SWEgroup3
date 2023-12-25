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
    public AudioClip gacha1;
    public AudioClip gacha2;
    private GameObject panelHolder;
    private float time1;
    private float time2;
    private float vol2;

    private bool stop = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        panelHolder = GameObject.Find("PanelHolder");
        time1 = 1;
        time2 = 0;
    }

    public void Page5jump()
    {
        stop = false;
        StartCoroutine(wait());
    }

    public void GachaSE1()
    {
        audioSource.PlayOneShot(gacha1);
    }

     public void GachaSE10()
    {
        audioSource.PlayOneShot(gacha2);
    }

    public void Page4jump()
    {
        if(stop == false){
            stop = true;
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.45f);
        stop = true;
    }

    void Update()
    {
        audioSource.volume = slider.value;

        float x = panelHolder.GetComponent<RectTransform>().offsetMax.x;

        if (stop == false || (x > -200 || x <= -1968))
        {
            if (audioSource.clip != clip1)
            {
                audioSource.clip = clip1;
                audioSource.time = time1;
                audioSource.Play();
            }
        }
        else if(stop == true)
        {
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
        }
    }
}
