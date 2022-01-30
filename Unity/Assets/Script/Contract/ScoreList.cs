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
        //Adjust size of the container to match items
        RectTransform boardRt = scoreContainer.GetComponent<RectTransform>();
        boardRt.sizeDelta = new Vector2(
            boardRt.sizeDelta.x, 
            Mathf.Max(((RectTransform)transform).rect.height, scoreboard.Count * scoreHeight * 2f)
        );
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
        displayText(theScore, playerScore.name, nameFontSize ,"name", index, 0);
        displayText(theScore, "POINTS: " + playerScore.value, nameFontSize, "points", index, 1);
        displayText(theScore, "TIME: " + playerScore.durationMillis + " ms", nameFontSize, "time", index, 2);
        displayText(theScore, "" + playerScore.timestamp, nameFontSize, "date", index, 3);
    }

    void displayText(GameObject theScore, string text, int fontSize, string objName, int index, int paramIndex) { 
        int textWidth = paramIndex == 0 ? 600 : 400;
        int leftOffset = paramIndex == 0 ? 0 : 100;
        GameObject obj = new GameObject(objName, typeof(RectTransform));
        obj.transform.SetParent(theScore.transform);
        RectTransform rectObj = obj.GetComponent<RectTransform>();
        rectObj.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, textWidth*paramIndex+leftOffset, textWidth);
        rectObj.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, scoreHeight*index, scoreHeight);
        Text name = obj.AddComponent<Text>();
        name.text = text;
        name.font = nameFont;
        name.fontSize = paramIndex == 0 ? fontSize : 40;
    }

}
