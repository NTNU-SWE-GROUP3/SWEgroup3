using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{

    public DrawCard drawCard;
    public static bool isCom;
    public ComputerPlayer ComPlayer;
    public CountDown Timer;
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

    public UseSkill useSkill;
    public static int PlayerSkillId;
    public static int OpponentSkillId;
    public static bool OpponentFUS;
    AudioSource audioSource;
    bool ComSkillForbidden;
    
    void Start()
    {
        OpponentFUS = false;
        PlayerSkillId = -1;
        OpponentSkillId = -1;
        NoSkillCanUse = false;
        isCom = true;
        SkipButton.SetActive(false);
        SkillPanel.SetActive(false);
        SkillImage.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        drawCard = GameObject.Find("GameController").GetComponent<DrawCard>();
        Timer = GameObject.Find("GameController").GetComponent<CountDown>();
        audioManager = GameObject.Find("AudioBox").GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();
        if(isCom == true)
        {
            ComPlayer = GameObject.Find("ComputerPlayer").GetComponent<ComputerPlayer>();
        }
    }
    void Update()
    {
        DrawAreaCount.text = "平手區累積牌數: " + DrawArea.transform.childCount.ToString() + "張";
    }

    public IEnumerator GameBegin()
    {
        Turn = 0;
        yield return StartCoroutine(drawCard.Drawing());
        // Game Start
        StartCoroutine(TurnStart());
    }
    public IEnumerator TurnStart()
    {
       
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

        if(isCom == true)
        {
            if (UseSkill.ComSkillNextForbidden == true)
            {
                ComSkillForbidden = true;
                UseSkill.ComSkillNextForbidden = false;
            }

        }
        
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

        if(NoSkillCanUse == false)
        {
            NoSkillCanUse = true;
            for(int i = 0; i<SkillPanel.transform.childCount;i++)
            {
                if(SkillPanel.transform.GetChild(i).gameObject.layer != 14 )
                {
                    NoSkillCanUse = false;
                    break;
                }
            }
                
        }
        if(NoSkillCanUse == false)
        {
            if(UseSkill.PlayerSkillForbidden == false)
            {
                audioSource.PlayOneShot(PlayerSkillVoice1);
                SkillMassage.text = "請選擇要使用的技能";
                SkillDescription.text = "";
                SkipButton.SetActive(true);

                for(int i = 0; i<SkillPanel.transform.childCount;i++)
                {
                    if(SkillPanel.transform.GetChild(i).gameObject.layer == 15)
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

                for(int i = 0; i<SkillPanel.transform.childCount;i++)
                {
                    if(SkillPanel.transform.GetChild(i).gameObject.layer == 13)
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

        while(OpponentFUS == false)
        {
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("PLayer SUS" + PlayerSkillId);
        yield return StartCoroutine(useSkill.Use(PlayerSkillId,true));
        PlayerSkillId = -1;
        Debug.Log("PLayer FUS");
        yield return new WaitForSeconds(1f);
        Debug.Log("Oppo SUS " + OpponentSkillId);
        yield return StartCoroutine(useSkill.Use(OpponentSkillId,false));
        OpponentSkillId = -1;
        OpponentFUS = false;
        Debug.Log("Oppo FUS");


        MessagePanel.SetActive(false);
        SkillImage.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        SkipButton.SetActive(false);
        
        if(PlayerShow.transform.childCount == 0)
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
        StartCoroutine(Timer.TurnCountdown());
        if(isCom == true)
        {
            yield return StartCoroutine(ComPlayer.PlayCard());
        }

    }

    public void FinishCheck(int PlayerEarnCard,int OpponentEarnCard,int PlayerHandCard,int OpponentHandCard)
    {
        if( OpponentEarnCard< 10 && PlayerEarnCard < 10 && PlayerHandCard > 0 && OpponentHandCard > 0)
            StartCoroutine(TurnStart());
        else
        {
            audioManager.StopBGM();
            audioSource.PlayOneShot(EndSound);
            SkillImage.SetActive(false);
            WinImage.SetActive(true);
            NextRoundText.gameObject.SetActive(true);
            if(PlayerEarnCard >= 10 )
            {
                NextRoundText.text = "VICTORY";
                StartCoroutine(VictorySE());
            }
            else if (OpponentEarnCard >= 10)
            {
                NextRoundText.text = "DEFEAT";
                StartCoroutine(DefeatSE());
            }
            else
            {
                if(PlayerEarnCard > OpponentEarnCard)
                {
                    NextRoundText.text = "VICTORY";
                    StartCoroutine(VictorySE());
                }
                else if(OpponentEarnCard > PlayerEarnCard)
                {
                    NextRoundText.text = "DEFEAT";
                    StartCoroutine(DefeatSE());
                }
                else
                {
                    NextRoundText.text = "Draw";
                    StartCoroutine(DrawSE());
                }

            }
        }
    }

    
    IEnumerator VictorySE()
    {
        audioManager.StopBGM();
        MusicImg = GameObject.Find("MusicButton").GetComponent<Image>();
        yield return new WaitForSeconds(2.5f);
        audioSource.PlayOneShot(VictoryVoice);
        if(MusicImg.sprite == Resources.Load<Sprite>("images/Music1")){
            audioSource.PlayOneShot(VictoryMusic);
        }
    }
    IEnumerator DefeatSE()
    {
        audioManager.StopBGM();
        MusicImg = GameObject.Find("MusicButton").GetComponent<Image>();
        yield return new WaitForSeconds(2.5f);
        int RandNum = Random.Range(0, 2);
        if(RandNum == 0)
        {
            audioSource.PlayOneShot(DefeatVoice1);
        }
        else if(RandNum == 1)
        {
            audioSource.PlayOneShot(DefeatVoice2);
        }
        else
        {
            audioSource.PlayOneShot(DefeatVoice3);
        }

        if(MusicImg.sprite == Resources.Load<Sprite>("images/Music1")){
            audioSource.PlayOneShot(DefeatMusic);
        }
    }
    IEnumerator DrawSE()
    {
        audioManager.StopBGM();
        yield return new WaitForSeconds(2.5f);
        audioSource.PlayOneShot(DrawVoice1);
    }
    public void DestoryCardOnPanel()
    {
        for(int i = 0 ; i < CardPanel.transform.childCount;i++)
        {
            ClickDetector.cardId = -1;
            Destroy(CardPanel.transform.GetChild(i).gameObject);
        }
    }
}