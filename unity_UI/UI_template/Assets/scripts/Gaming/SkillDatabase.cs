using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
  
    public static List<PlayerSkill> SkillList = new List<PlayerSkill>();
    void Awake()
    {
        SkillList.Add(new PlayerSkill(1,"此回合縮短對手選牌時間5秒",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(2,"一張平民變騎士",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(3,"一張平民變殺手",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(4,"下回合禁止對方使用玩家技能",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(5,"此回合對方平民卡技能無效",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(6,"獲勝金幣總數*1.5",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(7,"查看對手剩餘手牌",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(8,"限制對手只能從隨機的兩張牌中出一張",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(9,"對手贏牌區張數-1",Resources.Load<Sprite>("images/GameSc/king")));
        SkillList.Add(new PlayerSkill(10,"我方贏牌區張數+1",Resources.Load<Sprite>("images/GameSc/king")));//路徑可改
        
    
    }

}
