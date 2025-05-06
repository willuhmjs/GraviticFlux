using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MLAgent : Agent
{
    private Rigidbody2D rb;
    private Vector3 startPos;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        // Reset the agent's position and velocity
        transform.position = startPos;
        rb.velocity = Vector2.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add the agent's position and velocity to the observations
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rb.velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get the continuous actions
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        // Apply the actions to the agent's Rigidbody2D
        rb.AddForce(new Vector2(moveX, moveY));
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
