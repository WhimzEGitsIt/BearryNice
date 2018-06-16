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
    private static int FRAME_DELAY = 120;

    private static float DISTANCE_THRESHOLD = 3f;

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
        bool playerLeftCircle = CheckPointOutsideCircle(_player.transform.position, _center, _radius);
        if(playerLeftCircle || _frameCounter++ % FRAME_DELAY == 0)
        {
            _frameCounter = 0;
            SetCircle();
        }
        bool bearReachedTarget = DistanceIsLessThanThreshold(_bear.transform.position, _center, DISTANCE_THRESHOLD);
        if (bearReachedTarget)
        {
            Debug.Log("Bear reached target. Setting new target");
            _hunt.ChangeTarget(_bearTarget);
        }
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
        if(CheckPointOutsideCircle(_hunt.GetTarget(), _center, _radius))
        {
            _hunt.ChangeTarget(_bearTarget);
        }
    }

    public static bool DistanceIsLessThanThreshold(Vector3 one, Vector3 two, float threshold)
    {
        var dX = Mathf.Abs(one.x - two.x);
        var dY = Mathf.Abs(one.z - two.z);
        return (dX * dX) + (dY * dY) < threshold;
    }

    public static bool CheckPointOutsideCircle(Vector3 position, Vector3 center, float radius)
    {
        var dX = Mathf.Abs(position.x - center.x);
        var dY = Mathf.Abs(position.z - center.z);
        return (dX * dX) + (dY * dY) > (radius * radius);
    }
}
