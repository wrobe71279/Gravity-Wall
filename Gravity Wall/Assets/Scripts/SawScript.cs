using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    public Transform gameOverCanvas;
    public Transform timerUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameOverCanvas.gameObject.SetActive(true);
        timerUI.gameObject.SetActive(false);
    }
}
