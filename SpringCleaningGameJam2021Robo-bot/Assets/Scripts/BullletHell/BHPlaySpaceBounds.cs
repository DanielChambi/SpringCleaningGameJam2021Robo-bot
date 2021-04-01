using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Controls the size and position of the space where the player will be able to move*/
public class BHPlaySpaceBounds : MonoBehaviour
{
    public Camera mainCamera;

    public float playSpaceAspect = 9/16;   // (width / height) of determined play space

    static Bounds playSpaceBounds;

    void Start()
    {
        //Set initial Play Space Bounds
        Bounds cameraBounds = mainCamera.OrthographicBounds();

        float playSpaceWidht = cameraBounds.size.y * playSpaceAspect;
        Vector3 playSpaceSize = new Vector3( playSpaceWidht, cameraBounds.size.y, 0 );

        playSpaceBounds = new Bounds(cameraBounds.center, playSpaceSize);
    }

    /*Obtaing current bounds for all gameplay to take place (player movement, projectile existance)
     * 
     */
    public static Bounds Bounds()
    {
        return playSpaceBounds;
    }
}
