using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public Transform winCanvas;

    [SerializeField] private AudioSource winSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            winSFX.Play();
            Time.timeScale = 0;
            winCanvas.gameObject.SetActive(true);
        }           
    }
}
