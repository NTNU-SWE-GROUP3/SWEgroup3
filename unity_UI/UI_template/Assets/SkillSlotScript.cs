using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillSlotScript : MonoBehaviour
{
    //public GameObject SkillPopupPrefab;
    public Image skillImage;
    public string skillName;
    public string skillDes;
    public int skillStyleID;
    public static GameObject lastClickedSprite;

    // Function to set the skill style based on the skill ID or other parameters
    public void SetSkillStyle(int skillStyleID)
    {
        this.skillStyleID = skillStyleID;
        Sprite skillSprite = GetSkillSprite(skillStyleID);
        skillName = GetSkillName(skillStyleID);
        skillDes = GetSkillDesc(skillStyleID);

        if (skillSprite != null)
        {
            skillImage.sprite = skillSprite;
        }
        else
        {
            Debug.LogError("Skill sprite not found for ID: " + skillStyleID);
        }
    }

    private Sprite GetSkillSprite(int skillStyleID)
    {
        string SkillImagesPath = "images/MainSc/Skill/";
        if (skillStyleID == 1)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SKillTime");
        }
        else if (skillStyleID == 2)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillKnight");
        }
        else if (skillStyleID == 3)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillAssassin");
        }
        else if (skillStyleID == 4)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillStopSkill");
        }
         else if (skillStyleID == 5)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillCivilian" );
        }
         else if (skillStyleID == 6)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillCoin");
        }
         else if (skillStyleID == 7)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillShowOpponent");
        }
         else if (skillStyleID == 8)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillCardChoose");
        }
         else if (skillStyleID == 9)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillMin1");
        }
         else if (skillStyleID == 10)
        {
            return Resources.Load<Sprite>(SkillImagesPath+"SkillEarn1");
        }

        // Return null if the skill ID is out of bounds
        return null;
    }

    private string GetSkillName(int skillStyleID)
    {
        // Replace with your actual skill names array
        string[] skillNames = { "時間限縮", "階級流動", "暗影轉職", "技能封印", "力量剝奪", "黃金風暴", "知己知彼", "抉擇束縛", "強制徵收", "勝者之堆" };
        return skillNames[skillStyleID-1];
    }

    private string GetSkillDesc(int skillStyleID)
    {
        // Replace with your actual skill descriptions array
        string[] skillDescs = { "此回合縮短對手選牌時間1秒", "一張平民變騎士", "一張平民變殺手", "下回合禁止對方使用玩家技能", "此回合對方平民卡技能無效", "獲勝金幣總數*1.5", "查看對手剩餘手牌", "限制對手只能從隨機的兩張牌中出一張", "對手贏牌區張數-1", "我方贏牌區張數+1" };
        return skillDescs[skillStyleID-1];
    }

    private float GetSkillProbability(int skillStyleID)
    {
        // Replace with your actual skill probabilities array
        float[] skillProbs = { 0.05f, 0.15f, 0.1f, 0.08f, 0.12f, 0.07f, 0.1f, 0.09f, 0.1f, 0.14f };
        return skillProbs[skillStyleID];
    }
}
