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


    }
}
