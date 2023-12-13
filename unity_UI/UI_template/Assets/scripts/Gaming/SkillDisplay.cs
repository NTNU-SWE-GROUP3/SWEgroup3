using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillDisplay : MonoBehaviour
{
    public List<PlayerSkill> displaySkill = new List<PlayerSkill>();

    public GameObject UsedPanel;
    
    public GameObject ForbiddenPanel;

    public int id;
    public string skillDescription;
    public string skillName;
    public Sprite skillSprite;
    public Text SkillNameText;
    public Text SkillDescriptionText;
    
    public Image skillImage;
    public void  Awake()
    {
        SkillDescriptionText = GameObject.Find("Canvas").GetComponentInChildren<Transform>().Find("MessagePanel/SkillImage/Image/SkillDescription").GetComponent<Text>();
        SkillNameText = GameObject.Find("Canvas").GetComponentInChildren<Transform>().Find("MessagePanel/SkillImage/SkillName").GetComponent<Text>();
    }
    void Start()
    {
        UsedPanel.SetActive(false);
        ForbiddenPanel.SetActive(false);
        displaySkill[0] = SkillDatabase.SkillList[ShowSkill.PlayerSkillIdList[ShowSkill.skillIndex]-1];
        id = displaySkill[0].SkillId;
        skillName = displaySkill[0].SkillName;
        skillDescription = displaySkill[0].SkillDescription;
        skillSprite = displaySkill[0].SkillSprite;

        skillImage.sprite = skillSprite;
        ShowSkill.skillIndex++;
    }

    void Update()
    {
        if(this.gameObject.layer == 13)
        {
            ForbiddenPanel.SetActive(false);
            if(ClickDetector.skillId == this.id)
            {
                if(this.id == 8)
                {
                    SkillDescriptionText.fontSize = 60;
                }
                else
                {
                    SkillDescriptionText.fontSize = 70;
                }
                this.GetComponent<Image>().color = new Color32(0, 255, 0,100);
                SkillDescriptionText.text = skillDescription ;
                SkillNameText.text = skillName;
            }
            else
            {
                this.GetComponent<Image>().color = new Color32(0,0,0,0);
            }
        }
        else if(this.gameObject.layer == 14)
        {
            this.GetComponent<Image>().color = new Color32(0,0,0,0);
            UsedPanel.SetActive(true);
        }
        else if(this.gameObject.layer == 15)
        {
            this.GetComponent<Image>().color = new Color32(0,0,0,0);
            ForbiddenPanel.SetActive(true);
        }

 
        
    }

    
}
