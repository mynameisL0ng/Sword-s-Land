using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;
    public PlayerController playerController;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 posOffset; // need to set z = -10

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    private void LateUpdate()
    {
        CameraDirectionX();
        Vector3 targetPosition = target.position + posOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity , smoothTime);
    }
    private void CameraDirectionX()
    {
        switch (playerController.player.direction)
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
