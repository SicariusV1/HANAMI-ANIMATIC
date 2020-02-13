using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    [SerializeField, Range(0, 360)] private float rotationSpeed = default;
    [SerializeField] private Transform origin;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.RotateAround(origin.position, Vector3.up, Time.deltaTime * rotationSpeed);
        transform.LookAt(origin);
    }
}
