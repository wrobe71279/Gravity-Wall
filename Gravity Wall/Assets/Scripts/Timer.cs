using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //starting time
    [SerializeField] float startTime;

    //current Time
    public float currentTime;

    //whether or not the timer started?
    bool timerStarted;
    public Text timerText;
    void Start()
    {
        currentTime = startTime;
        timerText.text = currentTime.ToString();
        timerStarted = true;
    }

    void Update()
    {
        if (timerStarted)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                timerStarted = false;
                currentTime = 0;
            }

            timerText.text = currentTime.ToString("f0");
        }
    }
}
