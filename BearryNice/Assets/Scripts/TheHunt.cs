using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHunt : MonoBehaviour {

    private Vector3 _target;
    [SerializeField]
    private float _speed;

	// Use this for initialization
	void Start () {
        if(_speed == 0)
        {
            _speed = 10f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.LookAt(_target);
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Debug.Log("Eating player. NOM NOM NOM.");
            Destroy(other.gameObject);
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        _target = new Vector3(newTarget.x, 0, newTarget.z);
    }
}
