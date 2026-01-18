using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnTempText;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI score;

    private double scoreDouble;
    private int finalScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time.text = "Time: " + Timer.GetTime();
        turnTempText.text = "Turns: " + GameManager.Instance.GetTurnCount();

        scoreDouble = ((100 - (Timer.GetTime()) * 0.3) + ((100 - GameManager.Instance.GetTurnCount()) * 0.7)) + 1000;
        finalScore = (int)scoreDouble;

        score.text = "Score: " + finalScore;
    }


}
