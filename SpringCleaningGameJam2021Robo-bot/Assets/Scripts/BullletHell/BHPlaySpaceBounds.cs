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

    private void OnDrawGizmos()
    {
        Vector2 p1 = new Vector2(playSpaceBounds.min.x, playSpaceBounds.min.y);
        Vector2 p2 = new Vector2(playSpaceBounds.max.x, playSpaceBounds.min.y);
        Vector2 p3 = new Vector2(playSpaceBounds.max.x, playSpaceBounds.max.y);
        Vector2 p4 = new Vector2(playSpaceBounds.min.x, playSpaceBounds.max.y);


        Gizmos.color = Color.blue;

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);

        Gizmos.color = Color.white;
    }
}
