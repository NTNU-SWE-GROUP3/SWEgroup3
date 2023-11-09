using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private Image img;
    private int count = 1;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        img = GameObject.Find("MusicButton").GetComponent<Image>();
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
