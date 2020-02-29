using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In ms")] [SerializeField] float xRange = 6.5f;
    [Tooltip("In ms")] [SerializeField] float yRange = 4f;
    [SerializeField] GameObject[] guns;


    [Header("Screen-Postion Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-Throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    bool controlsEnabled = true;
    // Update is called once per frame
    void Update()
    {
        if(controlsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            //gun.SetActive(isActive);
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    void OnPlayerDeath() //called by string reference
    {
        //print("Controls Frozen");
        controlsEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow*controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        //print(xOffset);
        float rawX = transform.localPosition.x + xOffset;
        float xPos = Mathf.Clamp(rawX, -xRange, xRange);

        //transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawY = transform.localPosition.y + yOffset;
        float yPos = Mathf.Clamp(rawY, -yRange, yRange);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}
