using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Transform mainMenu;
    public Transform howToPlay;


    public void BackButton()
    {
        howToPlay.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void Controls()
    {
        howToPlay.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }
}
