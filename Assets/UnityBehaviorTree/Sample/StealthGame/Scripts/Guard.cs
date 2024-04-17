using System;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Vector3 InitPosition;

    private void Start()
    {
        InitPosition = transform.position;
    }
}
