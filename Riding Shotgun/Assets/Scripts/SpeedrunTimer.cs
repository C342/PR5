using TMPro;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    public TextMeshProUGUI Timertext;

    private float elapsedTime = 0f;
    private bool isRunning = true;

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            int hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int miliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

            if (Timertext != null)
            {
                Timertext.text = $"{minutes:00}:{seconds:00}:{miliseconds:000}";
            }
        }
    }

    public void StartTimer() => isRunning = true;
    public void StopTimer() => isRunning = false;
    public void ResetTimer()
    {
        elapsedTime = 0f;
        if (Timertext != null)
        {
            Timertext.text = "00:00:000";
        }
    }
}