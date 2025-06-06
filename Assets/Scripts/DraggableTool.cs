using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableTool : Draggable
{
    private float initialDragHeigh;

    public bool BeingUsed {get; protected set;}

    public ToolType toolType;

    [SerializeField]
    private float useHeight = 0;

    protected override void Awake()
    {
        base.Awake();
        initialDragHeigh = dragHeight;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            dragHeight = useHeight;
            BeingUsed = true;
        }
        else
        {
            dragHeight = initialDragHeigh;
            BeingUsed = false;
        }
    }
}

public enum ToolType
{
    Shovel,
    Brush,
    Pick,
}
