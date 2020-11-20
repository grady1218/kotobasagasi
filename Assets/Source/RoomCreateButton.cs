using UnityEngine;
using UnityEngine.UI;

public class RoomCreateButton : MonoBehaviour
{
    [SerializeField] GameObject PlayerCount;
    public bool IsPushed { get; set; } = false;
    public string RoomName { get; set; }
    public int PlayerNumber { get; set; }
    // Start is called before the first frame update
    public void PushCreateButton()
    {
        PlayerNumber = int.Parse(PlayerCount.GetComponent<InputField>().text);

        if (PlayerNumber > 1 && PlayerNumber <= 4 && !IsPushed)
        {
            IsPushed = true;
            transform.parent.GetComponent<Animator>().SetTrigger("RoomInfoExitTrigger");
        }
    }
}
