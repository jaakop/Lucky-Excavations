using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public abstract class Draggable : MonoBehaviour
{
    private Camera _mainCam;
    private Rigidbody _rb;
    
    protected bool _bIsBeingDragged;

    public UnityEvent OnBeginDragged = new();
    public UnityEvent OnStopDragging = new();

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        _mainCam = Camera.main;
    }

    public void SetIsBeingDragged(bool bInIsBeingDragged, float velocityMultiplier = 1f)
    {
        _bIsBeingDragged = bInIsBeingDragged;
        _rb.useGravity = !_bIsBeingDragged;
        _rb.constraints = _bIsBeingDragged ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.None;

        if(_bIsBeingDragged)
            OnBeginDragged.Invoke();
        else
            OnStopDragging.Invoke();
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
                var pos = mouseCameraRay.GetPoint(distance);

                if(Physics.Raycast(new Ray(transform.position, Vector3.down), out var groundHit))
                {
                    pos.y = groundHit.point.y + dragHeight;
                }
                
                return pos;
            }
        }
        return Vector3.zero;
    }

    [Header("Drag Properties")]
    [SerializeField]
    private float forceMultiplier = 5f;
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    protected float dragHeight = 2;


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
