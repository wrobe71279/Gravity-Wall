using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public Transform winCanvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        winCanvas.gameObject.SetActive(true);
    }
}
