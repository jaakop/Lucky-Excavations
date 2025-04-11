using UnityEngine;

public class DragController : MonoBehaviour
{
    Camera camera;

    DraggableItem currentItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
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
            currentItem.SetIsBeingDragged(false);
            currentItem = null;
        }
    }

    void ShootRay() 
    {
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
        {
            if(!hitInfo.collider.TryGetComponent<DraggableItem>(out var item))
            {
                return;
            }

            item.SetIsBeingDragged(true);
            currentItem = item;
        }
    }
}
