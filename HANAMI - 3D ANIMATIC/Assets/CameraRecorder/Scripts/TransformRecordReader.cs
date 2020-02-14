using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRecordReader : MonoBehaviour
{
    [SerializeField] private Record record = default;
    [SerializeField] private bool playOnStart = false;
    [SerializeField, Range(0.25f, 3.0f)] private float speed = 1.0f;
    [SerializeField] private bool relative = false;
    [SerializeField, Range(0.001f, 10.0f)] private float positionMagnitude = 1.0f;
    [SerializeField, Range(0.001f, 1.0f)] private float rotationLerp = 1.0f;
    [SerializeField] private bool useLocal = false;


    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart)
        {
            if(!useLocal)StartCoroutine(Play());
            else StartCoroutine(PlayLocal());
        }
    }

    private IEnumerator Play()
    {
        Debug.Log("Start lecture.");

        float timer = 0.0f;
        float duration = record.keys[record.keys.Count - 1].timecode;
        int keyIndex = 1;
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        float lastTimeDif = 0.0f;
        int tmpIndex = 0;
        float lastTimecode = 0.0f;
        Key currentKey;

        Vector3 posCorrection = (record.keys[0].position - transform.position) * positionMagnitude;
        Quaternion rotationCorrection = record.keys[0].rotation * Quaternion.Inverse(transform.rotation);

        transform.position = (record.keys[0].position * positionMagnitude) - posCorrection;
        transform.rotation = record.keys[0].rotation * Quaternion.Inverse(rotationCorrection);

        yield return null;

        while (keyIndex < record.keys.Count-1)
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
            //Debug.Log(keyIndex+" -> "+ timer + " - " + lastTimecode + " - " + currentKey.timecode);
            if (!relative)
            {
                transform.position = currentKey.position* positionMagnitude;// Vector3.Lerp(transform.position, currentKey.position, timeLerp);
                transform.rotation = Quaternion.Lerp(transform.rotation, currentKey.rotation, rotationLerp);// Quaternion.Lerp(transform.rotation, currentKey.rotation, timeLerp);
            }
            else
            {
                transform.position = (currentKey.position * positionMagnitude )- posCorrection;
                transform.rotation = Quaternion.Lerp(transform.rotation, currentKey.rotation, rotationLerp);
            }

            yield return new WaitForSeconds((duration / record.keys.Count )/speed);
        }
        stopwatch.Stop();
        Debug.Log(stopwatch.ElapsedMilliseconds / 1000.0f);
    }

    private IEnumerator PlayLocal()
    {
        Debug.Log("Start lecture.");

        float timer = 0.0f;
        float duration = record.keys[record.keys.Count - 1].timecode;
        int keyIndex = 1;
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        float lastTimeDif = 0.0f;
        int tmpIndex = 0;
        float lastTimecode = 0.0f;
        Key currentKey;

        Vector3 posCorrection = (record.keys[0].position - transform.localPosition) * positionMagnitude;
        Quaternion rotationCorrection = record.keys[0].rotation * Quaternion.Inverse(transform.localRotation);

        transform.localPosition = (record.keys[0].position * positionMagnitude) - posCorrection;
        transform.localRotation = record.keys[0].rotation * Quaternion.Inverse(rotationCorrection);

        yield return null;

        while (keyIndex < record.keys.Count - 1)
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
            //Debug.Log(keyIndex+" -> "+ timer + " - " + lastTimecode + " - " + currentKey.timecode);
            if (!relative)
            {
                transform.localPosition = currentKey.position * positionMagnitude;// Vector3.Lerp(transform.position, currentKey.position, timeLerp);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, currentKey.rotation, rotationLerp);// Quaternion.Lerp(transform.rotation, currentKey.rotation, timeLerp);
            }
            else
            {
                transform.localPosition = (currentKey.position * positionMagnitude) - posCorrection;
                transform.localRotation = Quaternion.Lerp(transform.localRotation, currentKey.rotation, rotationLerp);
            }

            yield return new WaitForSeconds((duration / record.keys.Count) / speed);
        }
        stopwatch.Stop();
        Debug.Log(stopwatch.ElapsedMilliseconds / 1000.0f);
    }
}
