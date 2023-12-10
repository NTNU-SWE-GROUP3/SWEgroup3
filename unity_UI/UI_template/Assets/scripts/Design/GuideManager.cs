using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GuideManager : MonoBehaviour
{

    [SerializeField] GameObject pagechange;

    [SerializeField] GameObject Mainsc;
    [SerializeField] GameObject Gamesc;

    [SerializeField] GameObject MaskPanel;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject continueIcon;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.1f;
    [SerializeField] private float waitingSpeed = 0.2f;
    [SerializeField] private float switchSpeed = 0.8f;


    [SerializeField] GameObject MaskPanel1;
    [SerializeField] GameObject MaskPanel2;
    [SerializeField] GameObject MaskPanel3;
    [SerializeField] GameObject MaskPanel4;
    [SerializeField] GameObject MaskPanel5;
    

    private Coroutine displayLineCoroutine;
    private int dialogueLenth;
    private int dialogueCurrentLenth;
    private string[] textPtr;
    private int currentstory = 0;
    private static float[] timeControl ={1f,0.5f,0.5f,0.5f,0.5f};
    
    private static string[] intro = {"在某個古老的中世紀酒吧裡\n木制長桌上灰塵飛揚\n沾滿酒漬的桌面反映出暗淡的燭光。", 
                                    "石牆上掛著斑駁的旗幟\n勇士和冒險家的壁畫在暗淡的燈光下閃爍著。", 
                                    "酒吧的角落有著一個區域\n桌子上放著古老的牌組\n周圍圍著一群群酒醉的顧客\n吆喝著、嬉笑打鬧著。",
                                    "人們聚集在一起\n不僅是為了享受美酒佳餚\n更是為了一場激烈的紙牌遊戲。", 
                                    "紙牌在手中翻飛\n與此同時\n挑釁和嘲笑聲也在空氣中交織。",
                                    "古老的故事在酒吧的角落裡重新活現\n每一次選擇都蘊含著機會和挑戰\n等待著那些勇於冒險的人來探索。", 
                                    "在這片滄桑的環境中\n勇者、魔法師和盜賊們相聚一堂\n他們各自擁有著獨特的技能和策略\n試圖在這場紙牌遊戲中佔據優勢。", 
                                    "酒吧的氛圍充滿了刺激和緊張感\n而每一局遊戲都是一次充滿挑戰與機遇的旅程。",
                                    "人們聚在一起\n為了贏得學分\n為了譜寫屬於自己的傳奇故事\n他們將卡牌的世界帶入現實\n與朋友和陌生人一同開展這場精彩絕倫的遊戲之旅。" };

            
    private static string[] start = {"歡迎來到xxx 請讓我為進行你導覽", 
                                    "首先,我們所在的位置是遊戲大廳,正上方顯示的是玩家資訊,由左到到右分別為\n玩家暱稱\n玩家頭像\n以及玩家經驗", 
                                    "現在請試著點擊玩家頭像\n" };
    private bool startc = false;

    private static string[] user = {"這裡是玩家設定,玩家可以自行調整遊戲相關設定", 
                                        "了解後請點擊離開按鈕\n" };

    private static string[] gatcha = {"這裡是抽卡區域,玩家可以消耗金幣或寶石抽取稀有造型以及強力技能", 
                                        "左右滑動卡池圖片可以更換卡池,嘗試後請點擊繼續按鈕" };

    private static string[] pg5 = {"這裡是其他對戰區域,玩家可以在這裡選擇\n自訂對戰\n牌位對戰\n電腦對戰\n等三種模式", 
    };
    
    private static string[] pg1 = {"這裡造型裝備區域,玩家可以在這裡選擇造型裝備",};

    
    void Start()
    {
        ClearMask();
        MainscGuide();
    }

    void MainscGuide(){
        Mainsc.SetActive(true);
        Gamesc.SetActive(false);
        StoryController();
    }

    void GamescGuide(){



        
    }

    void  ClearMask(){
        MaskPanel1.SetActive(false);
        MaskPanel2.SetActive(false);
        MaskPanel3.SetActive(false);
        MaskPanel4.SetActive(false);
        MaskPanel5.SetActive(false);
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
        
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        yield return new WaitForSeconds(timeControl[currentstory]);
        MaskPanel.SetActive(false);
        currentstory++;
        StoryController();

    }


    private IEnumerator DisplayLine(string line) 
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
             
                dialogueText.maxVisibleCharacters++;

                if((letter=='\n')){
                    yield return new WaitForSeconds(waitingSpeed);
                }
                yield return new WaitForSeconds(typingSpeed);

        }
        // actions to take after the entire line has finished displaying
        if(dialogueCurrentLenth==dialogueLenth-1){
            yield return new WaitForSeconds(waitingSpeed);
        }
        continueIcon.SetActive(true);

    }
    public void StoryController(){
        switch(currentstory){
        
            case 0://intro
            MaskPanel.SetActive(true);
            EnterDialogueMode(intro,1);//9
            break;

            case 1://start
            MaskPanel.SetActive(true);
            EnterDialogueMode(start,1);
            break;

            case 2://user
            if(startc==false){
                startc=true;
                MaskPanel1.SetActive(true);

            }
            else{
            startc=false;
            ClearMask();
            MaskPanel.SetActive(true);
            EnterDialogueMode(user,1);
            }
            break;

            case 3://gatcha
            if(startc==false){
                startc=true;
                
            }
            else{
            startc=false;
            MaskPanel.SetActive(true);
            EnterDialogueMode(gatcha,1);
            }
            break;

            case 4://friendly
            if(startc==false){
                startc=true;
                MaskPanel2.SetActive(true);
                MaskPanel3.SetActive(true);
            }
            else{
            startc=false;
            ClearMask();
            MaskPanel.SetActive(true);
            EnterDialogueMode(pg5,1);
            }
            break;

            case 5://skin
            if(startc==false){
                startc=true;
                MaskPanel4.SetActive(true);
            }
            else{
            startc=false;
            ClearMask();
            MaskPanel.SetActive(true);
            EnterDialogueMode(pg1,1);
            }
            break;

            case 6://skin relate
            if(startc==false){
                startc=true;
                MaskPanel5.SetActive(true);
            }
            else{
            startc=false;
            ClearMask();
            MaskPanel.SetActive(true);
            }
            break;






        }
    }
}
