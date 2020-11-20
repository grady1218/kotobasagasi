using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetName : MonoBehaviour
{
    private bool _isAnimationStart = false;
    private bool _firstPlay = false;
    private Animator _myAnim;
    private float _count = 0f;

    [SerializeField] GameObject RoomInfoPrefub;
    GameObject RoomInfo;

    private void Start()
    {
        _myAnim = transform.parent.GetComponent<Animator>();
    }
    private void Update()
    {
        if (_isAnimationStart)
        {
            if (_myAnim.GetCurrentAnimatorStateInfo(0).IsName("JoinClickedAnim"))
            {
                _firstPlay = true;
            }

            if(_firstPlay)
            {
                if (_count > 1f) SceneManager.LoadScene("GameScene");
                _count += Time.deltaTime;
            }
        }    
    }

    public void PushButton()
    {
        var rooms = PhotonNetwork.GetRoomList();
        bool isExist = false;

        foreach(var room in rooms)
        {
            isExist = room.Name == GetComponent<Text>().text;
            break;
        }

        if (isExist)
        {
            PhotonNetwork.JoinRoom(GetComponent<Text>().text);
           _myAnim.SetBool("IsJoinClicked", true);
            transform.parent.SetAsLastSibling();
            _isAnimationStart = true;
        }
        else
        {
            GameObject.Find("NoTouchPanel").GetComponent<Image>().enabled = true;

            RoomInfo = Instantiate(RoomInfoPrefub);
            
            RoomInfo.name = "RoomInfoBoard";
            RoomInfo.transform.SetParent(GameObject.Find("Canvas").transform);
            RoomInfo.transform.parent.SetAsLastSibling();

            RoomInfo.transform.GetChild(2).GetComponent<RoomCreateButton>().RoomName = GetComponent<Text>().text;
            RoomInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
            RoomInfo.GetComponent<Animator>().SetTrigger("RoomInfoDispTrigger");
        }

    }
}
