using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeout : MonoBehaviour
{
    public int t=5;
    public void DoTurn()
    {
        t--;
        if(t<=0)
        {
            Destroy(this.gameObject);
        }
    }
	
}
