using System;

/// <summary>
/// 文字列をランダムに選択するクラス。
/// .QuestionTextsで文字列を取れますよ。
/// </summary>
public class QuestionText
{
    public string[] QuestionTexts = { 
        "あるみかん",
        "じゃがいも",
        "うぃんなー"
    };

    public string selectedText { get; set; }

    public void Select()
    {
        Random r = new Random();
        selectedText = QuestionTexts[r.Next(QuestionTexts.Length)];
    }
}
