using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class TickBase
{
    public virtual void OnTick(Buffable statsOwner) {
        Debug.Log("No Tick Function added, but script applied on the basebuff");   
    }
}
