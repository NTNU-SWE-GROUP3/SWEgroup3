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
    public GameObject ConfirmButton;
    public GameObject CancelButton;
    public GameObject Panel;
    public GameObject MessagePanel;
    public GameObject SkillName;
    public GameObject WinImage;
    public GameObject DrawArea;
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
    public Image MusicImg;

    AudioSource audioSource;
    //  public GameObject Skill;
    void Start()
    {
        // WinImage.SetActive(true);
        // Instantiate(Skill,transform.position,transform.rotation).transform.SetParent(WinImage.transform,true);

        isCom = true;
        SkillName.SetActive(false);
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

    public void GameBegin()
    {
        Turn = 0;
        drawCard.Draw();
        // Game Start
        StartCoroutine(TurnStart());
    }
    public IEnumerator TurnStart()
    {
       
        Turn++;
        MessagePanel.SetActive(true);
        ClickDetector.cardId = -1;
        for(int i = 0 ; i < Panel.transform.childCount;i++)
        {
            Destroy(Panel.transform.GetChild(i).gameObject);
        }
        ConfirmButton.SetActive(false);
        CancelButton.SetActive(false);
        SkillName.SetActive(false);
        WinImage.SetActive(true);
        NextRoundText.gameObject.SetActive(true);
        NextRoundText.text = "Round" + Turn.ToString();
        yield return new WaitForSeconds(1);
        MessagePanel.SetActive(false);
        DropZone.haveCard = false;
        DropZone.backToHand = true;
        TurnText.text = "回合:" + Turn.ToString();
        StartCoroutine(Timer.TurnCountdown());
        if(isCom == true)
        {
            ComPlayer.PlayRandomCard();
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
            SkillName.SetActive(false);
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
}