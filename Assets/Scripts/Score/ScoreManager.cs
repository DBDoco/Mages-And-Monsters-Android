using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    [SerializeField] int levelNumber;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore"+levelNumber, 0);
        scoreText.text = "X" + score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "X" + score.ToString();
        if (highscore < score)
            PlayerPrefs.SetInt("highscore"+levelNumber, score);
    }

    public void RemoveScore()
    {
        score = 0;
        scoreText.text = "X" + score.ToString();
    }
}
