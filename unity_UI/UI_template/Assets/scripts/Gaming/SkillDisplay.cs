using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillDisplay : MonoBehaviour
{
    public List<PlayerSkill> displaySkill = new List<PlayerSkill>();

    public int id;
    public string skillDescription;
    public Sprite skillSprite;
    public Text SkillDescriptionText;
    
    public Image skillImage;
    public void  Awake()
    {
        SkillDescriptionText = GameObject.Find("Canvas").GetComponentInChildren<Transform>().Find("MessagePanel/SkillImage/Image/SkillDescription").GetComponent<Text>();
    }
    void Start()
    {
        displaySkill[0] = SkillDatabase.SkillList[ShowSkill.PlayerSkillIdList[ShowSkill.skillIndex]];
        id = displaySkill[0].SkillId;
        skillDescription = displaySkill[0].SkillDescription;
        skillSprite = displaySkill[0].SkillSprite;

        skillImage.sprite = skillSprite;
        ShowSkill.skillIndex++;
    }

    void Update()
    {
        if(ClickDetector.skillId == this.id)
        {
            this.GetComponent<Image>().color = new Color32(0, 255, 0,100);
            SkillDescriptionText.text = skillDescription ;
        }
        else
        {
            this.GetComponent<Image>().color = new Color32(0,0,0,0);
        }
 
        
    }

    
}
