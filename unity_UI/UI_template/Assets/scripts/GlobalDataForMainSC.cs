using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataForMainSC : MonoBehaviour
{
    private DontDestroy userdata; // 參考 DontDestroy 類
    //Variables
    private string player_token;
    public int player_coins;
    public int player_level;
    public int player_totalwin;
    public int player_totalmatch;
    public float player_winrate;
    public string player_ranking;
    public string player_nickname;
    public string player_email;

    void Start()
    {
        // 獲取 DontDestroy 類的實例

        // I'm So SORRY! 這裡不需要這個！ 但可以用來測試一下
       
        userdata = FindObjectOfType<DontDestroy>();
        if (userdata != null)
        {
            player_token = userdata.token;
            Debug.Log("Token value: " + player_token);

            string cardDescription = userdata.characterDataList[2].CardDescription;
            Debug.Log("character 3 Description: " + cardDescription);

        }
        else
        {
            Debug.LogError("DontDestroy script not found!");
        }


    }

}
