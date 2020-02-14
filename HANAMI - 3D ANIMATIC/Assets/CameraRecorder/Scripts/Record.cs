using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Record")]
public class Record : ScriptableObject
{
    public List<Key> keys;
    [SerializeField] private bool saved = default;

    public void Init()
    {
        keys = new List<Key>();
        saved = false;
    }
}
