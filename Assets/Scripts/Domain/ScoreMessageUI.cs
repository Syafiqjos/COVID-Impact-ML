using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreMessageUI : MonoBehaviour
{
    public GameplayManager gameplayManager;
    public Text text;

    public float normalFontScale = 1.0f;
    public float growFontScale = 1.5f;

    private float disappearTimer = 0;

    private void Update()
    {
        Color c;
        if (gameplayManager)
        {
            string latestScoreMessage = gameplayManager.GetScoreMessage();

            if (latestScoreMessage != "")
            {

                text.text = latestScoreMessage;
                text.rectTransform.localScale = Vector3.one * growFontScale;

                c = text.color; c.a = 1.0f; text.color = c;
                disappearTimer = 2.0f;
            }
        }

        if (disappearTimer <= 0)
        {
            c = text.color; c.a = Mathf.Lerp(c.a, 0.0f, 0.1f); text.color = c;
        } else
        {
            disappearTimer -= Time.deltaTime;
        }

        text.rectTransform.localScale = Vector3.Lerp(text.rectTransform.localScale, Vector3.one * normalFontScale, 0.05f);
    }
}