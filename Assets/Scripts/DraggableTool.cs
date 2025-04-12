using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableTool : Draggable
{
    private float initialDragHeigh;

    protected override void Awake()
    {
        base.Awake();
        initialDragHeigh = dragHeight;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            dragHeight = -1;
        }
        else
        {
            dragHeight = initialDragHeigh;
        }
    }
}
