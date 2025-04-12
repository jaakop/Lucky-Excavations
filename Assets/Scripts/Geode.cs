using UnityEngine;

public class Geode : MonoBehaviour
{
    public GameObject item;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<DraggableTool>(out var tool))
        {
            if(!tool.BeingUsed  || tool.toolType != ToolType.Pick)
                return;

            if(tool.GetComponent<Rigidbody>().linearVelocity.magnitude < 1)
                return;
            
            var inst = Instantiate(item);
            inst.transform.position = transform.position;

            Destroy(gameObject);
        }
    }
}
