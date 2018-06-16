using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float _lookSpeed;
    protected Vector3 _direction;
    [SerializeField]
    protected Rigidbody _rigidBody;

	// Use this for initialization
	void Start ()
    {
		if(_rigidBody == null)
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
        if(_speed == 0)
        {
            _speed = 10;
        }
        if (_lookSpeed == 0)
        {
            _lookSpeed = 5;
        }
    }
	
	void FixedUpdate ()
    {
        HandleInput();
        Move();
        Animate();
	}

    private void Move()
    {
        _rigidBody.velocity = _direction * _speed * Time.deltaTime * Utils.ADJUST_SPEED;
    }

    private void Animate()
    {
        // Not sure if this is needed.
    }

    private void HandleInput()
    {
        _direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 cameraDirection = Camera.main.transform.TransformDirection(Vector3.forward);
            cameraDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);
            _direction = cameraDirection;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // change these so that they just rotate the camera.
        {
            //_direction += cameraDirection;
            //_lookDirection += cameraDirection;
            transform.Rotate(Vector3.down * _lookSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 cameraDirection = Camera.main.transform.TransformDirection(Vector3.back);
            cameraDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);
            _direction = cameraDirection;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // change these so that they just rotate the camera.
        {
            //_direction += cameraDirection;
            //_lookDirection += cameraDirection;
            transform.Rotate(Vector3.up * _lookSpeed);
        }

        if(Input.GetKey(KeyCode.Space))
        {

        }
    }
}
