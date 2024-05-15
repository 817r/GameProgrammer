using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private float _speed;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;
    private float _timeStamp;
    private float _waitTime;
    private float _timeNow;

    bool waitTimes = true;

    void Start()
    {
        TargetNextWaypoint();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _timeStamp += Time.deltaTime;
        _timeNow = _timeStamp;

        //Debug.Log(_elapsedTime);
        //Debug.Log(_timeNow);
        //Debug.Log(_waitTime);
    }

    void FixedUpdate()
    {
        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);
        //Debug.Log(elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            if (waitTimes)
            {
                waitTime(4f);
                waitTimes = false;
            }
                
            if (_timeNow >= _waitTime)
            {
                TargetNextWaypoint();
                waitTimes = true;
            }
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    private void waitTime(float value)
    {
        _waitTime = _timeNow + value;
    }
}
