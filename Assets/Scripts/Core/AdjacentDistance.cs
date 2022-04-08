using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AdjacentDistance
{
    /*
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
    readonly float[] distances;

    public AdjacentDistance(float[] distances) {
        this.distances = distances;
    }

    public float GetDistance(int index) {
        return index < distances.Length ? distances[index] : 0;
    }

    public float GetSouthWestDistance()
    {
        return GetDistance(0);
    }

    public float GetSouthDistance()
    {
        return GetDistance(1);
    }

    public float GetSouthEastDistance()
    {
        return GetDistance(2);
    }

    public float GetMiddleWestDistance()
    {
        return GetDistance(3);
    }

    public float GetMiddleEastDistance()
    {
        return GetDistance(4);
    }

    public float GetNorthWestDistance()
    {
        return GetDistance(5);
    }

    public float GetNorthDistance()
    {
        return GetDistance(6);
    }

    public float GetNorthEastDistance()
    {
        return GetDistance(7);
    }
}
