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
    public float nameWidth;
    public float otherTextWidth;
    public Color nameColor;

    async void Start()
    {
        nameColor = new Color(62f/255.0f,21f/255.0f,21f/255.0f);
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
            Mathf.Max(((RectTransform)transform).rect.height, scoreboard.Count * scoreHeight)
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
        displayText(theScore, formatDate(playerScore.timestamp), nameFontSize, "date", index, 3);
    }

    void displayText(GameObject theScore, string text, int fontSize, string objName, int index, int paramIndex) { 
        GameObject obj = new GameObject(objName, typeof(RectTransform));
        obj.transform.SetParent(theScore.transform);
        RectTransform rectObj = obj.GetComponent<RectTransform>();
        rectObj.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 
            nameWidth*Mathf.Min(paramIndex, 1) + otherTextWidth*Mathf.Max(paramIndex-1, 0), 
            paramIndex == 0 || paramIndex == 3 ? nameWidth : otherTextWidth 
        );
        rectObj.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, scoreHeight);
        Text name = obj.AddComponent<Text>();
        name.text = text;
        name.font = nameFont;
        name.fontSize = paramIndex == 0 ? fontSize : 50;
        if(paramIndex == 0) {
            name.color = nameColor;
        }
    }

    public static System.DateTime UnixTimeStampToDateTime( double unixTimeStamp )
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new System.DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
        return dtDateTime;
    }

    public string formatDate(int timestamp) {
        System.DateTime dt = UnixTimeStampToDateTime(timestamp);
        return dt.ToString();
    }
}
