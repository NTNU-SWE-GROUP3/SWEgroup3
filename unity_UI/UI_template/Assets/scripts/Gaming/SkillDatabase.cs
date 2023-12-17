using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
  
    public static List<PlayerSkill> SkillList = new List<PlayerSkill>();
    string SkillImagesPath = "images/MainSc/Skill/";
    void Awake()
    {
        SkillList.Add(new PlayerSkill(1,"時間限縮","此回合縮短對手選牌時間1秒",Resources.Load<Sprite>(SkillImagesPath+"SKillTime")));
        SkillList.Add(new PlayerSkill(2,"階級流動","一張平民變騎士",Resources.Load<Sprite>(SkillImagesPath+"SkillKnight")));
        SkillList.Add(new PlayerSkill(3,"暗影轉職","一張平民變殺手",Resources.Load<Sprite>(SkillImagesPath+"SkillAssassin")));
        SkillList.Add(new PlayerSkill(4,"技能封印","下回合禁止對方使用玩家技能",Resources.Load<Sprite>(SkillImagesPath+"SkillStopSkill")));
        SkillList.Add(new PlayerSkill(5,"力量剝奪","此回合對方平民卡技能無效",Resources.Load<Sprite>(SkillImagesPath+"SkillCivilian" )));
        SkillList.Add(new PlayerSkill(6,"黃金風暴","獲勝金幣總數*1.5",Resources.Load<Sprite>(SkillImagesPath + "SkillCoin")));
        SkillList.Add(new PlayerSkill(7,"知己知彼","查看對手剩餘手牌",Resources.Load<Sprite>(SkillImagesPath + "SkillShowOpponent")));
        SkillList.Add(new PlayerSkill(8,"抉擇束縛","限制對手只能從隨機的兩張牌中出一張",Resources.Load<Sprite>(SkillImagesPath+ "SkillCardChoose")));
        SkillList.Add(new PlayerSkill(9,"強制徵收","對手贏牌區張數-1",Resources.Load<Sprite>(SkillImagesPath + "SkillMin1")));
        SkillList.Add(new PlayerSkill(10,"勝者之堆","我方贏牌區張數+1",Resources.Load<Sprite>(SkillImagesPath+ "SkillEarn1")));//路徑可改
        
    
    }

}
