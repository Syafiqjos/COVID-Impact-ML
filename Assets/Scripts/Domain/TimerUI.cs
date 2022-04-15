using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public GameplayManager gameplayManager;
    public Text text;

    private void Update()
    {
        if (gameplayManager)
        {
            text.text = "Time: " + (int) Mathf.Clamp(gameplayManager.GetTimeLeft(), 0, float.MaxValue);
        }
    }
}
