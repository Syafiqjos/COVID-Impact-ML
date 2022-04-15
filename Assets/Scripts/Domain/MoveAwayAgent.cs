using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MoveAwayAgent : Agent
{
    private Rigidbody2D rigidbody2D;

    public float maxSearchRadius = 10.0f;
    private Vector3 initialPosition;
    private Vector3 previousPosition;

    [SerializeField] private float currentForwardAngle = 0;
    private float previousForwardAngle = 0;

    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private bool enableSmoothTurn = true;
    [SerializeField] private float changingAngleRewardMaxFactor = 0.1f;

    [Header("Heuristic Only")]
    [SerializeField] private float changingAngleSpeed = 0.1f;
    [SerializeField] private bool debugObservation = false;
    [SerializeField] private bool debugAction = false;
    [SerializeField] private bool debugReward = false;

    [Header("Parameter")]
    [SerializeField] private float changingAngleRewardMinFactor = 0.1f;
    [SerializeField] private float changingAngleReward = 0.001f;
    [SerializeField] private float movingCloserToTargetReward = -0.01f;
    [SerializeField] private float movingAwayFromTargetReward = 0.02f;

    private GameObject nearestObject = null;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        AddRewardOnMovingAwayFromTarget();
        AddRewardOnChangingAngle();

        previousPosition = transform.position;
        previousForwardAngle = currentForwardAngle;

        GoForward();
    }

    private void GoForward() {
        Vector2 cp = transform.position;
        float x = Mathf.Cos(currentForwardAngle);
        float y = Mathf.Sin(currentForwardAngle);

        cp += new Vector2(x, y) * Time.deltaTime * moveSpeed;

        rigidbody2D.MovePosition(cp);
    }

    private void AddRewardOnChangingAngle()
    {
        float delta = Mathf.Abs(previousForwardAngle - currentForwardAngle);
        if (delta >= changingAngleRewardMinFactor) {
            AddReward(changingAngleReward);
        }
    }

    private void AddRewardOnMovingAwayFromTarget()
    {
        // GameObject nearestObject = GetNearestObject();
        // nearestObject = GetNearestObject();

        if (nearestObject)
        {
            float targetToCurrentDistance = Vector2.Distance(nearestObject.transform.position, transform.position);
            float targetToPreviousDistance = Vector2.Distance(nearestObject.transform.position, previousPosition);

            if (targetToCurrentDistance > targetToPreviousDistance)
            {
                if (debugReward)
                {
                    Debug.Log("Moving Away from Target Reward");
                }
                AddReward(movingAwayFromTargetReward);
            }
            else if (targetToCurrentDistance < targetToPreviousDistance) {
                if (debugReward)
                {
                    Debug.Log("Moving Closer to Target Reward");
                }
                AddReward(movingCloserToTargetReward);
            }
        }
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Begin Episode");

        transform.localPosition = initialPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector2 normalizedRelativePos = Vector2.zero;

        // GameObject nearestObject = GetNearestObject();
        nearestObject = GetNearestObject();
        if (nearestObject)
        {
            // Debug.Log(nearestObject);
            Vector2 relativePos = transform.position - nearestObject.transform.position;
            normalizedRelativePos = relativePos.normalized;
        }

        if (debugObservation)
        {
            Debug.Log(normalizedRelativePos);
        }

        sensor.AddObservation(normalizedRelativePos.x);
        sensor.AddObservation(normalizedRelativePos.y);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float ac0 = actions.ContinuousActions[0];

        // if tidak ada object di sekitar maka
        if (nearestObject == null)
        {
            ac0 = Random.Range(-1f, 1f);
        }

        float normalizedAc0 = ((1.0f + ac0) % 1.0f) * 2 * Mathf.PI;

        if (enableSmoothTurn)
        {
            currentForwardAngle = Mathf.Clamp(normalizedAc0 % (2 * Mathf.PI), currentForwardAngle - changingAngleRewardMaxFactor, currentForwardAngle + changingAngleRewardMaxFactor);
        }
        else
        {
            currentForwardAngle = normalizedAc0 % (2 * Mathf.PI);
        }


        if (debugAction)
        {
            Debug.Log(currentForwardAngle);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        float x = Input.GetAxisRaw("Horizontal");

        float angle = currentForwardAngle / (2 * Mathf.PI);

        if (x > 0)
        {
            angle -= changingAngleSpeed;
        } else if (x < 0)
        {
            angle += changingAngleSpeed;
        }

        angle = (angle + 1.0f) % 1.0f;

        continuousActions[0] = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

    }

    private GameObject GetNearestObject() {
        LayerMask layerMask = LayerMask.GetMask("Raycastable");

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, maxSearchRadius, layerMask);

        float nearestDistance = float.MaxValue;
        GameObject nearestObject = null;
        foreach (Collider2D col in objects) {
            if (col.gameObject != gameObject) {
                float objectDistance = Vector2.Distance(transform.position, col.transform.position);
                if (objectDistance < nearestDistance) {
                    nearestObject = col.gameObject;
                    nearestDistance = objectDistance;
                }
            }
        }

        return nearestObject;
    }
}
