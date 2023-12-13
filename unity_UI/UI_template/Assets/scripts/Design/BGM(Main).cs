using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private Slider MusicSlider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        MusicSlider = GameObject.Find("Slider").GetComponent<Slider>();;
    }
    private void Update()
    {
        audioSource.volume = MusicSlider.value;
    }
}
