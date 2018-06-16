using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheHunt : MonoBehaviour {

    private Vector3 _target;
    [SerializeField]
    private float _speed;
    private BearStates _state;
    [SerializeField]
    private Transform _playerTransform;
    private Rigidbody _rigidbody;
    [SerializeField]
    private static float ATTACK_THRESHOLD = 30f;
    private float _distractTimer;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        if(_speed == 0)
        {
            _speed = 10f;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        bool isCloseEnoughToAttack = false;
        bool isDistracted = false;
        switch (_state)
        {
            case BearStates.SEARCH:
                Move();
                isCloseEnoughToAttack = BearCanAttack();
                isDistracted = BearIsDistracted();
                break;
            case BearStates.ATTACK:
                Attack();
                isCloseEnoughToAttack = BearCanAttack();
                isDistracted = BearIsDistracted();
                break;
            case BearStates.DISTRACT:
                if(Time.realtimeSinceStartup - _distractTimer > 0)
                {
                    // here we set the bear back to search
                    isCloseEnoughToAttack = false;
                    isDistracted = false;
                }
                break;
        }
        _state = isCloseEnoughToAttack ? BearStates.ATTACK : BearStates.SEARCH;
        _state = isDistracted ? BearStates.DISTRACT : BearStates.SEARCH;
	}

    private void Move()
    {
        transform.LookAt(_target);        
        _rigidbody.velocity = transform.forward * _speed * Time.deltaTime * Utils.ADJUST_SPEED;
    }

    private void Attack()
    {
        transform.LookAt(_playerTransform);
        _rigidbody.velocity = transform.forward * _speed * Time.deltaTime * Utils.ADJUST_SPEED;
    }

    private bool BearIsDistracted()
    {
        return false;
    }

    private bool BearCanAttack()
    {
        bool res = CircleSpawner.DistanceIsLessThanThreshold(_rigidbody.position, _playerTransform.position, ATTACK_THRESHOLD);
        if (res) { Debug.Log("Bear is switching to ATTACK"); }
        return res;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Debug.Log("Eating player. NOM NOM NOM.");
            SceneManager.LoadScene("MainMenu"); 
            //Destroy(other.gameObject);
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
