using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRecordReader : MonoBehaviour
{
    [SerializeField] private Record record = default;
    [SerializeField] private bool playOnStart = false;
    [SerializeField, Range(0.25f, 3.0f)] private float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart) StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        Debug.Log("Start lecture.");

        float timer = 0.0f;
        float duration = record.keys[record.keys.Count - 1].timecode;
        int keyIndex = 1;

        float lastTimeDif = 0.0f;
        int tmpIndex = 0;
        float lastTimecode = 0.0f;
        Key currentKey;

        transform.position = record.keys[0].position;
        transform.rotation = record.keys[0].rotation;

        yield return null;

        while (keyIndex < record.keys.Count)
        {
            //tmpIndex = keyIndex;

            //do
            //{
            //    lastTimeDif = timer - record.keys[tmpIndex].timecode;
            //    tmpIndex++;
            //} while (tmpIndex < record.keys.Count && lastTimeDif < timer - record.keys[tmpIndex].timecode);

            //keyIndex = tmpIndex -1;

            keyIndex++;

            currentKey = record.keys[keyIndex];
            float timeLerp = (timer - lastTimecode) / (currentKey.timecode - lastTimecode);
            Debug.Log(keyIndex+" -> "+ timer + " - " + lastTimecode + " - " + currentKey.timecode);

            transform.position = currentKey.position;// Vector3.Lerp(transform.position, currentKey.position, timeLerp);
            transform.rotation = currentKey.rotation;// Quaternion.Lerp(transform.rotation, currentKey.rotation, timeLerp);

            yield return new WaitForSeconds(Time.deltaTime/speed);
            timer += Time.deltaTime;
            lastTimecode = currentKey.timecode;
        }
    }
}
