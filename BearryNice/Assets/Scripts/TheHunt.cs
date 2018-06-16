using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHunt : MonoBehaviour {

    private Vector3 _target;
    [SerializeField]
    private float _speed;
    private BearStates _state;
    [SerializeField]
    private Transform _playerTransform;

	// Use this for initialization
	void Start () {
        if(_speed == 0)
        {
            _speed = 10f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        switch(_state)
        {
            case BearStates.SEARCH:
                
                break;
            case BearStates.ATTACK:
                break;
            case BearStates.DISTRACT:
                break;
        }
        Move();
	}

    void Move()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        transform.LookAt(_target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Debug.Log("Eating player. NOM NOM NOM.");
            Destroy(other.gameObject);
        }
    }
    
    public Vector3 GetTarget()
    {
        return _target;
    }

    public void ChangeTarget(Vector3 newTarget)
    {
        Debug.Log("Changing bear target " + Time.realtimeSinceStartup);
        _target = new Vector3(newTarget.x, 0, newTarget.z);
    }
}
