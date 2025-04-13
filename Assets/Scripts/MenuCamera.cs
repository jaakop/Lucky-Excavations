using UnityEngine;

// Moves the camera between 2 points in space
public class MenuCamera : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;
    [SerializeField]
    private Transform pointB;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed;
    
    private void Update()
    {
        var t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        
        // Interpolate between point A and B
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
        
        // Always look at the target
        if (target)
        {
            transform.LookAt(target);
        }
    }
}
