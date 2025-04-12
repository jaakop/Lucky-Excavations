using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SellingValueManager))]
public class DraggableItem : Draggable
{
    private SellingValueManager _valueManager;

    public SellingValueManager GetValueManager()
    {
        return _valueManager;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(_bIsBeingDragged)
            _valueManager.HandleCollisionDamage(collision.relativeVelocity.magnitude);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<DraggableTool>(out var tool))
        {
            if(!tool.BeingUsed ||tool.toolType != ToolType.Brush)
                return;
            if(!other.isTrigger)
                return;
            _valueManager.PerformCleaning();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        _valueManager = GetComponent<SellingValueManager>();
    }
}
