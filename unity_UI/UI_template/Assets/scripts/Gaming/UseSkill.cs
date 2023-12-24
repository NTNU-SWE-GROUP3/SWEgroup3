using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UseSkill : MonoBehaviour
{
    GameController GC;
    public static bool PlayerSkillForbidden;
    public static bool ComSkillNextForbidden;
    public static bool PlayerIsdilemmaDictator;
    public static bool ComIsdilemmaDictator;
    public GameObject PlayerArea;
    public GameObject OpponentArea;
    public GameObject PlayerShow;
    public GameObject SkillPanel;
    public GameObject SkipButton;
    public GameObject ToConfirmButton;
    public GameObject CancelButton;
    public static int Clock = 8;
    public Text TimerText;
    ShowCard SC;
    ToMessagePanel card;
    DeleteChange deleteChange;
    public static int[] ComDilemmaDictatorId = {0,0};
    public static int[] PlayerDilemmaDictatorId = {0,0};
    private int cardId = 0;
    public AudioClip UseSkillVoice;
    public AudioClip ThreeSec;
    AudioSource audioSource;

    private DontDestroy userdata;

    public static bool UseTimeLimit;
    public static bool UseCoinPlus;
    void Start()
    {
        userdata = FindObjectOfType<DontDestroy>();
        UseCoinPlus = false;
        UseTimeLimit = false;
        PlayerSkillForbidden = false;
        ComSkillNextForbidden = false;
        SC = GameObject.Find("GameController").GetComponent<ShowCard>();
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        deleteChange = GameObject.Find("GameController").GetComponent<DeleteChange>();
        audioSource = GetComponent<AudioSource>();
        ComIsdilemmaDictator = false;
        PlayerIsdilemmaDictator = false;
    }
    public IEnumerator Timer()
    {
        TimerText.gameObject.SetActive(true);
        if(SC.skillMessage.text != "抉擇束縛!" || SC.skillDescription.text != "請從以下兩張牌中擇一出牌")
            SkipButton.gameObject.SetActive(true);
        while(Clock > 0 )
        {
            TimerText.text = Clock.ToString();
            yield return new WaitForSeconds(1);
            if (Clock == (3 + 1))
            {
                audioSource.PlayOneShot(ThreeSec);
            }
            Clock -- ;
        }
        SkillPanel.gameObject.SetActive(false);
        TimerText.gameObject.SetActive(false);
        ToConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        GC.DestoryCardOnPanel();
    }

    public IEnumerator Use(int gameType,int skillId,bool isPlayer)
    {
        
        if( isPlayer == true)
        {
        
            switch (skillId)
            {
                case -1:
                    Clock = -1;
                    break;
                case 1: //時間限縮
                    Debug.Log("Player Use Skill 1");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "時間限縮!";
                    SC.skillDescription.text = "此回合出牌時間縮短1秒";
                    UseTimeLimit = true;
                    yield return new WaitForSeconds(2);
                    break;
                case 2: //階級流動
                    Debug.Log("Player Use Skill 2");
                    Clock = 8;
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "階級流動!";
                    SC.skillDescription.text = "請選擇一張要轉換的平民卡";
                    for(int i = 0;i<PlayerArea.transform.childCount;i++)
                    {
                        card = PlayerArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (PlayerArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                        {
                            card.CardShowOnMessagePanel(true);
                        }
                    }
                    yield return(StartCoroutine(Timer()));
                    break;
                case 3: //暗影轉職
                    Debug.Log("Player Use Skill 3");
                    Clock = 8;
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "暗影轉職!";
                    SC.skillDescription.text = "請選擇一張要轉換的平民卡";
                    
                    for(int i = 0;i<PlayerArea.transform.childCount;i++)
                    {
                        card = PlayerArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (PlayerArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                        {
                            card.CardShowOnMessagePanel(true);
                        }
                    }
                    yield return(StartCoroutine(Timer()));
                    break;
                case 4: //技能封印
                    Debug.Log("Player Use Skill 4");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "技能封印!";
                    SC.skillDescription.text = "下回合對手技能將被封印";
                    ComSkillNextForbidden = true;
                    yield return new WaitForSeconds(2);
                    break;
                case 5: //力量剝奪
                    Debug.Log("Player Use Skill 5");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "力量剝奪!";
                    SC.skillDescription.text = "此回合對手民卡技能無效";
                    SC.isPlayerPeasantImmunity = true;
                    yield return new WaitForSeconds(2);
                    break;
                case 6: //黃金風暴
                    Debug.Log("Player Use Skill 6");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "黃金風暴!";
                    SC.skillDescription.text = "玩家獲勝金幣*1.5";
                    UseCoinPlus = true;
                    yield return new WaitForSeconds(2);
                    break;
                case 7: //知己知彼
                    Debug.Log("Player Use Skill 7");
                    Clock = 5;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "知己知彼!";
                    SC.skillDescription.text = "查看對手剩餘手牌";
                    deckRecon();
                    yield return(StartCoroutine(Timer()));
                    break;
                case 8: //抉擇束縛
                    Debug.Log("Player Use Skill 8");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "抉擇束縛!";
                    SC.skillDescription.text = "對手只能從以下兩張牌中擇一出牌";
                    ComIsdilemmaDictator = true;
                    int[] randomIndex = {0,0};
                    randomIndex[0] = Random.Range(0,OpponentArea.transform.childCount);
                    do
                    {
                        randomIndex[1] = Random.Range(0,OpponentArea.transform.childCount);
                    } while (randomIndex[0] == randomIndex[1]);

                    if(gameType == 0)
                    {
                        dilemmaUse gs = gameObject.AddComponent<dilemmaUse>();
                        gs.gameType = userdata.gameType;
                        gs.roomId = userdata.roomId;
                        gs.playerToken = userdata.token;
                        gs.cardId1 = randomIndex[0];
                        gs.cardId2 = randomIndex[1];

                        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"dilemmaUse"));
                        yield return cd.coroutine;
                        Debug.Log("return : " + cd.result);

                        string retString = cd.result.ToString();
                        dilemmaUseBack ret = new dilemmaUseBack();
                        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
                        {
                            Debug.Log("dilemmaUse:" + retString);
                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            ret = dilemmaUseBack.CreateFromJSON(cd.result.ToString());
                        }

                        if(ret.state == -1)
                        {
                            Debug.Log("dilemmaUse:" + ret.errMessage);
                            SceneManager.LoadScene(1);
                            userdata.gameType = 1;
                            userdata.roomId = -2;
                        }
                        else
                        {
                            Debug.Log("dilemmaUse:" + ret.errMessage);
                        }
                    }
                    
                    for(int i = 0;i<OpponentArea.transform.childCount;i++)
                    {
                        card = OpponentArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (i == randomIndex[0])
                        {
                            ComDilemmaDictatorId[0] = OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id;
                            card.CardShowOnMessagePanel(false);
                        }
                        else if (i == randomIndex[1])
                        {
                            ComDilemmaDictatorId[1] = OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id;
                            card.CardShowOnMessagePanel(false);
                        }
                    }
                    yield return StartCoroutine(OpponentFinishCheck());
                    break;
                case 9: //強制徵收
                    Debug.Log("Player Use Skill 9");
                    Clock = 2;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "強制徵收!";
                    SC.skillDescription.text = "對手贏牌區張數-1";
                    SC.OpponentX -= 1;
                    SC.RefreshEarnText(2);
                    yield return new WaitForSeconds(2);
                    break;
                case 10: //勝者之堆
                    Debug.Log("Player Use Skill 10");
                    Clock = 2;
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "勝者之堆!";
                    SC.skillDescription.text = "我方贏牌區張數+1";
                    SC.PlayerX += 1;
                    SC.RefreshEarnText(1);
                    yield return new WaitForSeconds(2);
                    break;
            }
            
        }
        else 
        {
            switch (skillId)
            {
                case -1:
                    Clock = -1;
                    break;
                case 1: //時間限縮
                    Debug.Log("Opponent Use Skill 1");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    audioSource.PlayOneShot(UseSkillVoice);
                    SC.skillMessage.text = "時間限縮!";
                    SC.skillDescription.text = "此回合出牌時間縮短1秒";
                    UseTimeLimit = true;
                    yield return new WaitForSeconds(2);
                    break;
                case 2: //階級流動
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 2");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "階級流動!";
                    SC.skillDescription.text = "等待對手選擇要轉換的平民卡";
                    if(GameController.isCom == true)
                    {
                        for(int i = OpponentArea.transform.childCount-1;i>=0;i--)
                        {
                            if (OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                            {
                                cardId = OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id;
                                deleteChange.Change(OpponentArea,cardId, "階級流動");
                                break;
                            }
                        }
                    }     
                    else if(gameType == 0)
                    {
                        SkillCheck gs = gameObject.AddComponent<SkillCheck>();
                        gs.gameType = userdata.gameType;
                        gs.roomId = userdata.roomId;
                        gs.playerToken = userdata.token;

                        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"useSkillCheck"));
                        yield return cd.coroutine;
                        Debug.Log("return : " + cd.result);

                        string retString = cd.result.ToString();
                        SkillCheckBack ret = new SkillCheckBack();
                        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
                        {
                            Debug.Log("useSkillCheck:" + retString);
                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            ret = SkillCheckBack.CreateFromJSON(cd.result.ToString());
                        }

                        if(ret.cardId == -2)
                        {
                            Debug.Log("useSkillCheck:" + ret.errMessage);
                            SceneManager.LoadScene(1);
                            userdata.gameType = 1;
                            userdata.roomId = -2;
                        }
                        else
                        {
                            cardId = ret.cardId;
                        }

                        yield return deleteChange.Change(OpponentArea,cardId, "階級流動");
                    }

                    yield return StartCoroutine(OpponentFinishCheck());
                    break;
                case 3: //暗影轉職
                    Clock = 4;
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 3");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "暗影轉職!";
                    SC.skillDescription.text = "等待對手選擇要轉換的平民卡";
                    if(GameController.isCom == true)
                    {
                        for(int i = OpponentArea.transform.childCount-1;i>=0;i--)
                        {
                            if (OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().cardName == "平民")
                            {
                                cardId = OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id;
                                deleteChange.Change(OpponentArea,cardId, "暗影轉職");
                                break;
                            }
                        }
                    }    
                    else if(gameType == 0)
                    {
                        SkillCheck gs = gameObject.AddComponent<SkillCheck>();
                        gs.gameType = userdata.gameType;
                        gs.roomId = userdata.roomId;
                        gs.playerToken = userdata.token;

                        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"useSkillCheck"));
                        yield return cd.coroutine;
                        Debug.Log("return : " + cd.result);

                        string retString = cd.result.ToString();
                        SkillCheckBack ret = new SkillCheckBack();
                        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
                        {
                            Debug.Log("useSkillCheck:" + retString);
                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            ret = SkillCheckBack.CreateFromJSON(cd.result.ToString());
                        }

                        if(ret.cardId == -2)
                        {
                            Debug.Log("useSkillCheck:" + ret.errMessage);
                            SceneManager.LoadScene(1);
                            userdata.gameType = 1;
                            userdata.roomId = -2;
                        }
                        else
                        {
                            cardId = ret.cardId;
                        }

                        yield return deleteChange.Change(OpponentArea,cardId, "暗影轉職");
                    }

                    yield return StartCoroutine(OpponentFinishCheck());
                    break;
                case 4: //技能封印
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 4");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "技能封印!";
                    SC.skillDescription.text = "下回合玩家技能將被封印";
                    PlayerSkillForbidden = true;
                    yield return new WaitForSeconds(2f);
                    break;
                case 5: //力量剝奪
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 5");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "力量剝奪!";
                    SC.skillDescription.text = "此回合玩家平民卡技能無效";
                    SC.isComPeasantImmunity = true;
                    yield return new WaitForSeconds(2f);
                    break;
                case 6: //黃金風暴
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 6");
                    yield return new WaitForSeconds(1);
                    break;
                case 7: //知己知彼
                    Clock = 5;
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 7");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "知己知彼!";
                    SC.skillDescription.text = "等待對手查看你的手牌";
                    break;
                case 8: //抉擇束縛
                    Clock = 8;
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 8");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "抉擇束縛!";
                    SC.skillDescription.text = "請從以下兩張牌中擇一出牌";

                    PlayerIsdilemmaDictator = true;
                    SkipButton.SetActive(false);

                    int[] randomIndex = {0,0};
                    if(gameType != 0)
                    {
                        randomIndex[0] = Random.Range(0,PlayerArea.transform.childCount);
                        do
                        {
                            randomIndex[1] = Random.Range(0,PlayerArea.transform.childCount);
                        } while (randomIndex[0] == randomIndex[1]);
                    }
                    else
                    {
                        dilemmaCheck gs = gameObject.AddComponent<dilemmaCheck>();
                        gs.gameType = userdata.gameType;
                        gs.roomId = userdata.roomId;
                        gs.playerToken = userdata.token;

                        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"dilemmaUseCheck"));
                        yield return cd.coroutine;
                        Debug.Log("return : " + cd.result);

                        string retString = cd.result.ToString();
                        dilemmaCheckBack ret = new dilemmaCheckBack();
                        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
                        {
                            Debug.Log("dilemmaUseCheck:" + retString);
                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            ret = dilemmaCheckBack.CreateFromJSON(cd.result.ToString());
                        }

                        if(ret.cardId1 == -1 && ret.cardId2 == -1)
                        {
                            Debug.Log("dilemmaUseCheck:" + ret.errMessage);
                            SceneManager.LoadScene(1);
                            userdata.gameType = 1;
                            userdata.roomId = -2;
                        }
                        else
                        {
                            Debug.Log("dilemmaUseCheck:" + ret.errMessage);
                            randomIndex[0] = ret.cardId1;
                            randomIndex[1] = ret.cardId2;
                        }
                    }
                    
                    for(int i = 0;i<PlayerArea.transform.childCount;i++)
                    {
                        card = PlayerArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
                        if (i == randomIndex[0])
                        {
                            PlayerDilemmaDictatorId[0] = PlayerArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id;
                            card.CardShowOnMessagePanel(true);
                        }
                        else if (i == randomIndex[1])
                        {   PlayerDilemmaDictatorId[1] = PlayerArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id;
                            card.CardShowOnMessagePanel(true);
                        }
                    }  
                    yield return(StartCoroutine(Timer()));
                    break;
                case 9: //強制徵收
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 9");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "強制徵收!";
                    SC.skillDescription.text = "玩家贏牌區張數-1";
                    SC.PlayerX -= 1;
                    SC.RefreshEarnText(1);
                    yield return new WaitForSeconds(2f);
                    break;
                case 10: //勝者之堆
                    audioSource.PlayOneShot(UseSkillVoice);
                    Debug.Log("Opponent  Use Skill 10");
                    SC.WinImage.SetActive(false);
                    SC.SkillImage.SetActive(true);
                    SC.skillMessage.gameObject.SetActive(true);
                    SC.skillDescription.gameObject.SetActive(true);
                    SC.skillMessage.text = "勝者之堆!";
                    SC.skillDescription.text = "對手贏牌區張數+1";
                    SC.OpponentX += 1;
                    SC.RefreshEarnText(2);
                    yield return new WaitForSeconds(2f);
                    break;
            }
        }
        
    
    }
    IEnumerator OpponentFinishCheck()
    {
        if(GameController.isCom == true)
            yield return new WaitForSeconds(4f);
        GC.DestoryCardOnPanel();
    }
    void deckRecon()
    {
        SkipButton.SetActive(true);
        ToMessagePanel card;
        for(int i = 0;i<OpponentArea.transform.childCount;i++)
        {
            card = OpponentArea.transform.GetChild(i).GetComponent<ToMessagePanel>();
            card.CardShowOnMessagePanel(false);
        }
    }

}
