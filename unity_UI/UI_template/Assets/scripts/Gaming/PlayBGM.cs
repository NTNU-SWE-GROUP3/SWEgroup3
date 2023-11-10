using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    GameController GC;
    private Image img;
    private int count = 1;
    public GameObject OpponentEarn;
    public GameObject PlayerEarn;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        img = GameObject.Find("MusicButton").GetComponent<Image>();
    }
    private void Update()
    {
        if(OpponentEarn.transform.childCount >= 10 || PlayerEarn.transform.childCount >= 10)
        {
            StartCoroutine(StopBGM());
        }
        //Debug.Log(OpponentEarn.transform.childCount);
    }
    public IEnumerator StopBGM()
    {
        
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.0006f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ToggleAudio()
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
    }

    public override int GetHashCode()
    {
        return img.GetHashCode();
    }
}
