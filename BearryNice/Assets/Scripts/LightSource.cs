using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public Light flashlight;

    [Header("Settings")]
    public float currentPercentage = 1.0f;
    public float decayRate = 0.02f;
    public float batteryValue = 0.1f;

    [Header("Controls")]
    public KeyCode lightSwitchKey = KeyCode.F;

    private float MAX_RANGE;
    private float MAX_LIGHT;
    private bool adjustLight;

    // Use this for initialization
    void Start()
    {
        if (flashlight == null) throw new MissingReferenceException("Need to define reference to flashlight.");
        MAX_RANGE = flashlight.range;
        MAX_LIGHT = currentPercentage;
        adjustLight = flashlight.gameObject.activeInHierarchy;
    }

    IEnumerator FlashLightDecayTimer()
    {
        while (!flashlight.gameObject.activeInHierarchy) yield return null;
        yield return new WaitForSecondsRealtime(1);
        adjustLight = true;   
    }

    private void Update()
    {
        HandleKeyInput();
        if(adjustLight)
        {
            currentPercentage -= decayRate;
            Debug.Log("current% = " + currentPercentage);
            if (currentPercentage < 0)
            {
                currentPercentage = 0;
            }
            flashlight.range = MAX_RANGE * currentPercentage;
            Debug.Log(flashlight.range);
            adjustLight = false;
            StartCoroutine("FlashLightDecayTimer");
        }
    }

    public void AddLight()
    {
        currentPercentage += batteryValue;
        if (currentPercentage > MAX_LIGHT) currentPercentage = MAX_LIGHT;
    }

    void HandleKeyInput()
    {
        if (Input.GetKeyDown(lightSwitchKey))
        {
            flashlight.gameObject.SetActive(!flashlight.gameObject.activeInHierarchy);
        }
    }
}
