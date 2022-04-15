using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    PlayerInstance player;
    [SerializeField] MoveAwayAgentDiscrete agent;

    public float minimumDieDistance = 20;

    [SerializeField] private bool isInfected = false;

    [Header("Config")]
    [SerializeField] SpriteRenderer graphics;
    [SerializeField] Color healthyColor;
    [SerializeField] Color infectedColor;

    private void Start()
    {
        player = PlayerInstance.Instance;
    }

    private void Update()
    {
        if (player && !isInfected)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > minimumDieDistance)
            {
                Destroy(gameObject);
            }
        }

        UpdateColor();
    }

    public void Infect() {
        isInfected = true;
    }

    public bool GetIsInfected()
    {
        return isInfected;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            EnemyInstance enemyInstance = collision.collider.GetComponent<EnemyInstance>();
            if (enemyInstance)
            {
                if (isInfected && !enemyInstance.GetIsInfected())
                {
                    enemyInstance.Infect();
                    enemyInstance.GetAgent().SetMoveCloser(true);
                    GameplayManager.Instance.AddScore(5);
                    GameplayManager.Instance.ShowScoreMessage("Infected by Proxy!!");
                }
            }
        }
    }

    private void UpdateColor()
    {
        if (isInfected)
        {
            graphics.color = infectedColor;
        }
        else
        {
            graphics.color = healthyColor;
        }
    }

    public MoveAwayAgentDiscrete GetAgent()
    {
        return agent;
    }
}
