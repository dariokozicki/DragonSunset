using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PreviousPlayScore
{
    public static int durationMillis {get; set; }
    public static int timestamp {get; set; }
    public static string username {get; set;}
    public static long hiscore {get; set;}

    public static void reset(){
        durationMillis = 0;
        timestamp = 0;
        username = "";
        hiscore = 0;
    }
}
