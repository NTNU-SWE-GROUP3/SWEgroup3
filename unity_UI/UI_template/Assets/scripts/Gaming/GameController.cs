using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public DrawCard drawCard;
    public bool isCom;
    public ComputerPlayer ComPlayer;
    public CountDown Timer;
    public static int Turn;
    public Text TurnText;
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
    public AudioManager audioManager;
    bool NoSkillCanUse;
    public Image MusicImg;

    public UseSkill useSkill;
    AudioSource audioSource;
    
    void Start()
    {
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
        WinImage.SetActive(false);
        SkillPanel.SetActive(true);
            SkillImage.SetActive(true);
        if(NoSkillCanUse == false)
        {
            if(SkillPanel.transform.GetChild(0).gameObject.layer == 14 && SkillPanel.transform.GetChild(1).gameObject.layer == 14 && SkillPanel.transform.GetChild(2).gameObject.layer == 14)
                NoSkillCanUse = true;
        }
        if(NoSkillCanUse == false)
        {
            SkillMassage.text = "請選擇要使用的技能";
            SkillDescription.text = "";
            yield return StartCoroutine(useSkill.Timer());
            ClickDetector.skillId = -1;
        }
        else 
        {
            SkillMassage.text = "已無技能可以使用";
            SkillDescription.text = "";
            SkipButton.SetActive(true);
            yield return StartCoroutine(useSkill.Timer());
        }

        

        MessagePanel.SetActive(false);
        SkillPanel.SetActive(false);
        SkillImage.SetActive(false);
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);

        DropZone.haveCard = false;
        DropZone.backToHand = true;

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
                }

            }
        }
    }

    
    IEnumerator VictorySE()
    {
        MusicImg = GameObject.Find("MusicButton").GetComponent<Image>();
        yield return new WaitForSeconds(2.5f);
        audioSource.PlayOneShot(VictoryVoice);
        if(MusicImg.sprite == Resources.Load<Sprite>("images/Music1")){
            audioSource.PlayOneShot(VictoryMusic);
        }
    }
    IEnumerator DefeatSE()
    {
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
    public void DestoryCardOnPanel()
    {
        Debug.Log("Start Delete");
        for(int i = 0 ; i < CardPanel.transform.childCount;i++)
        {
            ClickDetector.cardId = -1;
            Destroy(CardPanel.transform.GetChild(i).gameObject);
        }
    }
}