using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector2 startPosition;
    [SerializeField] GameObject resetPoint;
    [SerializeField] float moveRange;
    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        if(Vector2.Distance(transform.position, resetPoint.transform.position) > moveRange)
        {
            transform.position = startPosition;
        }
    }
}
