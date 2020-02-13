using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRecorder : MonoBehaviour
{
    [SerializeField] private Record record = default;
    [SerializeField] private Transform objectRecorded = default;
    [SerializeField] private Status status = Status.Stopped;

    private bool stop;
    private bool pause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Record();
        else if (Input.GetKeyDown(KeyCode.S)) Stop();
        else if (Input.GetKeyDown(KeyCode.P)) Pause();
    }

    public void Record()
    {
        if(status == Status.Stopped) StartCoroutine(RecordRoutine());
    }

    public void Stop()
    {
        if (status != Status.Stopped) stop = true;
    }

    public void Pause()
    {
        if (status != Status.Stopped)
        {
            pause = !pause;

            if (pause) Debug.Log("Recording paused.");
            else Debug.Log("Recording unpaused.");
        }
    }

    public IEnumerator RecordRoutine()
    {
        float timer = 0.0f;

        Debug.Log("Start recording");
        status = Status.Recording;

        record.Init();

        while (!stop)
        {
            if (!pause) record.keys.Add(new Key(timer, objectRecorded ? objectRecorded : transform));

            yield return null;
            if(!pause) timer += Time.deltaTime;
        }

        stop = false;

        Debug.Log("Stop recording.");
        status = Status.Stopped;
    }

    public enum Status { Stopped, Recording, Paused}
}
