using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
    public float time;
    float msec;
    float sec;
    float min;
    private void Start()
    {
        StartCoroutine("StopWatch");
    }
    IEnumerator StopWatch()
    {
        while (true)
        {
            time += Time.deltaTime;
            msec = (int)((time - (int)time) * 1000);
            sec = (int)(time % 60);
            min = (int)(time / 60 % 60);
            timer.text = DBManager.Time = string.Format("{0:00}:{1:00}:{2:000}", min, sec, msec);
            yield return null;
        }
    }
}
