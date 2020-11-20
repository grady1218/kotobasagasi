using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateTextButtons : MonoBehaviour
{
    [SerializeField] GameObject prefub;
    [SerializeField] GameObject TextField;
    [SerializeField] GameObject EnterButton;
    [SerializeField] float margin = 10f;
    [SerializeField] float CenterSpace = 600f;

    //  ボタンの文字
    string[,] texts = { 
        {"あ","い","う","え","お"},
        {"か","き","く","け","こ"},
        {"さ","し","す","せ","そ"},
        {"た","ち","つ","て","と"},
        {"な","に","ぬ","ね","の"},
        {"は","ひ","ふ","へ","ほ"},
        {"ま","み","む","め","も"},
        {"や","　","ゆ","　","よ"},
        {"ら","り","る","れ","ろ"},
        {"わ","　","を","　","ん"},
        {"が","ぎ","ぐ","げ","ご"},
        {"ざ","じ","ず","ぜ","ぞ"},
        {"だ","ぢ","づ","で","ど"},
        {"ば","び","ぶ","べ","ぼ"},
        {"ぁ","ぃ","ぅ","ぇ","ぉ"},
        {"ゃ","ゅ","ょ","っ","ー"},
    };
    public void Genetate()
    {
        //  サイズを測る
        float space = margin;
        float buttonSpace = 1600 - CenterSpace - margin * 2;
        float buttonSize = buttonSpace / texts.GetLength(0);

        //  ボタンを生成
        for(int i = 0; i < texts.GetLength(0); i++)
        {
            if (i == texts.GetLength(0) / 2) space += CenterSpace;
            for(int j = 0; j < texts.GetLength(1); j++)
            {
                var g = Instantiate(prefub);
                g.transform.SetParent(transform,true);  //  親を設定
                g.GetComponent<ButtonSystem>().setSelectText(TextField);
                g.GetComponent<ButtonSystem>().setText(texts[i,j]);
                g.GetComponent<ButtonSystem>().EnterButton = EnterButton;
                if (texts[i, j] == "　") g.GetComponent<Button>().interactable = false;  //  空だったらボタンを無効にする
                g.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-space - buttonSize * i, -buttonSize * j, 0);
                g.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSize - 5, buttonSize - 5);
                g.transform.GetChild(0).GetComponent<Text>().fontSize = (int)buttonSize - 10;
            }
        }
    }
}
