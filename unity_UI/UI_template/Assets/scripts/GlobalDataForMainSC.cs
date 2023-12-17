using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataForMainSC : MonoBehaviour 
{
    private DontDestroy userdata; // 參考 DontDestroy 類

    void Start()
    {
        // 獲取 DontDestroy 類的實例
        userdata = FindObjectOfType<DontDestroy>();
        if (userdata != null)
        {

            string player_token = userdata.token;
            Debug.Log("Token value: " + player_token);

            StartCoroutine(logdata(player_token)); //把拿變數的動作包裝起來

        }
        else
        {
            Debug.LogError("DontDestroy script not found!");
        }

        IEnumerator logdata(string player_token)
        {
            yield return StartCoroutine(userdata.Init_Card_Skill_Account_Data(player_token)); //等更新完在拿資料，以免得到更新前資料

            string cardDescription = userdata.characterDataList[2].CardDescription;
            Debug.Log("character 3 Description: " + cardDescription);

            int player_coin = userdata.coin;
            Debug.Log("player's coin: " + player_coin);

        }


    }

}
