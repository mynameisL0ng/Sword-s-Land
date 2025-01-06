using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public RectTransform spinnerRotation;
    public float rotationSpeed;
    private void Start()
    {
        spinnerRotation = GetComponent<RectTransform>();
    }
    void Update()
    {
        spinnerRotation.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
