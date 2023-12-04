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


