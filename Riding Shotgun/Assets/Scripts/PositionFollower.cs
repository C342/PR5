using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform TargetTransform;
    public Vector3 PositionOffset = new Vector3(0.3f, -0.3f, 0.5f);
    public float FollowSmoothness = 10f;

    [Header("Rotation Settings")]
    public float RotationSmoothness = 15f;

    private void LateUpdate()
    {
        if (TargetTransform == null)
            return;

        Vector3 desiredPosition = TargetTransform.TransformPoint(PositionOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * FollowSmoothness);

        Quaternion desiredRotation = Quaternion.Lerp(transform.rotation, TargetTransform.rotation, Time.deltaTime * RotationSmoothness);
        transform.rotation = desiredRotation;
    }
}