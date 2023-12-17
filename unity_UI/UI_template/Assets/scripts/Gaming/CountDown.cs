using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CountDown : MonoBehaviour
{
    public GameController GC;
    public int countdownTime;
    public static int TurnTime = 5;
    public static bool timeUp = false;
    public Text countdownDisplay;
    public Text TimerText;
    public GameObject PlayerArea;
    public GameObject PlayerShow;
    public GameObject OpponentShow;
    public GameObject ShowDisplay;
    public GameObject MessagePanel;
    public Transform Card;
    ShowCard showcard;

    GameObject PlayerCardObject;
    GameObject OpponentCardObject;
    CardDisplay PlayerCard;
    CardDisplay OpponentCard;

    public AudioClip StartSound;
    public AudioClip ThreeSec;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        MessagePanel.SetActive(false);
        showcard = GameObject.Find("GameController").GetComponent<ShowCard>();
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime -- ;
        }

        GameStart gs = gameObject.AddComponent<GameStart>();
        gs.gameType = 1;
        gs.roomId = 1;
        gs.playerToken = "ABC";

        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"gameStart"));
        yield return cd.coroutine;
        Debug.Log("return : " + cd.result);

        string retString = cd.result.ToString();
        RoomInfo ret = new RoomInfo();
        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
        {
            Debug.Log("GameController/CountdownToStart:" + retString);
            //here should back to login scene
        }
        else
        {
            ret = RoomInfo.CreateFromJSON(cd.result.ToString());
        }

        if(ret.roomId == -1)
        {
            Debug.Log("GameController: this room doesn't exist.");
            //back to game lobby or main scene
        }

        int cardSet = -1;
        if(ret.opponentCardSet == "A")
        {
            cardSet = 0;
        }
        else
        {
            cardSet = 1;
        }

        countdownDisplay.text = "START!";
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(StartSound);
        yield return new WaitForSeconds(1f);
        StartCoroutine(GC.GameBegin(cardSet));
        countdownDisplay.gameObject.SetActive(false);
    }

    public IEnumerator TurnCountdown()
    {
        MessagePanel.SetActive(false);
        TurnTime = 5;
        if(UseSkill.UseTimeLimit == true)
        {
            TurnTime --;
            UseSkill.UseTimeLimit = false;
        }
        timeUp = false;
        TimerText.gameObject.SetActive(true);
        if (ConfirmButton.CardSelected == false && UseSkill.PlayerIsdilemmaDictator == true)
        {
            dilemmaDictator();
            UseSkill.PlayerIsdilemmaDictator = false;
        }
            
        while(TurnTime >= 0)
        {
            TimerText.text = TurnTime.ToString();
            yield return new WaitForSeconds(1);
            if(TurnTime == (3 + 1)){
                audioSource.PlayOneShot(ThreeSec);
            }
            TurnTime -- ;
        }
        timeUp = true;
        DragCard.canDrag = false;
        DropZone.backToHand = true;
        if(PlayerShow.transform.childCount == 0)
            NoPlayCard();

        // pass card info to server 
        PlayerCardObject = PlayerShow.transform.GetChild(0).gameObject;
        OpponentCardObject = OpponentShow.transform.GetChild(0).gameObject;
        PlayerCard = PlayerCardObject.GetComponent<CardDisplay>();
        OpponentCard = OpponentCardObject.GetComponent<CardDisplay>();

        CardSelection selected = gameObject.AddComponent<CardSelection>();
        selected.gameType = 1;
        selected.roomId = 1;
        selected.playerToken = "ABC";
        selected.playerCardID = PlayerCard.id;
        
        CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(selected.SaveToString(),"cardSelection"));
        yield return cd.coroutine;
        Debug.Log("return : " + cd.result);

        string retString = cd.result.ToString();
        MsgBack ret = new MsgBack();
        if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
        {
            Debug.Log("CountDown:" + retString);
            //here should back to login scene
        }
        else
        {
            ret = MsgBack.CreateFromJSON(cd.result.ToString());
        }

        if(ret.OpponentCardId == -1)
        {
            Debug.Log("CountDown:" + ret.errMessage);
            //back to game lobby or Main scene
        }
        else
        {
            Debug.Log("Opponent card:" + ret.OpponentCardId);
        }

        StartCoroutine(showcard.Show(ret.OpponentCardId));
        TimerText.text = "Show!"; 
        yield return new WaitForSeconds(1f);
        DragCard.canDrag = true;
        DropZone.haveCard = false;
        TimerText.gameObject.SetActive(false);
        MessagePanel.SetActive(true);
    }

    public void NoPlayCard()
    {
        Card = PlayerArea.transform.GetChild(PlayerArea.transform.childCount - 1);
        Card.SetParent(PlayerShow.transform,false);
        Card.position = ShowDisplay.transform.position;
        Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
    }
    void dilemmaDictator()
    {
        Transform Card;
        Debug.Log("no card selected");
        for (int i = 0;i<PlayerArea.transform.childCount;i++)
        {
            Card = PlayerArea.transform.GetChild(i);
            if (PlayerArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id == UseSkill.PlayerDilemmaDictatorId[0])
            {
                Card.SetParent(PlayerShow.transform, false);
                Card.position = PlayerShow.transform.position;
                Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
                break;
            }
        } 
    }
}
