using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainAudioManager : MonoBehaviour
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
}
