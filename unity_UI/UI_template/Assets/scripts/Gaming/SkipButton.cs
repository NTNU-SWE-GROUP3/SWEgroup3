using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{

    private DontDestroy userdata;

    public IEnumerator SendSkillCard(int skillId)
    {
        userdata = FindObjectOfType<DontDestroy>();
        SkillSelection gs = gameObject.AddComponent<SkillSelection>();
        gs.gameType = userdata.gameType;
        gs.roomId = userdata.roomId;
        gs.playerToken = userdata.token;
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
            SceneManager.LoadScene(0);
        }
        else
        {
            ret = SkillMsgBack.CreateFromJSON(cd.result.ToString());
        }

        if(ret.OpponentSkillId == -1)
        {
            Debug.Log("SkipButton:" + ret.errMessage);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("SkipButton:" + ret.errMessage);
        }
    }
    
    public void ClickSkip()
    {
        UseSkill.Clock = 0;
        gameObject.SetActive(false);
    }
}
