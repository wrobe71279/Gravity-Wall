using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform gameOverCanvas;
    public Transform timerUI;

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameOverCanvas.gameObject.SetActive(true);
        timerUI.gameObject.SetActive(false);
    }
}
