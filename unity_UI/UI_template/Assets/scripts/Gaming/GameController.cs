using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Networking;
using MiniJSON;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public string apiUrl = "http://140.122.185.169:5050/gaming/get_skills_card_styles";
    public string response;

    public GameObject FinishPanel;
    public Image FinishPanelImage;
    public Text FinishPanelCoin;
    public Text FinishPanelLV;

    public DrawCard drawCard;
    public CardDatabase cardDatabase;
    public static bool isCom;
    public ComputerPlayer ComPlayer;
    public CountDown Timer;
    public ShowSkill showSkill;
    public static int Turn;
    public Text TurnText;
    public GameObject PlayerShow;
    public GameObject SkipButton;
    public GameObject ConfirmButton;
    public GameObject CancelButton;
    public GameObject CardPanel;
    public GameObject MessagePanel;
    public GameObject SkillPanel;
    public GameObject SkillImage;
    public GameObject WinImage;
    public GameObject DrawArea;
    public Text SkillMassage;
    public Text SkillDescription;
    public Text DrawAreaCount;
    public Text NextRoundText;
    public AudioClip EndSound;
    public AudioClip VictoryMusic;
    public AudioClip VictoryVoice;
    public AudioClip DefeatMusic;
    public AudioClip DefeatVoice1;
    public AudioClip DefeatVoice2;
    public AudioClip DefeatVoice3;
    public AudioClip DrawVoice1;
    public AudioClip PlayerSkillVoice1;
    public AudioClip PlayerSkillVoice2;
    public AudioManager audioManager;
    bool NoSkillCanUse;
    public Image MusicImg;
    public Slider slider;
    public static string WinOrLose;
    public UseSkill useSkill;
    public static int PlayerSkillId;
    public static int OpponentSkillId;
    public static bool OpponentFUS;
    AudioSource audioSource;
    bool ComSkillForbidden;

    public Text PlayerEarnText;
    public Text OpponentEarnText;
    // int id;

    private DontDestroy userdata;

    void Start()
    {
        userdata = FindObjectOfType<DontDestroy>();
        OpponentFUS = false;
        PlayerSkillId = -1;
        OpponentSkillId = -1;
        NoSkillCanUse = false;
        if(userdata.gameType == 0)
        {
            isCom = false;
        }
        else
        {
            isCom = true;
        }
        
        FinishPanel.SetActive(false);
        SkipButton.SetActive(false);
        SkillPanel.SetActive(false);
        SkillImage.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        cardDatabase  = GameObject.Find("CardDatabase").GetComponent<CardDatabase>();
        drawCard = GameObject.Find("GameController").GetComponent<DrawCard>();
        Timer = GameObject.Find("GameController").GetComponent<CountDown>();
        audioManager = GameObject.Find("AudioBox").GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();
        if (isCom == true)
        {
            ComPlayer = GameObject.Find("ComputerPlayer").GetComponent<ComputerPlayer>();
        }
        // id = PlayerPrefs.GetInt("id");
        ShowSkill.PlayerSkillIdList = LoadSkills();
        StartCoroutine(showSkill.ShowSkills());
        Debug.Log(string.Join(", ", ShowSkill.PlayerSkillIdList));
        CardDatabase.cardStyleIdList = LoadStyles();
        cardDatabase.Create();
        Debug.Log(string.Join(", ", CardDatabase.cardStyleIdList));



    }
    void Update()
    {
        DrawAreaCount.text = "平手區累積牌數: " + DrawArea.transform.childCount.ToString() + "張";
    }

    public IEnumerator GameBegin(int gameType,int n)
    {
        Turn = 0;
        yield return StartCoroutine(drawCard.Drawing(gameType,n));
        // Game Start
        StartCoroutine(TurnStart(gameType));
    }
    public IEnumerator TurnStart(int gameType)
    {
        if(gameType == 0)
        {
            GameTurn turnStartSignal = gameObject.AddComponent<GameTurn>();
            turnStartSignal.gameType = gameType;
            turnStartSignal.roomId = userdata.roomId;
            turnStartSignal.playerToken = userdata.token;
            turnStartSignal.playerEarn = Convert.ToInt32(PlayerEarnText.text);
            turnStartSignal.opponentEarn = Convert.ToInt32(OpponentEarnText.text);
            
            CoroutineWithData cd2 = new CoroutineWithData(this, Flask.SendRequest(turnStartSignal.SaveToString(),"turnStart"));
            yield return cd2.coroutine;
            Debug.Log("return : " + cd2.result);

            string retString = cd2.result.ToString();
            TrunStat ret = new TrunStat();
            if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
            {
                Debug.Log("GameController:" + retString);
                SceneManager.LoadScene(0);
                
            }
            else
            {
                ret = TrunStat.CreateFromJSON(cd2.result.ToString());
            }

            if(ret.state == -1)
            {
                Debug.Log("GameController:" + ret.errMessage);
                SceneManager.LoadScene(1);
                userdata.gameType = 1;
                userdata.roomId = "None";
            }
            else
            {
                Debug.Log("GameController:" + ret.errMessage);
            }
        }

        Turn++;
        MessagePanel.SetActive(true);
        DestoryCardOnPanel();
        SkillPanel.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        SkillImage.SetActive(false);
        WinImage.SetActive(true);
        NextRoundText.gameObject.SetActive(true);
        NextRoundText.text = "Round" + Turn.ToString();
        yield return new WaitForSeconds(1);

        DragCard.canDrag = true;

        WinImage.SetActive(false);
        SkillPanel.SetActive(true);
        SkillImage.SetActive(true);

        if (isCom == true || gameType == 0)
        {
            if (UseSkill.ComSkillNextForbidden == true)
            {
                ComSkillForbidden = true;
                UseSkill.ComSkillNextForbidden = false;
            }

        }

        if (gameType != 0)
        {
            if(isCom == true && ComputerPlayer.ComSkillIndex < 3 && ComSkillForbidden == false)
            {
                Debug.Log("Opponent Start Choosing Skill");
                StartCoroutine(ComPlayer.ToUseSkill());
                
            }
            else if(ComputerPlayer.ComSkillIndex >= 3)
            {
                Debug.Log("Opponent have no skill left");
                if(isCom == true)
                {
                    OpponentFUS = true;
                }
            }
            else if(ComSkillForbidden == true)
            {
                Debug.Log("Opponent can't not use skill this round");
                ComSkillForbidden = false;
                if(isCom == true)
                {
                    OpponentFUS = true;
                }
            }
        }
        else
        {
            if(ComSkillForbidden == true)
            {
                Debug.Log("Opponent can't not use skill this round");
                ComSkillForbidden = false;
                if(isCom == true)
                {
                    OpponentFUS = true;
                }
            }
        }

        if (NoSkillCanUse == false)
        {
            NoSkillCanUse = true;
            for (int i = 0; i < SkillPanel.transform.childCount; i++)
            {
                if (SkillPanel.transform.GetChild(i).gameObject.layer != 14)
                {
                    NoSkillCanUse = false;
                    break;
                }
            }

        }
        if (NoSkillCanUse == false)
        {
            if (UseSkill.PlayerSkillForbidden == false)
            {
                audioSource.PlayOneShot(PlayerSkillVoice1);
                SkillMassage.text = "請選擇要使用的技能";
                SkillDescription.text = "";
                SkipButton.SetActive(true);

                for (int i = 0; i < SkillPanel.transform.childCount; i++)
                {
                    if (SkillPanel.transform.GetChild(i).gameObject.layer == 15)
                        SkillPanel.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Skill(Unused)");
                }
                UseSkill.Clock = 8;
                yield return StartCoroutine(useSkill.Timer());
                ClickDetector.skillId = -1;
            }
            else
            {
                audioSource.PlayOneShot(PlayerSkillVoice2);
                SkillMassage.text = "此回合無法使用技能";
                SkillDescription.text = "";
                SkipButton.SetActive(true);

                for (int i = 0; i < SkillPanel.transform.childCount; i++)
                {
                    if (SkillPanel.transform.GetChild(i).gameObject.layer == 13)
                        SkillPanel.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Skill(Forbidden)");
                }
                UseSkill.Clock = 8;
                yield return StartCoroutine(useSkill.Timer());

                ClickDetector.skillId = -1;
                UseSkill.PlayerSkillForbidden = false;
            }
        }
        else
        {
            audioSource.PlayOneShot(PlayerSkillVoice2);
            SkillMassage.text = "已無技能可以使用";
            SkillDescription.text = "";
            SkipButton.SetActive(true);
            UseSkill.Clock = 8;
            yield return StartCoroutine(useSkill.Timer());
        }

        MessagePanel.SetActive(true);
        SkillPanel.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        SkipButton.SetActive(false);
        SkillMassage.text = "等待對手使用技能";
        SkillDescription.text = "";

        if(gameType != 0)
        {
            while(OpponentFUS == false)
            {
                yield return new WaitForSeconds(1f);
            }
        }
        
        if(gameType == 0)
        {
            //---pass player skill id to server and receive opponent skill id------
            SkillSelection selected = gameObject.AddComponent<SkillSelection>();
            selected.gameType = gameType;
            selected.roomId = userdata.roomId;
            selected.playerToken = userdata.token;
            selected.playerSkillID = PlayerSkillId;
            
            CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(selected.SaveToString(),"skill"));
            yield return cd.coroutine;
            Debug.Log("return : " + cd.result);

            string retString2 = cd.result.ToString();
            SkillMsgBack ret2 = new SkillMsgBack();
            if (retString2 == "ConnectionError" || retString2 == "ProtocolError" || retString2 == "InProgress" || retString2 == "DataProcessingError")
            {
                Debug.Log("GameController:" + retString2);
                SceneManager.LoadScene(0);
            }
            else
            {
                ret2 = SkillMsgBack.CreateFromJSON(cd.result.ToString());
            }

            if(ret2.OpponentSkillId == -1)
            {
                Debug.Log("GameController:" +ret2.errMessage);
                SceneManager.LoadScene(1);
                userdata.gameType = 1;
                userdata.roomId = "None";
            }
            else
            {
                Debug.Log("Opponent skill:" + ret2.OpponentSkillId);
                OpponentSkillId = ret2.OpponentSkillId;
            }

            //---------------------------------------------------------------------
        }

        Debug.Log("PLayer SUS" + PlayerSkillId);
        yield return StartCoroutine(useSkill.Use(gameType,PlayerSkillId, true));
        PlayerSkillId = -1;
        Debug.Log("PLayer FUS");
        yield return new WaitForSeconds(1f);
        Debug.Log("Oppo SUS " + OpponentSkillId);
        yield return StartCoroutine(useSkill.Use(gameType,OpponentSkillId, false));
        OpponentSkillId = -1;
        OpponentFUS = false;
        Debug.Log("Oppo FUS");


        MessagePanel.SetActive(false);
        SkillImage.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        SkipButton.SetActive(false);

        if (PlayerShow.transform.childCount == 0)
        {
            DropZone.haveCard = false;
            DropZone.backToHand = true;
            DragCard.canDrag = true;
        }
        else
        {
            DragCard.canDrag = false;
        }

        TurnText.text = "回合:" + Turn.ToString();
        StartCoroutine(Timer.TurnCountdown(gameType));
        if (isCom == true)
        {
            yield return StartCoroutine(ComPlayer.PlayCard());
        }

    }

    public void FinishCheck(int gameType,int PlayerEarnCard, int OpponentEarnCard, int PlayerHandCard, int OpponentHandCard)
    {
        if (OpponentEarnCard < 2 && PlayerEarnCard < 2 && PlayerHandCard > 0 && OpponentHandCard > 0)
            StartCoroutine(TurnStart(gameType));
        else
        {
            audioManager.StopBGM();
            audioSource.PlayOneShot(EndSound);
            SkillImage.SetActive(false);
            WinImage.SetActive(true);
            NextRoundText.gameObject.SetActive(true);
            if (PlayerEarnCard >= 2)
            {
                NextRoundText.text = "VICTORY";
                StartCoroutine(VictorySE());
                WinOrLose = "win";
                StartCoroutine(GameFinishPanel(WinOrLose));
            }
            else if (OpponentEarnCard >= 2)
            {
                NextRoundText.text = "DEFEAT";
                StartCoroutine(DefeatSE());
                WinOrLose = "lose";
                StartCoroutine(GameFinishPanel(WinOrLose));
            }
            else
            {
                if (PlayerEarnCard > OpponentEarnCard)
                {
                    NextRoundText.text = "VICTORY";
                    StartCoroutine(VictorySE());
                    WinOrLose = "win";
                    StartCoroutine(GameFinishPanel(WinOrLose));
                }
                else if (OpponentEarnCard > PlayerEarnCard)
                {
                    NextRoundText.text = "DEFEAT";
                    StartCoroutine(DefeatSE());
                    WinOrLose = "lose";
                    StartCoroutine(GameFinishPanel(WinOrLose));
                }
                else
                {
                    NextRoundText.text = "Tie";
                    StartCoroutine(DrawSE());
                    WinOrLose = "tie";
                    StartCoroutine(GameFinishPanel(WinOrLose));
                }

            }
            CardDatabase.cardList.Clear();
        }
    }

    IEnumerator GameFinishPanel(string winorlose)
    {
        yield return new WaitForSeconds(2f);
        if(winorlose == "win")
        {
            FinishPanelImage.sprite = Resources.Load<Sprite>("images/GameSc/Finish/VictoryBanner");
            FinishPanelLV.text = "+200";
            if(UseSkill.UseCoinPlus == true)
                FinishPanelCoin.text = "+300";
            
            else
                FinishPanelCoin.text = "+200";

        }
        else if(winorlose == "lose")
        {
            FinishPanelImage.sprite = Resources.Load<Sprite>("images/GameSc/Finish/DefeatBanner");
            if(UseSkill.UseCoinPlus == true)
                FinishPanelCoin.text = "+75";
            else
                FinishPanelCoin.text = "+50";
            FinishPanelLV.text = "+50";
        }
        else
        {
            FinishPanelImage.sprite = Resources.Load<Sprite>("images/GameSc/Finish/TieBanner");
            if(UseSkill.UseCoinPlus == true)
                FinishPanelCoin.text = "+150";
            else
                FinishPanelCoin.text = "+100";
            FinishPanelLV.text = "+100";
        }
        userdata.gameType = 1;
        userdata.roomId = "None";
        FinishPanel.SetActive(true);
    }

    IEnumerator VictorySE()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        float vol = slider.value;
        yield return new WaitForSeconds(2.5f);
        audioSource.PlayOneShot(VictoryVoice);
        audioSource.PlayOneShot(VictoryMusic, vol);
    }
    IEnumerator DefeatSE()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        yield return new WaitForSeconds(2.5f);
        int RandNum = UnityEngine.Random.Range(0, 2);
        if (RandNum == 0)
        {
            audioSource.PlayOneShot(DefeatVoice1);
        }
        else if (RandNum == 1)
        {
            audioSource.PlayOneShot(DefeatVoice2);
        }
        else
        {
            audioSource.PlayOneShot(DefeatVoice3);
        }

        if (MusicImg.sprite == Resources.Load<Sprite>("images/Music1"))
        {
            audioSource.PlayOneShot(DefeatMusic);
        }
    }
    IEnumerator DrawSE()
    {
        yield return new WaitForSeconds(2.5f);
        audioSource.PlayOneShot(DrawVoice1);
    }
    public void DestoryCardOnPanel()
    {
        for (int i = 0; i < CardPanel.transform.childCount; i++)
        {
            ClickDetector.cardId = -1;
            Destroy(CardPanel.transform.GetChild(i).gameObject);
        }
    }

    List<int> LoadSkills()
    {
        string skillsString = PlayerPrefs.GetString("skills", "");

        if (!string.IsNullOrEmpty(skillsString))
        {
            string[] skillStrings = skillsString.Split(',');

            List<int> skills = new List<int>();
            List<int> random_skills = new List<int>();
            foreach (string skillStr in skillStrings)
            {
                int id = System.Convert.ToInt32(skillStr);
                skills.Add(id);
            }
            Debug.Log("Loaded Skills: " + string.Join(", ", skills));
        
            return skills;

        
        }
        else
        {
            Debug.Log("No skills found in PlayerPrefs.");
            return new List<int>();
        }
    }
    List<int> LoadStyles()
    {
        string stylesString = PlayerPrefs.GetString("card_styles", "");

        if (!string.IsNullOrEmpty(stylesString))
        {
            string[] styleStrings = stylesString.Split(',');

            List<int> styles = new List<int>();
            foreach (string styleStr in styleStrings)
            {
                int id = System.Convert.ToInt32(styleStr);
                styles.Add(id);
            }

            Debug.Log("Loaded Styles: " + string.Join(", ", styles));
            return styles;
        }
        else
        {
            Debug.Log("No styles found in PlayerPrefs.");
            return new List<int>();
        }
    }
}