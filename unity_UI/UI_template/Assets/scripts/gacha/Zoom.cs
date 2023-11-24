using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    public GameObject ShowPanel;
    public Image ZoomImage;
    public Text ZoomText;

    void Start()
    {
        ShowPanel.SetActive(false);
    }
    public void ClickImage()
    {
        ShowPanel.SetActive(true);
        ZoomImage.sprite = this.gameObject.GetComponent<Image>().sprite;
        ZoomText.text = this.gameObject.GetComponentInChildren<Text>().text;
        if(this.gameObject.GetComponentInChildren<Text>().text == "coin")
        {
            ZoomText.text = "不是欸...金幣有什麼好看的?";
            ZoomText.fontSize = 60;
        }
        else
            ZoomText.fontSize = 100;
    }

    public  void ClickCloseButton()
    {
        ShowPanel.SetActive(false);
    }
}
