using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TileUpdater : MonoBehaviour
{
    TileInfo myInfo;
    
    
    void Start()
    {

    }


    void Update()
    {

    }
    public void UpdateMe(TileInfo ti)
    {
        this.transform.position = new Vector3(ti.PositionX, 0, ti.PositionY);
        
        myInfo = ti;
        ti.myGo = this.gameObject;
    }


}
