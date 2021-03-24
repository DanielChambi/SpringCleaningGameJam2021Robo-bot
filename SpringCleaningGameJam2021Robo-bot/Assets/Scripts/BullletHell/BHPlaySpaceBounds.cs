using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHPlaySpaceBounds : MonoBehaviour
{
    /*Controls the size and position of the space where the player will be able to move*/

    public Camera mainCamera;

    public float playSpaceAspect = 9/16;   // (width / height) of determined play space

    Bounds playSpaceBounds;


    void Start()
    {
        /*Set initial Play Space Bounds*/
        Bounds cameraBounds = mainCamera.OrthographicBounds();

        float playSpaceWidht = cameraBounds.size.y * playSpaceAspect;
        Vector3 playSpaceSize = new Vector3( playSpaceWidht, cameraBounds.size.y, 0 );

        playSpaceBounds = new Bounds(cameraBounds.center, playSpaceSize);
    }

    public Bounds GetBounds()
    {
        return playSpaceBounds;
    }
}
