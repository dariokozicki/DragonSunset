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
        foreach (PlayerScore score in scoreboard) {
            print(score.name);
            displayScore(score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void displayScore(PlayerScore playerScore) {
        GameObject theScore = new GameObject(playerScore.name + "'s Score", typeof(RectTransform));
        theScore.transform.SetParent(scoreContainer.transform);
        RectTransform rectScore = theScore.GetComponent<RectTransform>();
        rectScore.offsetMin = new Vector2(0,0);
        rectScore.sizeDelta = new Vector2(scoreWidth, scoreHeight);
        Text name = theScore.AddComponent<Text>();
        name.text = playerScore.name;
        name.font = nameFont;
        name.fontSize = nameFontSize;
    }
}
