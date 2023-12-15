using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class CardSelection: MonoBehaviour
{
    public int gameType;
    public int roomId;
    public string playerToken;
    public int playerCardID;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class MsgBack
{
    public int OpponentCardId;
    public string errMessage;

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
    public int gameType;
    public int roomId;
    public string playerToken;
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
    public string errMessage;

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
    public int gameType;
    public int roomId;
    public string playerToken;
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


public class GameTurn: MonoBehaviour
{
    public int gameType;
    public int roomId;
    public string playerToken;
    public int playerEarn;
    public int opponentEarn;
    
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class TrunStat
{
    public int state;
    public string errMessage;

    public static TrunStat CreateFromJSON(string jsonString)
    {
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("TrunStatBack:" + jsonString);
            return JsonUtility.FromJson<TrunStat>(jsonString);
        }
        else
        {
            Debug.Log("TrunStatBackErr:" + jsonString);
            return null;
        }
    }
}


