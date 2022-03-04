using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    public Transform gameOverCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        gameOverCanvas.gameObject.SetActive(true);
    }
}
