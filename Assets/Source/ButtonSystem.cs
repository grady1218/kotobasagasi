using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSystem : MonoBehaviour
{
    private GameObject SelectedText;
    private Text childText;
    public GameObject EnterButton { get; set; }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSelectText(GameObject g)
    {
        SelectedText = g;
    }

    public void setText(string text)
    {
        childText = transform.GetChild(0).GetComponent<Text>();
        childText.text = text;
    }

    public void PressedButton()
    {
        EnterButton.GetComponent<Button>().interactable = true;
        SelectedText.GetComponent<Text>().text = childText.text;
        GameObject.Find("Canvas").GetComponent<GenerateQuestion>().CurrentPushObj = childText.transform.parent.gameObject;
    }
}
