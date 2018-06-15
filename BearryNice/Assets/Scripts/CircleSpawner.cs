using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _player;
    private Rigidbody _playerRigidbody;
    private Transform _circle;

    [SerializeField]
    private GameObject _bear;
    private TheHunt _hunt;

    private Vector3 _center;
    private float _radius;

    [SerializeField]
    private float RADIUS_MODIFIER = 0.3f;
    [SerializeField]
    private float MIN_RADIUS = 5f;
    [SerializeField]
    private float MAX_RADIUS = 1000f;

    private int _frameCounter = 0;
    private int FRAME_DELAY = 120;

    private float DISTANCE_THRESHOLD = 3f;

    public Vector3 _bearTarget
    {
        get
        {
            Vector3 temp = Random.insideUnitCircle * _radius;
            _center = new Vector3(_center.x, 0, _center.z);
            return _center + temp;
        }
    }


    // Use this for initialization
    void Start () {
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _hunt = _bear.GetComponent<TheHunt>();
        _circle = GetComponent("Circle") as Transform;
        _radius = MAX_RADIUS;
	}
	
	// Update is called once per frame
	void Update () {
        bool playerLeftCircle = CheckPlayerPosition();
        if(playerLeftCircle || _frameCounter++ % FRAME_DELAY == 0)
        {
            SetCircle();
        }
        bool bearReachedTarget = CheckBearReachedTarget();
	}

    bool CheckPlayerPosition()
    {
        var dX = Mathf.Abs(_player.transform.position.x - _center.x);
        var dY = Mathf.Abs(_player.transform.position.z - _center.z);
        return (dX * dX) + (dY * dY) > (_radius * _radius);
    }

    bool CheckBearReachedTarget()
    {
        var dX = Mathf.Abs(_bear.transform.position.x - _center.x);
        var dY = Mathf.Abs(_bear.transform.position.z - _center.z);
        return (dX * dX) + (dY * dY) < DISTANCE_THRESHOLD;
    }

    void SetCircle()
    {
        _center = _playerRigidbody.position;
        float oldRadius = _radius;
        _radius = _playerRigidbody.velocity.magnitude / RADIUS_MODIFIER;
        _radius = _radius <= 0 ? MAX_RADIUS : _radius;
        _radius = _radius > MAX_RADIUS ? MAX_RADIUS : _radius;
        transform.localScale = new Vector3(_radius, 0, _radius);
        transform.position = _center;
        _hunt.SetTarget(_bearTarget);
    }
}
