using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentController : Agent
{
    private Rigidbody2D rigidbody2D;
    public MovementController controller;
    public RaycastCatcher distanceMeasurer;

    public Transform waypointTarget;
    public float minimumTargetDistance = 1.0f;
    private Vector3 initialPosition;
    private Vector3 previousPosition;

    [SerializeField] private float discretePosRound = 0.2f;

    [SerializeField] private float hittingWallReward = -1000f;
    [SerializeField] private float nearTargetReward = 1000f;
    [SerializeField] private float approachingTargetReward = 0.02f;
    [SerializeField] private float deapproachingTargetReward = -10.0f;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.localPosition;

        if (waypointTarget == null)
        {
            waypointTarget = transform;
        }
    }

    private void Update()
    {
        AddRewardOnApproachingWaypointTarget();
        AddRewardOnNearWaypointTarget();
        previousPosition = GetCurrentPosition();
    }

    public void SetWaypointTarget(Transform newTarget) {
        waypointTarget = newTarget;
    }

    private void AddRewardOnApproachingWaypointTarget()
    {
        float targetToCurrentDistance = Vector2.Distance(GetWaypointTargetPosition(), GetCurrentPosition());
        float targetToPreviousDistance = Vector2.Distance(GetWaypointTargetPosition(), previousPosition);

        if (targetToCurrentDistance < targetToPreviousDistance) {
            Debug.Log("Approaching Target Reward");
            AddReward(approachingTargetReward);
        } else if (targetToCurrentDistance > targetToPreviousDistance) {
            Debug.Log("Deapproaching Target Reward");
            AddReward(deapproachingTargetReward);
        }
    }

    private void AddRewardOnNearWaypointTarget() {
        if (Vector2.Distance(GetCurrentPosition(), GetWaypointTargetPosition())
            <= minimumTargetDistance
        ) {
            Debug.Log("Near Target Reward");
            AddReward(nearTargetReward);
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("Begin Episode");

        SetCurrentPosition(initialPosition);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector2 currentPos = GetCurrentPosition();
        Vector2 targetPos = GetWaypointTargetPosition();

        sensor.AddObservation(RoundDiscrete(currentPos.x));
        sensor.AddObservation(RoundDiscrete(currentPos.y));
        sensor.AddObservation(RoundDiscrete(targetPos.x));
        sensor.AddObservation(RoundDiscrete(targetPos.y));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        /*
        float mx = actions.ContinuousActions[0];
        float my = actions.ContinuousActions[1];

        if (mx > 0.5f) mx = 1;
        else if (mx < -0.5f) mx = -1;
        else mx = 0;

        if (my > 0.5f) my = 1;
        else if (my < -0.5f) my = -1;
        else my = 0;

        // controller.Move(new Vector2(mx, my));
        rigidbody2D.MovePosition(rigidbody2D.position + new Vector2(mx * discretePosRound, my * discretePosRound));
        */

        int ac0 = actions.DiscreteActions[0]; // 0, 1, 2
        int ac1 = actions.DiscreteActions[1]; // 0, 1, 2

        int mx = ac0 - 1; // -1, 0, 1
        int my = ac1 - 1; // -1, 0, 1

        controller.Move(new Vector2(mx, my));
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        /*
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        */

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        discreteActions[0] = (int)Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        discreteActions[1] = (int)Input.GetAxisRaw("Vertical"); // -1, 0, 1

        discreteActions[0] += 1; // 0, 1, 2
        discreteActions[1] += 1; // 0, 1, 2
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall") {
            Debug.Log("Hitting Wall Reward");
            AddReward(hittingWallReward);
            EndEpisode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Wall")
        {
            Debug.Log("Hitting Wall Reward");
            AddReward(hittingWallReward);
            EndEpisode();
        }
    }

    private Vector3 GetCurrentPosition()
    {
        return transform.localPosition;
    }

    private void SetCurrentPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    private Vector3 GetWaypointTargetPosition()
    {
        return waypointTarget.localPosition;
    }

    private float RoundDiscrete(float x){
        return Mathf.Round(x / discretePosRound) * discretePosRound;
    }
}
