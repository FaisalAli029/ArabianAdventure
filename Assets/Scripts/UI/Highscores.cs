using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Highscores : MonoBehaviour
{
    public TextMeshProUGUI desertHighScore;
    public TextMeshProUGUI trackHighScore;
    public TextMeshProUGUI marketHighScore;
    public TextMeshProUGUI villageHighScore;

    // Start is called before the first frame update
    void Start()
    {
        desertHighScore.text = gameObject.GetComponent<ScoreController>().GetHighestScoreWithString("Level 1").ToString();
        trackHighScore.text = gameObject.GetComponent<ScoreController>().GetHighestScoreWithString("Level 2").ToString();
        marketHighScore.text = gameObject.GetComponent<ScoreController>().GetHighestScoreWithString("Level 3").ToString();
        villageHighScore.text = gameObject.GetComponent<ScoreController>().GetHighestScoreWithString("Level 4").ToString();
    }

}
