using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateQuestion : MonoBehaviour
{
    public GameObject basePrefub;
    public GameObject CurrentPushObj;
    public GameObject TextField;
    public QuestionText QT;
    GameObject[] questionObjects;
    float posX;
    float sizeX;
    public void Generate()
    {
        //  文字列をランダム選択
        QT = new QuestionText();
        QT.Select();

        //  ゲームオブジェクトを保存
        questionObjects = new GameObject[QT.selectedText.Length];

        //  サイズを図る
        sizeX = 100;
        posX = QT.selectedText.Length / 2f * -sizeX + sizeX / 2f;


        //  問題を生成
        for (int i = 0; i < QT.selectedText.Length; i++)
        {
            questionObjects[i] = Instantiate(basePrefub);
            questionObjects[i].transform.SetParent(transform, true);  //  親を設定
            questionObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(posX + sizeX * i, 200);
            questionObjects[i].transform.GetChild(0).GetComponent<Text>().text = QT.selectedText[i].ToString();
            questionObjects[i].transform.GetChild(0).GetComponent<Text>().enabled = false;  //  文字を見えなくする
            questionObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeX);
            questionObjects[i].transform.GetChild(0).GetComponent<Text>().fontSize = (int)sizeX;
        }

    }

    public void Generate(string text)
    {
        //  文字列をランダム選択
        QT = new QuestionText();
        QT.selectedText = text;

        //  ゲームオブジェクトを保存
        questionObjects = new GameObject[QT.selectedText.Length];

        //  サイズを図る
        sizeX = 100;
        posX = QT.selectedText.Length / 2f * -sizeX + sizeX / 2f;

        //  問題を生成
        for (int i = 0; i < QT.selectedText.Length; i++)
        {
            questionObjects[i] = Instantiate(basePrefub);
            questionObjects[i].transform.SetParent(transform, true);  //  親を設定
            questionObjects[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(posX + sizeX * i, 200);
            questionObjects[i].transform.GetChild(0).GetComponent<Text>().text = QT.selectedText[i].ToString();
            questionObjects[i].transform.GetChild(0).GetComponent<Text>().enabled = false;  //  文字を見えなくする
            questionObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, sizeX);
            questionObjects[i].transform.GetChild(0).GetComponent<Text>().fontSize = (int)sizeX;
        }

    }

    public void CheckString()
    {
        var text = TextField.GetComponent<Text>().text;

        for (int i = 0; i < QT.selectedText.Length; i++)
        {
            if (QT.selectedText[i].ToString() == text) {
                questionObjects[i].transform.GetChild(0).GetComponent<Text>().enabled = true;
            }
        }
        GameObject.Find("Enter").GetComponent<Button>().interactable = false;
        CurrentPushObj.GetComponent<Button>().interactable = false;
        TextField.GetComponent<Text>().text = "";

        if (IsFinished)
        {
            Debug.Log("くりあー");
            var ts = GameObject.Find("DataTransSystem").GetComponent<TransSystem>();
            ts.Win();
            PhotonNetwork.RaiseEvent((byte)TransSystem.Events.Clear, null, true, null);
        }
        else
        {
            var ts = GameObject.Find("DataTransSystem").GetComponent<TransSystem>();
            PhotonNetwork.RaiseEvent((byte)TransSystem.Events.Answer, text, true, null);
            ts.Answer(text);
            PhotonNetwork.RaiseEvent((byte)TransSystem.Events.Next, null, true, null);
            ts.Next();
        }
    }

    public bool IsCorrectAnswer(string text)
    {
        bool isCorrectAnswer = false;
        for (int i = 0; i < QT.selectedText.Length; i++)
        {
            if (QT.selectedText[i].ToString() == text)
            {
                isCorrectAnswer = true;
            }
        }
        return isCorrectAnswer;
    }
    public bool IsFinished  //  使わん
    {
        get
        {
            bool finish = true;
            foreach(var g in questionObjects)
            {
                if (!g.transform.GetChild(0).GetComponent<Text>().enabled) finish = false;
            }
            return finish;
        }
    }
}

