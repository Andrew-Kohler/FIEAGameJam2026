using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnTempText;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private GameObject unstable;

    private double scoreDouble;
    private int finalScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.Instance.challengeMode)
        {
            unstable.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time.text = "Time: " + Timer.GetTime();
        turnTempText.text = "Turns: " + GameManager.Instance.GetTurnCount();

        scoreDouble = ((850/GameManager.Instance.GetTurnCount()) * (400/Timer.GetTime()) * (100));
        finalScore = (int)scoreDouble;

        score.text = "Score: " + finalScore;
    }


}
