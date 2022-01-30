using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ContractInfo : MonoBehaviour
{
    public static string chain = "ethereum";
    public static string network = "rinkeby";
    public static string METHOD_SCOREBOARD = "scoreboard";
    public static string METHOD_PLAYERSCORE = "hiScores";
    public static string METHOD_SETSCORE = "setScore";
    public static string METHOD_CURRENTPLAYER_SCORE = "playerToScore";
    public static string abi = "[{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"string\",\"name\":\"name\",\"type\":\"string\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"timestamp\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"durationMillis\",\"type\":\"uint256\"}],\"name\":\"NewHiScore\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"hiScores\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"name\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"timestamp\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"durationMillis\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"playerToScore\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"scoreSize\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"scoreboard\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"_value\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"_name\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"_durationMillis\",\"type\":\"uint256\"}],\"name\":\"setScore\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    public static string contract = "0x6AE4598d853d11C1984f520A6dD67AdEf2328888";
    // https://chainlist.org/
    public static string chainId = "4"; // rinkeby

    public static string signature;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    async Task<int[]> getScoreboardIds() {
        // array of arguments for contract
        string args = "[]";
        // connects to user's browser wallet to call a transaction
        string response = await EVM.Call(chain, network, contract, abi, METHOD_SCOREBOARD, args);
        // display response in game
        int[] ids = JsonHelper.FromJson<int>(response, true);
        print(string.Join(", ", ids));
        return ids;
    }

    async Task<PlayerScore> getPlayerScore(int id) {
        string args = "["+id+"]";
        string response = await EVM.Call(chain, network, contract, abi, METHOD_PLAYERSCORE, args);
        return JsonUtility.FromJson<PlayerScore>(response);
    }

    public async Task<List<PlayerScore>> getScoreboard() {
        int[] ids = await getScoreboardIds();
        List<PlayerScore> playerScores = new List<PlayerScore>();
        foreach (int id in ids) {
            playerScores.Add(await getPlayerScore(id));
        }
        playerScores.Sort((a, b) => b.value.CompareTo(a.value));
        return playerScores;
    }

    public async Task<int> setScore(long value, string name, int durationMillis) {
        long previousScore = await getCurrentPlayerScore();
        if (previousScore >= value){
            throw new System.Exception("User didn't make a personal High Score!" +
             "Previous: " + previousScore + ", Current: " + value);
        }
        // value in wei
        string eth = "0";
        string[] obj = { ""+value,""+ name,""+ durationMillis };
        string args = JsonConvert.SerializeObject(obj);
        // create data to interact with smart contract
        string data = await EVM.CreateContractData(abi, METHOD_SETSCORE, args);
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, contract, eth, data, gasLimit, gasPrice);
        print(response);
        return 1;
    }

    public async Task<int> getCurrentPlayerScoreId() {
        string account = PlayerPrefs.GetString("Account");
        if (string.IsNullOrEmpty(account)) { throw new System.Exception("User didn't load their wallet"); }
        string args = "[\""+account+"\"]";
        string response = await EVM.Call(chain, network, contract, abi, METHOD_CURRENTPLAYER_SCORE, args);
        return int.Parse(response);
    }

    public async Task<long> getCurrentPlayerScore() {
        int scoreId = await getCurrentPlayerScoreId();
        PlayerScore score = await getPlayerScore(scoreId);
        return score.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async public Task<string> OnSignMessage()
    {
        string response = await Web3Wallet.Sign("hello");
        print(response);
        return response;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json, bool needsFormat)
        {

            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(
                needsFormat ? format(json) : json
            );
            return wrapper.Items;
        }

        public static string format(string json) {
            return "{\"Items\":"+json+"}";
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
