using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStepFade : MonoBehaviour
{
    public int PasDansLaNeigeTimer = 5;
    public void PasDansLaNeigeTimerDown()
    {

        PasDansLaNeigeTimer--;
        Color NewColor = new Color(1, 1, 1, (float)(PasDansLaNeigeTimer / 5f));
        //GetComponentInChildren<SpriteRenderer>().color = NewColor;
        GetComponentInChildren<SpriteRenderer>().material.color = NewColor;

        if (PasDansLaNeigeTimer<=0)
        {
            Destroy(this.gameObject);
        }
    }

}
