using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkindesPanel : MonoBehaviour
{

    [SerializeField] GameObject KingIcon;
    [SerializeField] GameObject QueenIcon;
    [SerializeField] GameObject PrinceIcon;
    [SerializeField] GameObject KnightIcon;
    [SerializeField] GameObject CivilianIcon;
    [SerializeField] GameObject AssassinIcon;

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
        KingIcon.SetActive(false);
        QueenIcon.SetActive(false);
        PrinceIcon.SetActive(false);
        KnightIcon.SetActive(false);
        CivilianIcon.SetActive(false);
        AssassinIcon.SetActive(false);
    }

    public void ToNextIcon(){
        ClearIcon();
        switch(current){
            case 0:
            KingIcon.SetActive(true);
            Icontext.text = "左上角為國王圖示";
            current++;
            break;

            case 1:
            QueenIcon.SetActive(true);
            Icontext.text = "上排中間為皇后圖示";
            current++;
            break;

            case 2:
            PrinceIcon.SetActive(true);
            Icontext.text = "右上角為王子圖示";
            current++;
            break;

            case 3:
            KnightIcon.SetActive(true);
            Icontext.text = "左下角為騎士圖示";
            current++;
            break;

            case 4:
            CivilianIcon.SetActive(true);
            Icontext.text = "下排中間為平民圖示";
            current++;
            break;

            case 5:
            AssassinIcon.SetActive(true);
            Icontext.text = "右下角為殺手圖示";
            current++;
            Button.SetActive(false);
            Button2.SetActive(true);
            break;

        }
    }
}
