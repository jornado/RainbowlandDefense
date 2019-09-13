using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // static doesn't require instance
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int WavesSurvived;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        WavesSurvived = 0;
    }
}
