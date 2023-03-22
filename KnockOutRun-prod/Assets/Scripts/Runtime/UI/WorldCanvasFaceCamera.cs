using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasFaceCamera : MonoBehaviour
{
    Camera _cam;

    private void Start()
    {
        _cam = Camera.allCameras[0];
    }

    private void Update()
    {
        if (_cam == null)
            return;

        transform.forward = _cam.transform.forward;
    }
}
