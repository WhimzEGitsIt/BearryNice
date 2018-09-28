using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour {

    public GameObject eyes;
    public GameObject flashlight;

    [Header("Movement Settings")]
    public float speed = 2f;
    public float sensitivity = 2f;
    public float currentRunPercentage = 1f;
    public float decayRate = 0.15f;

    [Header("Controls")]
    public KeyCode runKey = KeyCode.LeftShift;

    private float moveFB = 0f;
    private float moveLR = 0f;

    private float rotX = 0f;
    private float rotY = 0f;

    private float MAX_RUNNING;
    private bool isRunning = false;

    CharacterController player;

    // Use this for initialization
    void Start () {
        player = GetComponent<CharacterController>();
        Cursor.visible = false;
        if (flashlight == null || eyes == null) throw new MissingComponentException("Missing necessary components.");
        MAX_RUNNING = currentRunPercentage;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        HandleKeyInput();
	}

    void Move()
    {
        float localSpeed = CanRun() ? speed + speed : speed;
        moveFB = Input.GetAxis("Vertical") * localSpeed;
        moveLR = Input.GetAxis("Horizontal") * localSpeed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -60f, 60f);

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        transform.Rotate(0, rotX, 0);
        eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime);
    }

    private bool CanRun()
    {
        return currentRunPercentage > 0 && isRunning;
    }
    
    void HandleKeyInput()
    {
        if(Input.GetKeyDown(runKey))
        {
            isRunning = true;
            StartCoroutine("RunningTimer");
        }
        if(Input.GetKeyUp(runKey))
        {
            isRunning = false;
            StartCoroutine("ResetRunning");
        }
             
    }

    IEnumerator RunningTimer()
    {
        while(isRunning)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            currentRunPercentage -= decayRate;
            if (currentRunPercentage < 0) currentRunPercentage = 0;
            Debug.Log("RunningTimer: " + currentRunPercentage);
        }
    }

    IEnumerator ResetRunning()
    {
        while(!isRunning)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            currentRunPercentage += decayRate;
            if (currentRunPercentage > MAX_RUNNING) currentRunPercentage = MAX_RUNNING;
            Debug.Log("RunningReset: " + currentRunPercentage);
        }
    }
}
