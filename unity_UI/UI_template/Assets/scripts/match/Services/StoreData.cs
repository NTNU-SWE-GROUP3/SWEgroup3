using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreData : MonoBehaviour
{
    private static DontDestroy userdata = FindObjectOfType<DontDestroy>();
    public static void store(int gameType,string roomId)
    {
        userdata.gameType = 0;
        userdata.roomId = roomId;
    }
}
