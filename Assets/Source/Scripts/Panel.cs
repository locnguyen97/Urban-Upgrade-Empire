using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public void Show()
    {
        foreach (Transform tr in transform)
        {
            Destroy(tr.gameObject);
        }
        
    }
}
