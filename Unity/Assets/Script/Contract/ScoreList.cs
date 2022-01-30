using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreList : MonoBehaviour
{
    ContractInfo contractInfo;
    public GameObject scoreContainer;
    // Start is called before the first frame update
    public List<PlayerScore> scoreboard;
    public Font nameFont;
    public int nameFontSize;
    public float scoreHeight;
    public float scoreWidth;

    async void Start()
    {
        contractInfo = GameObject.FindGameObjectWithTag("tagContractInfo").GetComponent<ContractInfo>();
        scoreboard = await contractInfo.getScoreboard();
        for (int i = 0; i < scoreboard.Count; i++) {
            PlayerScore score = scoreboard[i];
            print(score.name);
            displayScore(score, i);
        }
        RectTransform boardRt = scoreContainer.GetComponent<RectTransform>();
        boardRt.sizeDelta = new Vector2(boardRt.sizeDelta.x, scoreboard.Count * scoreHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void displayScore(PlayerScore playerScore, int index) {
        GameObject theScore = new GameObject(playerScore.name + "'s Score", typeof(RectTransform));
        theScore.transform.SetParent(scoreContainer.transform);
        RectTransform rectScore = theScore.GetComponent<RectTransform>();
        rectScore.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 20, scoreWidth);
        rectScore.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, scoreHeight*index, scoreHeight);
        Text name = theScore.AddComponent<Text>();
        name.text = playerScore.name + "\n" + "Points: " + playerScore.value + "\n" + playerScore.timestamp + "\n" + playerScore.durationMillis;
        name.font = nameFont;
        name.fontSize = nameFontSize;
    }
}
