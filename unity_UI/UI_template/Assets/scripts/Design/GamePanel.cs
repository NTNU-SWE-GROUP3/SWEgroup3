using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField] GameObject Opponentarea;
    [SerializeField] GameObject Tiearea;
    [SerializeField] GameObject Playerarea;
    [SerializeField] GameObject End;
    [SerializeField] GameObject img;
    

    [SerializeField] GameObject Button;
    [SerializeField] GameObject Button2;
    [SerializeField] TMP_Text Icontext;

    private int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button2.SetActive(false);
        ToNextIcon();
    }

    // Update is called once per frame
    void ClearIcon()
    {
        Opponentarea.SetActive(false);
        Tiearea.SetActive(false);
        Playerarea.SetActive(false);
        End.SetActive(false);
    }

    public void ToNextIcon(){
        ClearIcon();
        switch(current){
            case 0:
            Playerarea.SetActive(true);
            Icontext.text = "畫面偏下為玩家出牌區域,將卡牌拖移至此即可完成出牌\n出牌區右邊為玩家贏牌區,玩家贏得的牌將會顯示於此";
            current++;
            break;

            case 1:
            Opponentarea.SetActive(true);
            Icontext.text = "畫面偏上為對手出牌區域,對手出的牌將會在回合結束後顯示";
            current++;
            break;

            case 2:
            Tiearea.SetActive(true);
            Icontext.text = "畫面中間為平手區域,當雙方平手時卡牌將會累計在平手區";
            current++;
            break;

            case 3:
            Icontext.text = "回合開始前可以選擇技能,當出具有功能的平民卡也會發動平民卡技能";
            current++;
            break;

            case 4:
            End.SetActive(true);
            Icontext.text = "當任意一方贏牌數達到10張便結束遊戲";
            current++;
            break;

            case 5:
            Icontext.text = "現在就退出新手教學試試吧";
            current++;
            Button.SetActive(false);
            Button2.SetActive(true);
            break;

        }
    }
}
