using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmScript : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("BGM");
        if (obj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
                
    }

}
