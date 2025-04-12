using UnityEngine;

public class DragController : MonoBehaviour
{
    // If true, dragged items will have their velocity multiplied by velocirtMultiplier when grabbed or let go of
    [SerializeField]
    private bool bUseVelocityMultiplier = true;
    // If true, items will have their velocity reset to 0,0,0 when grabbed (but not when let go of)
    [SerializeField]
    private bool bResetVelocityOnGrab = true;
    [SerializeField]
    private float velocityMultiplier = 0f;
    
    Camera mainCamera;

    Draggable currentItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(currentItem == null)
                ShootRay();
        }    
        if(Input.GetMouseButtonUp(0))
        {
            if(currentItem == null)
                return;
            var finalVelocityMultiplier = bUseVelocityMultiplier ? velocityMultiplier : 1f;
            currentItem.SetIsBeingDragged(false, finalVelocityMultiplier);
            currentItem = null;
        }
    }

    void ShootRay() 
    {
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
        {
            if(!hitInfo.collider.TryGetComponent<Draggable>(out var item))
            {
                return;
            }
            
            // It's probably possible to avoid calculating this if bResetVelocityOnGrab is true, but I don't care enough
            var finalVelocityMultiplier = bUseVelocityMultiplier ? velocityMultiplier : 1f;
            
            item.SetIsBeingDragged(true, bResetVelocityOnGrab ? 0 : finalVelocityMultiplier);
            currentItem = item;
        }
    }
}
