// SPDX-License-Identifier: MIT
// solhint-disable-next-line
pragma solidity ^0.8.4;

contract DragonGame {
    event NewHiScore(address player, uint256 value, string name, uint256 timestamp, uint256 durationMillis);

    mapping (uint256 => Score) public hiScores;
    mapping (address => uint256) public playerToScore;
    uint256 public scoreSize;

    struct Score {
        string name;
        uint256 value;
        uint256 timestamp;
        uint256 durationMillis;
    }

    function setScore(uint256 _value, string memory _name, uint256 _durationMillis) external returns (uint256) {
        uint scoreId = playerToScore[msg.sender] != 0 ? playerToScore[msg.sender] : (scoreSize + 1);
        Score storage theScore = hiScores[scoreId];
        bool isNewScore = theScore.value == 0;  
        require(theScore.value < _value, "The score is not higher than previous ones for that user.");
        playerToScore[msg.sender] = scoreId;
        theScore.value = _value;
        theScore.timestamp = block.timestamp;
        theScore.durationMillis = _durationMillis;
        theScore.name = _name;
        if (isNewScore) scoreSize++;
        emit NewHiScore(msg.sender, theScore.value, theScore.name, theScore.timestamp, theScore.durationMillis);
        return scoreId;
    }

    function scoreboard() external view returns (uint256[] memory) {
        uint256[] memory scores = new uint256[](scoreSize);
        for (uint256 i = 0; i < scoreSize; i++) { 
            scores[i] = i + 1;
        }
        return scores;
    }
}
