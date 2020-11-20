using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PushExit()
    {
        transform.parent.GetComponent<Animator>().SetTrigger("RoomInfoExitTrigger");
    }
}
