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
    private bool stop = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(stop == false)
        {
            audioSource.volume = slider.value;
        }
    }
}
