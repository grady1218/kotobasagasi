using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransSystem : MonoBehaviour
{
    public GameObject UserPlate;
    public GameObject Canvas;
    public GameObject NoTouchObj;
    public GameObject[] EndUIs;
    public bool TransStart = false;
    private PhotonPlayer[] PhotonPlayers;
    private GameObject[] Plates;

    private string state = "PreStart";
    private string _roomName = "";
    private int _maxPlayerNum = 0;
    private int _turn = 1;
    private int _inRoomOrder = 0;


    public enum Events : byte
    {
        SelectOrder,
        Next,
        Clear,
        SendState,
        Answer,
        QuestionString
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;

        _inRoomOrder = PhotonNetwork.room.PlayerCount;
        _roomName = PhotonNetwork.room.Name;
        _maxPlayerNum = PhotonNetwork.room.MaxPlayers;

        
        Canvas.GetComponent<GenerateTextButtons>().Genetate();
    }

    public void StartUp()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Debug.Log("master");
            Canvas.GetComponent<GenerateQuestion>().Generate();
            PhotonPlayers = PhotonNetwork.playerList.OrderBy(i => Guid.NewGuid()).ToArray();
            PhotonNetwork.RaiseEvent((byte)Events.SelectOrder, PhotonPlayers, true, null);
            PhotonNetwork.RaiseEvent((byte)Events.QuestionString, Canvas.GetComponent<GenerateQuestion>().QT.selectedText, true, null);
            state = "SelectOrder";
        }
    }

    private void GeneratePlayerPlate()
    {
        Plates = new GameObject[PhotonPlayers.Length];

        for(int i = 0; i < PhotonPlayers.Length; i++)
        {
            var uPlate = Instantiate(UserPlate);
            Plates[i] = uPlate;

            uPlate.transform.SetParent(Canvas.transform);
            uPlate.transform.localScale = Vector3.one;
            uPlate.GetComponent<RectTransform>().anchoredPosition = new Vector2(-600 + (i * 400), 50);

            uPlate.transform.GetChild(0).GetComponent<Text>().text = $"Name : {PhotonPlayers[i].NickName}";
            uPlate.GetComponent<Animator>().SetBool("IsPlayerJoined", true);

        }

    }

    private void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId)
    {
        switch (eventCode){
            case (byte)Events.SelectOrder: SelectOrder(content); break;
            case (byte)Events.Next: Next(); break;
            case (byte)Events.Answer: Answer(content); break;
            case (byte)Events.QuestionString: QuestionString(content); break;
            case (byte)Events.Clear: Clear(); break;
        }
    }

    private void SelectOrder(object obj)
    {
        Debug.Log("SelectOrder");
        PhotonPlayers = (PhotonPlayer[])obj;
        state = "SelectOrder";
    }

    public void Answer(object answer)
    {
        var p = Plates[_turn-1].transform;
        if(Canvas.GetComponent<GenerateQuestion>().IsCorrectAnswer((string)answer))
        {
            p.GetChild(1).GetComponent<Text>().text = "あたり！";
            p.GetComponent<Animator>().SetBool("Answer",true);
        }
        else
        {
            p.GetChild(1).GetComponent<Text>().text = "はずれ…";
            p.GetComponent<Animator>().SetBool("Answer", true);
            Canvas.GetComponent<GenerateQuestion>().TextField.GetComponent<Text>().text = (string)answer;
        }
    }

    public void Next()
    {
        if (++_turn > PhotonPlayers.Length) _turn = 1;
        CheckOrder(_turn);
    }

    public void Clear()
    {
        PhotonNetwork.LeaveRoom();
        EndUIs[0].GetComponent<Text>().text = "負け";
        EndUIs[0].GetComponent<Text>().color = Color.blue;
        NoTouchObj.SetActive(true);
        NoTouchObj.transform.GetChild(0).GetComponent<Text>().text = "";
        foreach (var ui in EndUIs)
        {
            ui.SetActive(true);
            ui.transform.SetAsLastSibling();
        }
    }
    public void Win()
    {
        NoTouchObj.SetActive(true);
        NoTouchObj.transform.GetChild(0).GetComponent<Text>().text = "";
        foreach (var ui in EndUIs)
        {
            ui.SetActive(true);
            ui.transform.SetAsLastSibling();
        }
    }

    private void QuestionString(object obj)
    {
        Canvas.GetComponent<GenerateQuestion>().Generate((string)obj);
    }

    private void TransStartUp()
    {
        Debug.Log("TransStartUp");
        for (int i = 0; i < Canvas.transform.childCount; i++)
        {
            Canvas.transform.GetChild(i).gameObject.SetActive(true);
            foreach(var ui in EndUIs)
            {
                if (ui.gameObject == Canvas.transform.GetChild(i).gameObject)
                {
                    Debug.Log("aaa");
                    Canvas.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        GeneratePlayerPlate();
        CheckOrder(_turn);
        NoTouchObj.transform.SetAsLastSibling();
        
    }

    private void CheckOrder(int order)
    {
        Debug.Log("CheckOrder");
        if (PhotonNetwork.player.NickName == PhotonPlayers[order-1].NickName)
        {
            //  Canvas.GetComponent<GenerateQuestion>().TextField.GetComponent<Text>().text = "";
            NoTouchObj.SetActive(false);
        }
        else NoTouchObj.SetActive(true);
    }

    public void OneMorePlay()
    {
        PhotonNetwork.JoinOrCreateRoom(_roomName, new RoomOptions{ MaxPlayers = (byte)_maxPlayerNum, }, new TypedLobby());
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnLobby()
    {
        //  PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "SelectOrder")
        {
            TransStartUp();
            state = "";
        }
    }
}
