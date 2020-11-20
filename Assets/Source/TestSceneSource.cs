using UnityEngine;
using UnityEngine.UI;

public class TestSceneSource : Photon.PunBehaviour
{
    public GameObject[] AnimPlay;
    public GameObject[] Rooms;
    public GameObject DisplayRooms;
    [SerializeField] GameObject InputField;
    bool isLoaded = false;
    float _count = 0f;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.autoJoinLobby = true;
    }

    private void Update()
    {
        _count += Time.deltaTime;
        if (_count >= 0.5f)
        {
            _count -= 0.5f;
            isLoaded = false;
        }
        if (PhotonNetwork.insideLobby && !isLoaded)
        {
            for (int i = 0; i < Rooms.Length; i++)
            {
                Rooms[i].GetComponent<Text>().text = $"現在の参加者 : 0 / 4人";
            }

            var rooms = PhotonNetwork.GetRoomList();
            if (rooms.Length != 0)
            {
                Debug.Log(rooms[0].Name);
                foreach(var room in rooms)
                {
                    for (int j = 0; j < Rooms.Length; j++)
                    {
                        if (room.Name == $"Room{j + 1}")
                        {
                            Rooms[j].GetComponent<Text>().text = $"現在の参加者 : {room.PlayerCount} / {room.MaxPlayers}人";
                            if (room.PlayerCount == room.MaxPlayers) Rooms[j].transform.parent.GetChild(3).GetComponent<Button>().interactable = false;
                            else Rooms[j].transform.parent.GetChild(3).GetComponent<Button>().interactable = true;
                        }
                    }
                }
            }
            isLoaded = true;
        }
    }
    public override void OnJoinedRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        Debug.Log("ルームに参加しました");
        Debug.Log(PhotonNetwork.room.PlayerCount);
    }

    public override void OnJoinedLobby()
    {
        DisplayRooms.SetActive(true);

        InputField.GetComponent<Animator>().SetTrigger("PressedStartButton");
        foreach(var g in AnimPlay)
        {
            g.GetComponent<Animator>().SetBool("IsPushButton", true);
            if (g.CompareTag("Title")) g.GetComponent<NoiseyString>().IsDraw = true;
        }
    }

    public void JoinButton()
    {
        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
        if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated && InputField.GetComponent<InputField>().text != "")
        {
            PhotonNetwork.ConnectUsingSettings(null);
        }
        else if(PhotonNetwork.connectionStateDetailed == ClientState.Authenticated)
        {
            OnJoinedLobby();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 30), PhotonNetwork.connectionStateDetailed.ToString());
    }
}
