using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DotSystem : MonoBehaviour
{

    public bool Waiting = true;

    [SerializeField] string BaseTest = "Waiting";
    [SerializeField] int DotNum = 3;
    [SerializeField] float DotTime = 1f;

    private Text _myText;
    private float _count = 0f;
    private string dots = "";
    private float _addTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _myText = transform.GetComponent<Text>();
        _addTime = DotTime / ( DotNum + 1 );
    }

    // Update is called once per frame
    void Update()
    {
        if (!Waiting) return;

        _count += Time.deltaTime;
        if(( dots.Length + 1 ) * _addTime <= _count)
        {
            dots += ".";
            _myText.text = BaseTest + dots;
        }
        if (_count >= DotTime)
        {
            dots = "";
            _count = 0f;
            _myText.text = BaseTest + dots;
        }
    }
}
