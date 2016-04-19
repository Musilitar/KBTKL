using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public float interpolateVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    public Vector3 targetPos;

    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 positionWithoutZ = transform.position;
            positionWithoutZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - positionWithoutZ);

            interpolateVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpolateVelocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }
    }
}
