using UnityEngine;
using UnityEngine.Events;

public class RelicSpot : MonoBehaviour
{
    public UnityEvent<Vector3> OnSpotDiscovered = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<DraggableTool>(out var tool))
        {
            if(tool.toolType != ToolType.Shovel)
                return;
            if(!tool.BeingUsed)
                return;

            OnSpotDiscovered.Invoke(transform.position);
        }
    }
}
