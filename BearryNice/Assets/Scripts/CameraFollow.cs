using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    protected Transform _playerTransform;

	// Use this for initialization
	void Start () {
		if (_playerTransform == null)
        {
            GameObject player = GameObject.Find("Player");
            _playerTransform = player.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(_playerTransform);
	}
}
