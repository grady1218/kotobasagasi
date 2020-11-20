using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour
{
    public GameObject Generator;
    public int CurrentPlayerNum { get; set; } = 0;
    public float WaitTime = 20f;
    private Text _myText;
    private bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        _myText = GetComponent<Text>();
        _myText.text = $"{CurrentPlayerNum} / {PhotonNetwork.room.MaxPlayers}";
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;

        transform.parent.transform.SetAsLastSibling();

        if (CurrentPlayerNum != 1) WaitTime -= Time.deltaTime;
        if(CurrentPlayerNum != PhotonNetwork.room.PlayerCount)
        {
            WaitTime += 5f;
            CurrentPlayerNum = PhotonNetwork.room.PlayerCount;
            _myText.text = $"{CurrentPlayerNum} / {PhotonNetwork.room.MaxPlayers}";
            if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers) WaitTime = 0f;
        }

        if(WaitTime <= 0)
        {
            transform.parent.GetComponent<Animator>().SetBool("IsWaitingEnded", true);
            stop = true;
        }
    }
}
