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
    [SerializeField] private float startSpeed = 0.5f;

    [SerializeField] GameObject skindesPanel;
    [SerializeField] GameObject skindesPanel2;
    [SerializeField] GameObject setPanel;
    [SerializeField] GameObject civPanel1;
    [SerializeField] GameObject civPanel2;
    [SerializeField] GameObject civPanel3;
    [SerializeField] GameObject skillPanel;
    [SerializeField] GameObject skinPanel;
    [SerializeField] GameObject MaskPanel1;
    [SerializeField] GameObject MaskPanel2;
    [SerializeField] GameObject MaskPanel3;
    [SerializeField] GameObject MaskPanel4;
    [SerializeField] GameObject MaskPanel5;
    [SerializeField] GameObject MaskPanel6;
    [SerializeField] GameObject MaskPanel7;
    [SerializeField] GameObject MaskPanel8;
    [SerializeField] GameObject GPanel;
    [SerializeField] GameObject button;
    
    public Animator Transition; 
    public float transitionTime = 1f;

    private Coroutine displayLineCoroutine;
    private int dialogueLenth;
    private int dialogueCurrentLenth;
    private string[] textPtr;
    private int currentstory = 0;
    // private static float[] timeControl ={0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f,0.5f};
    
    private static string[] intro = {"在某個古老的中世紀酒吧裡\n木製長桌上灰塵飛揚\n沾滿酒漬的桌面反映出暗淡的燭光。", 
                                    "石牆上掛著斑駁的旗幟\n勇士和冒險家的壁畫在暗淡的燈光下閃爍著。", 
                                    "酒吧的角落有著一個區域\n桌子上放著古老的牌組\n周圍圍著一群群酒醉的顧客\n吆喝著、嬉笑打鬧著。",
                                    "人們聚集在一起\n不僅是為了享受美酒佳餚\n更是為了一場激烈的紙牌遊戲。", 
                                    "紙牌在手中翻飛\n與此同時\n挑釁和嘲笑聲也在空氣中交織。",
                                    "古老的故事在酒吧的角落裡重新活現\n每一次選擇都蘊含著機會和挑戰\n等待著那些勇於冒險的人來探索。", 
                                    "在這片滄桑的環境中\n勇者、魔法師和盜賊們相聚一堂\n他們各自擁有著獨特的技能和策略\n試圖在這場紙牌遊戲中佔據優勢。", 
                                    "酒吧的氛圍充滿了刺激和緊張感\n而每一局遊戲都是一次充滿挑戰與機遇的旅程。",
                                    "人們聚在一起\n為了贏得學分\n為了譜寫屬於自己的傳奇故事\n他們將卡牌的世界帶入現實\n與朋友和陌生人一同開展這場精彩絕倫的遊戲之旅。" };

            
    private static string[] start = {"歡迎來到Fate of thrones 請讓我為你進行導覽", 
                                    "首先,我們所在的位置是遊戲大廳,正上方顯示的是玩家資訊,由左到到右分別為\n玩家暱稱\n玩家頭像\n以及玩家經驗", 
                                    "現在請試著點擊玩家頭像\n" };
    private bool startc = false;

    private static string[] user = {"這裡是玩家設定,玩家可以自行調整遊戲相關設定", 
                                        "了解後請點擊離開按鈕\n" };

    private static string[] gatcha = {"這裡是抽卡區域,玩家可以消耗金幣或寶石抽取稀有造型以及具有不同能力的啤酒", 
                                        "左右滑動卡池圖片可以更換卡池,嘗試後請點擊繼續按鈕" };

    private static string[] pg5 = {"這裡是其他對戰區域,玩家可以在這裡選擇\n一般對戰\n排位對戰\n電腦對戰\n自訂對戰等四種模式" 
    };
    
    private static string[] pg1 = {"這裡造型裝備區域,點擊造型圖示可以查看造型介紹以及選擇是否裝備,卡牌共分為四種陣營及六種職業,分別為\n皇家:國王、皇后、王子\n騎士:騎士\n平民:平民\n殺手:殺手",
                                    "接著請試著點擊角色圖示"};

    private static string[] pg1d2 = {"而四種陣營之間也互相存在剋制關係: 分別為\n皇家 勝過 騎士,平民\n騎士 勝過 平民,殺手 \n殺手 勝過 皇家",
                                    "另外,由於皇室鬥爭,皇室內部也存在剋制關係: \n國王勝過王子\n王子勝過皇后\n皇后勝過國王",
                                    "讓我們來看看各個角色對應的關係圖及平民卡技能介紹"};

    private static string[] pg2 = {"這裡是技能裝備區域,擁有的技能會顯示在這裡,點擊技能圖示可以查看技能介紹以及選擇是否裝備,現在請試著點擊技能圖示"};

    private static string[] pg3 = {"恭喜你完成遊戲大廳的導覽\n接著讓我們一起進入遊戲吧"};

    private static string[] enterGame = {"請試著點擊 '電腦對戰' 模式"};

    private static string[] game1 = {"歡迎來到遊戲吧檯,讓我為你介紹場地布置"};


    
    void Start()
    {
        dialoguePanel.SetActive(false);
        button.SetActive(false);
        ClearMask();
        Mainsc.SetActive(true);
        Gamesc.SetActive(false);
        StartCoroutine(Enteranimation());
        
    }

    void MainscGuide(){
        Mainsc.SetActive(true);
        Gamesc.SetActive(false);
        StoryController();
    }

    public void GamescGuide(){
        StartCoroutine(animationstart());
        Gamesc.SetActive(true);
        Mainsc.SetActive(false);
        StartCoroutine(animationend());
        StoryController2();
        
    }

    private IEnumerator Enteranimation() {
    // Transition.SetTrigger("End");
    yield return new WaitForSeconds(transitionTime);
    yield return new WaitForSeconds(startSpeed);
    MainscGuide();
    }

    private IEnumerator animationstart() {
    Transition.SetTrigger("Start");
    yield return new WaitForSeconds(transitionTime);
    }
    private IEnumerator animationend() {
    Transition.SetTrigger("End");
    yield return new WaitForSeconds(transitionTime);
    }

    void  ClearMask(){
        civPanel1.SetActive(false);
        civPanel2.SetActive(false);
        civPanel3.SetActive(false);
        setPanel.SetActive(false);
        skindesPanel.SetActive(false);
        skindesPanel2.SetActive(false);
        skillPanel.SetActive(false);
        skinPanel.SetActive(false);
        MaskPanel1.SetActive(false);
        MaskPanel2.SetActive(false);
        MaskPanel3.SetActive(false);
        MaskPanel4.SetActive(false);
        MaskPanel5.SetActive(false);
        MaskPanel6.SetActive(false);
        MaskPanel7.SetActive(false);
        MaskPanel8.SetActive(false);
    }

    void  ClearMask2(){
        
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
        yield return new WaitForSeconds(0.5f);
        MaskPanel.SetActive(false);
        currentstory++;
        Debug.Log(currentstory);
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
            EnterDialogueMode(intro,9);//9
            break;

            case 1://start
            MaskPanel.SetActive(true);
            EnterDialogueMode(start,3);
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
            EnterDialogueMode(user,2);
            }
            break;

            case 3://gatcha
            if(startc==false){
                startc=true;
                
            }
            else{
            startc=false;
            MaskPanel.SetActive(true);
            EnterDialogueMode(gatcha,2);
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
            EnterDialogueMode(pg1,2);
            }
            break;


            case 6://skinicon
            if(startc==false){
                startc=true;
                MaskPanel5.SetActive(true);
                Debug.Log("5");
            }
            else{
            startc=false;
            ClearMask();
            skindesPanel.SetActive(true);
            currentstory++;
            }
            break;

            case 7://skin
            ClearMask();
            MaskPanel.SetActive(true);
            EnterDialogueMode(pg1d2,3);
            break;

            case 8://skinicon
            ClearMask();
            skindesPanel2.SetActive(true);
            currentstory++;
            break;

            case 9://skillicon
            ClearMask();
            MaskPanel.SetActive(true);
            EnterDialogueMode(pg2,1);
            break;

            case 10://main
            if(startc==false){
                startc=true;
                MaskPanel6.SetActive(true);
            }
            else{
            startc=false;
            ClearMask();
            MaskPanel.SetActive(true);
            EnterDialogueMode(pg3,1);
            }
            break;

            case 11://game
            if(startc==false){
                startc=true;
                MaskPanel7.SetActive(true);
            }
            else{
            startc=false;
            ClearMask();
            MaskPanel.SetActive(true);
            EnterDialogueMode(enterGame,1);
            }
            break;

            case 12:
            startc=false;
            ClearMask();
            MaskPanel8.SetActive(true);
            currentstory ++;
            break;

            case 13:
            MaskPanel.SetActive(true);
            EnterDialogueMode(game1,1);
            break;

            case 14:
            ClearMask2();
            MaskPanel.SetActive(true);
            currentstory++;
            break;


        }
    }

    public void StoryController2(){
        switch(currentstory){

            case 0://intro
            
            break;

            case 1:
            ClearMask2();
            MaskPanel.SetActive(true);
            currentstory++;
            break;


        }
    }


}
