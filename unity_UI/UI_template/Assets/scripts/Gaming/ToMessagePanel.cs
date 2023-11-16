using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToMessagePanel : MonoBehaviour
{
    public Transform CardPanel;
    private GameObject zoomCard;
    private Transform border;
    private Transform cardName;
    private Transform iBorder;
    private Transform dBorder;

    private Text nameText;
    private Text skillText;
    private Text desText;
    private Image cardImage;
    public void  Awake()
    {
        CardPanel = GameObject.Find("Canvas").GetComponentInChildren<Transform>().Find("MessagePanel/CardPanel");
    }
    public void CardShowOnMessagePanel()
    {
            zoomCard = Instantiate(gameObject,CardPanel.position,Quaternion.identity);
            border = zoomCard.GetComponentInChildren<Transform>().Find("Border");
            cardName = zoomCard.GetComponentInChildren<Transform>().Find("Border/Name");
            nameText = zoomCard.GetComponentInChildren<Transform>().Find("Border/Name/Name Text").GetComponent<Text>();
            iBorder = zoomCard.GetComponentInChildren<Transform>().Find("Border/Image Border");
            dBorder = zoomCard.GetComponentInChildren<Transform>().Find("Border/Description Border");
            skillText = zoomCard.GetComponentInChildren<Transform>().Find("Border/Description Border/cardSkill Text").GetComponent<Text>();
            desText = zoomCard.GetComponentInChildren<Transform>().Find("Border/Description Border/Description Text").GetComponent<Text>();
            cardImage = zoomCard.GetComponentInChildren<Transform>().Find("Border/Image Border/Image").GetComponent<Image>();
            zoomCard.transform.SetParent(CardPanel,false);
            zoomCard.layer = LayerMask.NameToLayer("Choose");

            RectTransform rect;
            rect = zoomCard.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(300,450);

            rect = border.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(285,435);

            rect = cardName.GetComponent<RectTransform>();
            rect.position = new Vector2(rect.position.x,rect.position.y+70);
            rect.sizeDelta = new Vector2(270,40);

            rect = iBorder.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(270,170);
            rect.position = new Vector2(rect.position.x,rect.position.y+50);

            rect = dBorder.GetComponent<RectTransform>();
            rect.position = new Vector2(rect.position.x,rect.position.y-20);
            rect.sizeDelta = new Vector2(270,190);

            rect = nameText.GetComponent<RectTransform>();
            // rect.position = new Vector2(rect.position.x+5,rect.position.y-20);
            rect.sizeDelta = new Vector2(230,130);
            nameText.fontSize = 30;

            rect = skillText.GetComponent<RectTransform>();
            rect.position = new Vector2(rect.position.x+5,rect.position.y-20);
            rect.sizeDelta = new Vector2(230,130);
            skillText.fontSize = 32;

            rect = desText.GetComponent<RectTransform>();
            rect.position = new Vector2(rect.position.x,rect.position.y-15);
            rect.sizeDelta = new Vector2(265,130);
            desText.fontSize = 30;

            rect = cardImage.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(250,150);
            rect.position = new Vector2(rect.position.x,rect.position.y);
    }
}
