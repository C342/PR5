using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MoveToTargetAgent : Agent
{


    [SerializeField] private Transform env;
    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;



    public override void OnEpisodeBegin()
    {
        transform.localPosition += new Vector3(Random.Range(-3.5f, -1.5f), Random.Range(-3.5f, 3.5f));
        target.localPosition += new Vector3(Random.Range(1.5f, 3.5f), Random.Range(-3.5f, 3.5f));

        env.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        transform.rotation = Quaternion.identity;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log($"[CollectObservations] AgentPos; {transform.localPosition}, TargetPos; {target.localPosition}");

        sensor.AddObservation((Vector2)transform.position);
        sensor.AddObservation((Vector2)target.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        Debug.Log($"Action recieved; moveX = {moveX}, moveY = {moveY}");

        float movementSpeed = 5f;

        transform.position += new Vector3(moveX, moveY) * Time.deltaTime * movementSpeed;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target target))
        {
            AddReward(10f);
            backgroundSpriteRenderer.color = Color.green;
            EndEpisode();
        }
        else if (collision.TryGetComponent(out Wall wall))
        {
            AddReward(-2f);
            backgroundSpriteRenderer.color = Color.red;
            EndEpisode();
        }
    }
}