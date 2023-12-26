using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private Image img;
    //private int count = 1;

    private AudioSource audioSource;
    private Slider slider;
    private bool stop = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        img = GameObject.Find("Handle").GetComponent<Image>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
    }
    void Update()
    {
        if(stop == false)
        {
            audioSource.volume = slider.value;
        }
    }

    public void StopBGM()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        stop = true;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.007f;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public override int GetHashCode()
    {
        return img.GetHashCode();
    }
}
