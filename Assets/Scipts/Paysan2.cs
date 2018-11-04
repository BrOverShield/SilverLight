using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paysan2 : MonoBehaviour {

    void Start()
    {
        this.gameObject.GetComponent<PaysanBehavior>().id = 2;
    }
}
