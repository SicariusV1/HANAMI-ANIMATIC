using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Key
{
    public float timecode;
    public Vector3 position;
    public Quaternion rotation;

    public Key(float _time, Transform _transform)
    {
        timecode = _time;
        position = _transform.position;
        rotation = _transform.rotation;
    }
}
