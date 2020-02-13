using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Record")]
public class Record : ScriptableObject
{
    public List<Key> keys;

    public void Init()
    {
        keys = new List<Key>();
    }
}
