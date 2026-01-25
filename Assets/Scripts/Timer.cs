using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private IEnumerator timer;
    public bool timerOver = false;

    private void Start()
    {
        ResetTimer();
        CallStartTimer();
    }

    public void CallStartTimer()
    {
        timer = StartTimer(30);
        StartCoroutine(timer);
        timerOver = false;
    }

    public void StopTimer()
    {
        if(timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
        ResetTimer();
    }

    private void ResetTimer()
    {
        timerText.text = "00";
        timerOver = true;
    }

    IEnumerator StartTimer(int remainingTime)
    {
        for (int i = remainingTime; i > 0; i--)
        {
            timerText.text = i.ToString("00");
            yield return new WaitForSeconds(1);
        }
        ResetTimer();
    }
}
