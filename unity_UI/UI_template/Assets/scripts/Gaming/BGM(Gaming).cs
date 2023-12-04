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
    private Slider _slider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //img = GameObject.Find("MusicButton").GetComponent<Image>();
        img = GameObject.Find("Handle").GetComponent<Image>();
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
    }
    void Update()
    {
        audioSource.volume = _slider.value;
    }

    public void StopBGM()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.005f;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public override int GetHashCode()
    {
        return img.GetHashCode();
    }

    /*public void ToggleAudio()
    {
        Next();
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
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
        return obj is AudioManager change &&
               EqualityComparer<Image>.Default.Equals(img, change.img);
    }*/
}
