using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] float acceleration = 98.1f;

    Vector3 gravityOffset = Vector3.zero;

    bool isActive = true;
    private void Start() {
        if(SystemInfo.supportsGyroscope) {
            Input.gyro.enabled = true;
        }

        //CalibrateGravity();
    }

    private void Update() {
        if(isActive)
        {
            Physics.gravity = GetGravityFromSensor() + gravityOffset;
        }
        else
        {
            Physics.gravity = Vector3.zero;
        }
        //CalibrateGravity();
    }

    public void CalibrateGravity() {
        gravityOffset = Vector3.down * acceleration - GetGravityFromSensor();
    }

    public Vector3 GetGravityFromSensor(){
        Vector3 gravity;
        if(Input.gyro.gravity != Vector3.zero) {
            gravity = Input.gyro.gravity * acceleration;
        } else {
            gravity = Input.acceleration * acceleration;
        }

        //gravity landscape
        //gravity = Quaternion.Euler(0f, 0f, -90f) * gravity;

        gravity.z = Mathf.Clamp(gravity.z, float.MinValue, -1f);
        return new Vector3(gravity.x, gravity.z, gravity.y);
    }

    public void SetActive(bool value) {
        isActive = value;
        if(value){
            Time.timeScale = 1f;
        }
        else{
            Time.timeScale = 0f;
        }
    }
}
