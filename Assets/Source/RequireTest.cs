using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class RequireTest : MonoBehaviour
{
    [SerializeField] string PlayerKey = "PlayerKey";
    // Start is called before the first frame update
    void Start()
    {
        string defaultPlayerName = string.Empty;
        InputField inputField = GetComponent<InputField>();

        if (PlayerPrefs.HasKey(PlayerKey))
        {
            defaultPlayerName = PlayerPrefs.GetString(PlayerKey);
            inputField.text = defaultPlayerName;
        }

        PhotonNetwork.player.NickName = defaultPlayerName;
    }

    public void SetPlayerName(string value)
    {
        PhotonNetwork.player.NickName = value;

        PlayerPrefs.SetString(PlayerKey,value);
    }

}
