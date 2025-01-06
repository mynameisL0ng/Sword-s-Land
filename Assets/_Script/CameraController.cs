using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 posOffset; // need to set z = -10

    public Vector2 xLimit;
    public Vector2 yLimit;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        CameraDirectionX();
        Vector3 targetPosition = target.position + posOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity , smoothTime);
    }
    private void CameraDirectionX()
    {
        switch (InitPlayer.player.direction)
        {
            case Character.Direction.LEFT:
                posOffset.x = -1f;
                break;
            case Character.Direction.RIGHT:
                posOffset.x = 1f;
                break;
        }
    }
}
