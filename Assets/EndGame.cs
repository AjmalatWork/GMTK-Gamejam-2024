using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TMP_Text finishText;
    [SerializeField] private string finishMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayFinishText();
        }
    }

    private void DisplayFinishText()
    {
        finishText.text = finishMessage;
        finishText.gameObject.SetActive(true);
    }
}
