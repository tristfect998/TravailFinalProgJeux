using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text TimerText;
    float TimerTime = 0f;
    bool gameEnded = false;
    // Use this for initialization
    void Start()
    {
        TimerText = gameObject.GetComponent<Text>();
        ApplyNewTime();
    }

    // Update is called once per frame
    void Update()
    {
        TimerTime += Time.deltaTime;
        ApplyNewTime();
    }

    void ApplyNewTime()
    {
        if(!gameEnded)
        {
            string FormatedTime = "";
            float minutes = Mathf.Floor(TimerTime / 60);
            float secondes = (TimerTime % 60);
            if (minutes > 60)
            {
                float hours = Mathf.Floor(minutes / 60);
                if (hours < 10)
                {
                    FormatedTime += "0" + hours + ":";
                }
                else
                {
                    FormatedTime += hours + ":";
                }
            }
            else if (minutes < 10)
            {
                FormatedTime += "0" + minutes + ":";
            }
            else
            {
                FormatedTime += minutes + ":";
            }

            if (secondes < 10)
            {
                FormatedTime += "0" + Mathf.Floor(secondes);
            }
            else
            {
                FormatedTime += Mathf.Floor(secondes);
            }
            TimerText.text = FormatedTime;
        }
    }

    void EndTimer()
    {
        gameEnded = true;
        TimerText.enabled = false;
    }

    public float GetTimerTime()
    {
        return TimerTime;
    }
}
