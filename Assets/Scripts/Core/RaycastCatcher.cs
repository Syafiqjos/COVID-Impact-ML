using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCatcher : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    /*
     * return array of 8 adjacent direction
     * 0: south west
     * 1: south
     * 2: south east
     * 
     * 3: middle west
     * 4: middle east
     * 
     * 5: north west
     * 6: north
     * 7: north east
     */
    public AdjacentDistance GetRaycastDistance() {
        float[] distances = new float[8];

        int index = 0;

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x == 0 && y == 0) continue;
                float raycastDistance = GetRaycastDistance(new Vector2(x, y));
                distances[index] = raycastDistance;
            }
        }

        return new AdjacentDistance(distances);
    }

    public float GetRaycastDistance(Vector2 direction) {
        LayerMask layerMask = LayerMask.GetMask("Raycastable");
        float maxRaycastDistance = 100;

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.transform.position, direction, maxRaycastDistance, layerMask);
        return hit.distance;
    }
}
