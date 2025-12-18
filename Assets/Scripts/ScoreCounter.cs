using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    public int scoreAmount = 4;
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        string text = "Score: " + score.ToString();
        scoreText.text = text;
    }

    public void UpdateScore()
    {
        score += scoreAmount;
        Debug.Log("Score updated.");
    }
}
