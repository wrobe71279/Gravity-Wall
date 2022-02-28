using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform gameOverCanvas;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 0;
        gameOverCanvas.gameObject.SetActive(true);
    }
}
