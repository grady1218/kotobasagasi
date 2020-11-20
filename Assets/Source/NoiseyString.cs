using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseyString : MonoBehaviour
{

    public bool IsDraw = false;
    public float time = 1f;
    public string drawString;

    private float _count = 0f;
    private float _drawTime;
    private int _stringCount = 0;
    private Text _text;
    private string _baseText;
    // Start is called before the first frame update
    void Start()
    {
        _drawTime = time / drawString.Length;
        _text = GetComponent<Text>();
        _baseText = _text.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDraw)
        {
            if (_baseText == _text.text) _text.text = "";
            _count += Time.deltaTime;
            if(_drawTime <= _count)
            {
                _text.text += drawString[_stringCount];
                _stringCount++;
                _count -= _drawTime;
            }

            if (_stringCount == drawString.Length) IsDraw = false;
        }
    }
}
