using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GuideManager : MonoBehaviour
{
    [SerializeField] GameObject Mainsc;
    [SerializeField] GameObject Gamesc;

    [SerializeField] GameObject MaskPanel;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject continueIcon;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.1f;
    [SerializeField] private float waitingSpeed = 0.2f;
    private bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;
    private int dialogueLenth;
    private int dialogueCurrentLenth;
    private string[] textPtr;
    
    
    
    
    private static string[] intro = {"在某個古老的中世紀酒吧裡\n木制長桌上灰塵飛揚\n沾滿酒漬的桌面反映出暗淡的燭光。", 
                                    "石牆上掛著斑駁的旗幟\n勇士和冒險家的壁畫在暗淡的燈光下閃爍著。", 
                                    "酒吧的角落有著一個小小的區域\n桌子上放著古老的牌組\n周圍圍著一群群酒醉的顧客\n或是吆喝著、或是嬉笑打鬧著。",
                                    "這是一個人們聚集在一起\n不僅是為了享受美酒佳餚\n更是為了一場場激烈的紙牌遊戲。", 
                                    "紙牌在手中翻飛\n與此同時\n挑釁和嘲笑聲也在空氣中交織。",
                                    "這場酒吧中的遊戲不僅僅是一場勝負的較量\n更是展現技巧和智慧的機會。",
                                    "古老的故事在酒吧的角落裡重新活現\n每一張牌都蘊含著機會和挑戰\n等待著那些勇於冒險的人來探索。", 
                                    "在這片滄桑的環境中\n勇者、魔法師和盜賊們相聚一堂\n他們各自擁有著獨特的技能和策略\n試圖在這場紙牌遊戲中佔據優勢。", 
                                    "酒吧的氛圍充滿了刺激和緊張感\n而每一局遊戲都是一次充滿挑戰與機遇的旅程。",
                                    "在這個中世紀酒吧中\n人們聚在一起\n為了贏得榮耀\n為了譜寫屬於自己的傳奇故事\n他們將卡牌的世界帶入現實\n與朋友和陌生人一同開展這場精彩絕倫的遊戲之旅。" };


    
    void Start()
    {
        MainscGuide();
    }

    
    void Update()
    {
        
    }

    void MainscGuide(){
        Mainsc.SetActive(true);
        Gamesc.SetActive(false);
        M_Stage1();
    }

    void GamescGuide(){



        
    }

    void M_Stage1()
    {//start demo
        MaskPanel.SetActive(true);
        
        EnterDialogueMode(intro,10);
    }
    public void EnterDialogueMode(string[] targetText,int lenth) 
    {
        dialogueLenth = lenth;
        dialogueCurrentLenth = 0;
        dialoguePanel.SetActive(true);
        textPtr = targetText;
        ContinueStory();
    }

    public void ContinueStory() 
    {
        if (dialogueCurrentLenth<dialogueLenth) //can continue
        {
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }

            string nextLine = textPtr[dialogueCurrentLenth++];
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator ExitDialogueMode() 
    {
        yield return new WaitForSeconds(0.2f);
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }


    private IEnumerator DisplayLine(string line) 
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);

        canContinueToNextLine = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
             
                dialogueText.maxVisibleCharacters++;

                if((letter=='\n')){
                    yield return new WaitForSeconds(waitingSpeed);
                }
                yield return new WaitForSeconds(typingSpeed);
            //}
        }
        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);

    }

}
