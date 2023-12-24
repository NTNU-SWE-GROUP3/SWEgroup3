using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public GameController GC;
    public int countdownTime;
    public static int TurnTime = 5;
    public static bool timeUp = false;
    public static int gameType ;
    public Text countdownDisplay;
    public Text TimerText;
    public GameObject PlayerArea;
    public GameObject PlayerShow;
    public GameObject OpponentArea;
    public GameObject OpponentShow;
    public GameObject ShowDisplay;
    public GameObject MessagePanel;
    public Transform Card;
    ShowCard showcard;
    public static int playerCardSet = -1;

    GameObject PlayerCardObject;
    GameObject OpponentCardObject;
    CardDisplay PlayerCard;
    CardDisplay OpponentCard;

    public AudioClip StartSound;
    public AudioClip ThreeSec;
    AudioSource audioSource;
    private DontDestroy userdata;

    // Start is called before the first frame update
    private void Start()
    {
        userdata = FindObjectOfType<DontDestroy>();
        MessagePanel.SetActive(false);
        showcard = GameObject.Find("GameController").GetComponent<ShowCard>();
        gameType = userdata.gameType;//0 for PVP
        
        StartCoroutine(CountdownToStart(gameType));
    }

    IEnumerator CountdownToStart(int gameType)
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime -- ;
        }

        int cardSet = -1;
        if(gameType == 0)
        {
            GameStart gs = gameObject.AddComponent<GameStart>();
            gs.gameType = userdata.gameType;
            gs.roomId = userdata.roomId;
            gs.playerToken = userdata.token;

            CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(gs.SaveToString(),"getCardSet"));
            yield return cd.coroutine;
            Debug.Log("return : " + cd.result);

            string retString = cd.result.ToString();
            RoomInfo ret = new RoomInfo();
            if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
            {
                Debug.Log("CountDown/CountdownToStart:" + retString);
                SceneManager.LoadScene(0);
            }
            else
            {
                ret = RoomInfo.CreateFromJSON(cd.result.ToString());
            }

            if(ret.roomId == "None")
            {
                Debug.Log("CountDown: this room doesn't exist.");
                SceneManager.LoadScene(1);
                userdata.gameType = 1;
                userdata.roomId = "None";
            }

            
            if(ret.playerCardSet == "A")
            {
                playerCardSet = 0;
                cardSet = 0;
            }
            else
            {
                playerCardSet = 1;
                cardSet = 1;
            }
        }

        countdownDisplay.text = "START!";
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(StartSound);
        yield return new WaitForSeconds(1f);
        StartCoroutine(GC.GameBegin(gameType,cardSet));
        countdownDisplay.gameObject.SetActive(false);
    }

    public IEnumerator TurnCountdown(int gameType)
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

        if(gameType == 0)
        {
            // pass card info to server 
            PlayerCardObject = PlayerShow.transform.GetChild(0).gameObject;
            PlayerCard = PlayerCardObject.GetComponent<CardDisplay>();
            
            CardSelection selected = gameObject.AddComponent<CardSelection>();
            selected.gameType = gameType;
            selected.roomId = userdata.roomId;
            selected.playerToken = userdata.token;
            selected.playerCardID = PlayerCard.id;
            
            
            CoroutineWithData cd = new CoroutineWithData(this, Flask.SendRequest(selected.SaveToString(),"cardSelection"));
            yield return cd.coroutine;
            Debug.Log("return : " + cd.result);

            string retString = cd.result.ToString();
            MsgBack ret = new MsgBack();
            if (retString == "ConnectionError" || retString == "ProtocolError" || retString == "InProgress" || retString == "DataProcessingError")
            {
                Debug.Log("CountDown:" + retString);
                SceneManager.LoadScene(0);
            }
            else
            {
                ret = MsgBack.CreateFromJSON(cd.result.ToString());
            }

            if(ret.OpponentCardId == -1)
            {
                Debug.Log("CountDown:" + ret.errMessage);
                SceneManager.LoadScene(1);
                userdata.gameType = 1;
                userdata.roomId = "None";
            }
            else
            {
                Debug.Log("Opponent card:" + ret.OpponentCardId);
            }

            Transform Card;
            int test = -1;
            Debug.Log("OpponentArea.transform.childCount:" + OpponentArea.transform.childCount);
            for(int i = 0;i<OpponentArea.transform.childCount;i++)
            {
                Debug.Log(OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id);
                if (OpponentArea.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().id == ret.OpponentCardId && test != 0)
                {
                    test = 0;
                    Card = OpponentArea.transform.GetChild(i);
                    Card.SetParent(OpponentShow.transform,false);
                    Card.position = OpponentShow.transform.position;
                    Card.gameObject.layer = LayerMask.NameToLayer("CardBack");
                    //yield return new WaitForSeconds(1f);
                    //break;
                }
            }

            if(test == -1)
            {
                Debug.Log("Didn't find the card from opponent");
                SceneManager.LoadScene(1);
                userdata.gameType = 1;
                userdata.roomId = "None";
            }
        }

        StartCoroutine(showcard.Show(gameType));
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
