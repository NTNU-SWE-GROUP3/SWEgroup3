using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    public GameObject ShowPanel;
    public Image ZoomImage;

    void Start()
    {
        ShowPanel.SetActive(false);
    }
    public void ClickImage()
    {
        ShowPanel.SetActive(true);
        ZoomImage = gameObject.GetComponent<Image>();
    }

    public  void ClickCloseButton()
    {
        ShowPanel.SetActive(false);
    }
}
