using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensions
{
    
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float cameraHeight = camera.orthographicSize * 2;
        Vector3 boundsSize = new Vector3(cameraHeight * camera.aspect, cameraHeight, 0);
        
        Bounds bounds = new Bounds( camera.transform.position, boundsSize);

        return bounds;
    }
}
