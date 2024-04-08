using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectParent : PathPoints
{
    public PathPoints[] CommonPathPoints;
    public PathPoints[] RedPathPoints;
    public PathPoints[] GreenPathPoints;
    public PathPoints[] YellowPathPoints;
    public PathPoints[] BluePathPoints;

    [Header("Scales and Positioning Difference")]
    public float[] scales;
    public float[] positionDifference;
    public PathPoints[] BasePoints;
    public List<PathPoints> safePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
