using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    private void Awake()
    {
        // Add all waypoints to public waypoints property
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
    
}
