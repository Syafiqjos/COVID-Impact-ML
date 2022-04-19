using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance Instance { get; set; }
    public PlayerController controller;

    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            EnemyInstance enemyInstance = collision.collider.GetComponent<EnemyInstance>();
            if (enemyInstance && !enemyInstance.GetIsInfected())
            {
                enemyInstance.Infect();
                enemyInstance.GetAgent().SetMoveCloser(true);
                GameplayManager.Instance?.AddScore(1);
                GameplayManager.Instance?.ShowScoreMessage("Infect Other!");
            }
        }
    }
}
