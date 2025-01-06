using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImageLoading : MonoBehaviour
{
    public Image imageLoading;
    public Sprite[] sourceImageLoading;
    void Start()
    {
        int imgIndex = Random.Range(0, sourceImageLoading.Length);
        imageLoading.sprite = sourceImageLoading[imgIndex];
    }
}
