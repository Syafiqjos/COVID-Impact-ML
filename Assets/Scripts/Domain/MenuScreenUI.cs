using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenUI : MonoBehaviour
{
    public GameplayManager gameplayManager;
    public GameObject menuScreen;

    private void Awake()
    {
        menuScreen.SetActive(false);
    }

    void Update()
    {
        if (gameplayManager)
        {
            if (gameplayManager.IsGameFinished())
            {
                menuScreen.SetActive(true);
            }
        }
    }
}
