using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    public IEnumerator SendSkillCard(int skillId)
    {
        SkillSelection gs = gameObject.AddComponent<SkillSelection>();
        gs.gameType = 1;
        gs.roomId = 1;
        gs.playerToken = "XYZ";
        gs.playerSkillID = skillId;
        gs.cardId = -1;
        //Debug.Log("useSkill");
        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"useSkill"));
        yield return cd.coroutine;
        Debug.Log("return : " + cd.result);

        string retString = cd.result.ToString();
        SkillMsgBack ret = new SkillMsgBack();
        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
        {
            Debug.Log("SkipButton:" + retString);
            //here should back to login scene
        }
        else
        {
            ret = SkillMsgBack.CreateFromJSON(cd.result.ToString());
        }

        if(ret.OpponentSkillId == -1)
        {
            Debug.Log("SkipButton:" + ret.errMessage);
            //back to game lobby or main scene
        }
        else
        {
            Debug.Log("SkipButton:" + ret.errMessage);
        }
    }

    public void ClickSkip()
    {
        Debug.Log("ClickSkip:" + GameController.PlayerSkillId);
        if(GameController.PlayerSkillId == 2 || GameController.PlayerSkillId == 3 )
        {
            StartCoroutine(SendSkillCard(GameController.PlayerSkillId));
        }

        UseSkill.Clock = 0;
        gameObject.SetActive(false);
    }
}
