using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableItem : MonoBehaviour
{
    private Camera _mainCam;
    private Rigidbody _rb;
    private bool _bIsBeingDragged;

    public void SetIsBeingDragged(bool bInIsBeingDragged)
    {
        _bIsBeingDragged = bInIsBeingDragged;
        _rb.useGravity = !_bIsBeingDragged;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        // TODO Replace this with actual logic
        if(Input.GetKeyDown(KeyCode.Space))
            SetIsBeingDragged(true);
        if(Input.GetKeyUp(KeyCode.Space))
            SetIsBeingDragged(false);
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

    // Drag properties
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
