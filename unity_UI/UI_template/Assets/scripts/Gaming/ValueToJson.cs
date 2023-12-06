using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class CardSelection: MonoBehaviour
{
    public int type;
    public int player;
    public int playerCardID;
    public int opponetCardID;
    public bool isRevolution;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class MsgBack
{
    public int winLoss;
    public bool trojanActivate;

    public static MsgBack CreateFromJSON(string jsonString)
    {
        
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("msgBack:" + jsonString);
            return JsonUtility.FromJson<MsgBack>(jsonString);
        }
        else
        {
            Debug.Log("msgBackErr:" + jsonString);
            return null;
        }
        
    }
}

public class SkillSelection: MonoBehaviour
{
    public int type;
    public int player;
    public int playerSkillID;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class SkillMsgBack
{
    public int OpponentSkillId;

    public static SkillMsgBack CreateFromJSON(string jsonString)
    {
        
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("SkillmsgBack:" + jsonString);
            return JsonUtility.FromJson<SkillMsgBack>(jsonString);
        }
        else
        {
            Debug.Log("SkillmsgBackErr:" + jsonString);
            return null;
        }
        
    }
}

public class GameStart: MonoBehaviour
{
    public int type;
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class RoomInfo
{
    public int roomId;
    public string playerCardSet;
    public string opponentCardSet;

    public static RoomInfo CreateFromJSON(string jsonString)
    {
        
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("msgBack:" + jsonString);
            return JsonUtility.FromJson<RoomInfo>(jsonString);
        }
        else
        {
            Debug.Log("msgBackErr:" + jsonString);
            return null;
        }
        
    }
}


