using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public GameplayManager gameplayManager;
    public Text text;

    private void Update()
    {
        if (gameplayManager)
        {
            text.text = "Score: " + gameplayManager.GetScore();
        }
    }
}
