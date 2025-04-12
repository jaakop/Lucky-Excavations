using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SellingValueManager))]
public class DraggableItem : MonoBehaviour
{
    private Camera _mainCam;
    private Rigidbody _rb;
    private SellingValueManager _valueManager;
    private bool _bIsBeingDragged;

    public SellingValueManager GetValueManager()
    {
        return _valueManager;
    }
    
    public void SetIsBeingDragged(bool bInIsBeingDragged, float velocityMultiplier = 1f)
    {
        _bIsBeingDragged = bInIsBeingDragged;
        _rb.useGravity = !_bIsBeingDragged;
        
        // Zero out velocity for more predictable behaviour
        _rb.linearVelocity = Vector3.zero;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(_bIsBeingDragged)
            _valueManager.HandleCollisionDamage(collision.relativeVelocity.magnitude);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _valueManager = GetComponent<SellingValueManager>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }
    
    private void FixedUpdate()
    {
        if (_bIsBeingDragged)
            DragItemToLocation(GetCursorLocationOnPlane());
    }

    private Vector3 GetCursorLocationOnPlane()
    {
        if (_mainCam)
        {
            var mouseCameraRay = _mainCam.ScreenPointToRay(Input.mousePosition);
            var gamePlane = new Plane(Vector3.up, Vector3.zero);
        
            if (gamePlane.Raycast(mouseCameraRay, out float distance))
            {
                return mouseCameraRay.GetPoint(distance);
            }
        }
        return Vector3.zero;
    }

    [Header("Drag Properties")]
    [SerializeField]
    private float forceMultiplier = 5f;
    [SerializeField]
    private float maxSpeed = 10f;
    private void DragItemToLocation(Vector3 location)
    {
        var direction = (location - transform.position).normalized;
        var distance = Vector3.Distance(transform.position, location);
        
        // Don't drag if the cursor is too close to avoid weird movements
        if(distance < 0.1f)
        {
            _rb.linearVelocity = Vector3.zero;
            return;
        }
        
        // Move the Draggable
        _rb.linearVelocity = direction * distance * forceMultiplier;
        // Clamp maximum speed
        if (_rb.linearVelocity.magnitude > maxSpeed) {
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
