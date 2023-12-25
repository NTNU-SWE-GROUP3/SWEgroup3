using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class CardSelection: MonoBehaviour
{
    public int gameType;
    public string roomId;
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
    public string roomId;
    public string playerToken;
    public int playerSkillID;
    public int cardId;

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

public class SkillCheck: MonoBehaviour
{
    public int gameType;
    public string roomId;
    public string playerToken;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class SkillCheckBack
{
    public int cardId;
    public string errMessage;

    public static SkillCheckBack CreateFromJSON(string jsonString)
    {
        
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("SkillmsgBack:" + jsonString);
            return JsonUtility.FromJson<SkillCheckBack>(jsonString);
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
    public string roomId;
    public string playerToken;
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class RoomInfo
{
    public string roomId;
    public string playerCardSet;
    public string opponentCardSet;

    public static RoomInfo CreateFromJSON(string jsonString)
    {
        
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("RoomInfoBack:" + jsonString);
            return JsonUtility.FromJson<RoomInfo>(jsonString);
        }
        else
        {
            Debug.Log("RoomInfoBackErr:" + jsonString);
            return null;
        }
        
    }
}


public class GameTurn: MonoBehaviour
{
    public int gameType;
    public string roomId;
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
            Debug.Log("TurnStatBack:" + jsonString);
            return JsonUtility.FromJson<TrunStat>(jsonString);
        }
        else
        {
            Debug.Log("TurnStatBackErr:" + jsonString);
            return null;
        }
    }
}


public class dilemmaUse: MonoBehaviour
{
    public int gameType;
    public string roomId;
    public int cardId1;
    public int cardId2;
    public string playerToken;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class dilemmaUseBack
{
    public int state;
    public string errMessage;

    public static dilemmaUseBack CreateFromJSON(string jsonString)
    {
        
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("deckRecoBack:" + jsonString);
            return JsonUtility.FromJson<dilemmaUseBack>(jsonString);
        }
        else
        {
            Debug.Log("deckRecoBackErr:" + jsonString);
            return null;
        }
        
    }
}

public class dilemmaCheck: MonoBehaviour
{
    public int gameType;
    public string roomId;
    public string playerToken;
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}

[Serializable]
public class dilemmaCheckBack
{
    public int cardId1;
    public int cardId2;
    public string errMessage;

    public static dilemmaCheckBack CreateFromJSON(string jsonString)
    {
        
        if (jsonString != "ConnectionError" && jsonString != "ProtocolError" && jsonString != "InProgress" && jsonString != "DataProcessingError")
        {
            Debug.Log("deckRecoBack:" + jsonString);
            return JsonUtility.FromJson<dilemmaCheckBack>(jsonString);
        }
        else
        {
            Debug.Log("deckRecoBackErr:" + jsonString);
            return null;
        }
        
    }
}





