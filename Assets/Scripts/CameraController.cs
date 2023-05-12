using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
    private void Start() 
    {
        cam = Camera.main;
    }

    private void Update() 
    {
        var touches = Input.touches;

        if (touches.Length == 1)
        {
            Drag(touches);
        }
        else if (touches.Length == 2)
        {
            Zoom(touches);
        }
    }

    private void Drag(Touch[] touches)
    {
        var touch = Input.touches[0];
        var camDistance = cam.transform.position.y;
        var frustumHeight = 2.0f * camDistance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var scale = frustumHeight / Screen.height;
        var delta = touches[0].deltaPosition * scale;
        cam.transform.position -= new Vector3(delta.x, 0, delta.y);
    }

    private void Zoom(Touch[] touches)
    {
        var prevpos0 = touches[0].position - touches[0].deltaPosition;
        var prevpos1 = touches[1].position - touches[1].deltaPosition;
        var prevdist = (prevpos0 - prevpos1).magnitude;
        var dist = (touches[0].position - touches[1].position).magnitude;
        var delta = dist - prevdist;
        cam.transform.position += cam.transform.forward * delta;
        
    }
}
