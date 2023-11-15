using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public void StopBGM()
    {
        //audioSource.Stop();
        //慢慢的把音樂切掉才比較好聽 所以我一開始就使用的方式
        StartCoroutine(FadeOut());
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
}
