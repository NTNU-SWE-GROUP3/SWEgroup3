using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainAudioManager : MonoBehaviour
{
    private Image img;
    private int count = 1;

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
    
    public void ToggleAudio()
    {
        Next();
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
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

    public void Next()
    {
        count++;
        if (count > 2)
            count = 1;

        img.sprite = Resources.Load<Sprite>("images/Music" + count.ToString());
    }

    public override global::System.Boolean Equals(global::System.Object obj)
    {
        return obj is MainAudioManager change &&
               EqualityComparer<Image>.Default.Equals(img, change.img);
    }

    public override int GetHashCode()
    {
        return img.GetHashCode();
    }
}
