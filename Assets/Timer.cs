using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float elapsedTime;
    private bool isTiming;

    private void Start()
    {
        ResetTimer();
        StartTimer();
    }

    private void Update()
    {
        if (isTiming)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetTimer();
                StartTimer();
            }
        }
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
