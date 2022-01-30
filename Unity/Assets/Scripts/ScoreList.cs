using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreList : MonoBehaviour
{
    ContractInfo contractInfo;
    public UnityEngine.UI.VerticalLayoutGroup verticalLayoutGroup ;

    // Start is called before the first frame update
    public List<PlayerScore> scoreboard;
    async void Start()
    {
        contractInfo = GameObject.FindGameObjectWithTag("tagContractInfo").GetComponent<ContractInfo>();
        scoreboard = await contractInfo.getScoreboard();
        foreach (PlayerScore score in scoreboard) {
            print(score.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
