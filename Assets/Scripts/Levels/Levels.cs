using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Levels
{
    public static int Current = Max;
    public static int Max
    {
        get => PlayerPrefs.GetInt("MaxLevel", 0);
        set => PlayerPrefs.SetInt("MaxLevel", value);
    }
}
