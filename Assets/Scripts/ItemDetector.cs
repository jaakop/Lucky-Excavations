using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]

// Detects when an object that is an "Item" overlaps with the box collider
// Use the onItemDetected even to customize behaviour
public class ItemDetector : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onItemDetected = new UnityEvent();
    
    private BoxCollider _boxCollider;
    
    public SellingValueManager item {get; private set;}

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the overlapping object is an "Item"
        if (!other.CompareTag("Item")) return;
        
        Debug.Log("Detected item: " + other.name);
        onItemDetected.Invoke();
        item = other.GetComponent<SellingValueManager>();
    }

    public void ClearItem()
    {
        Destroy(item.gameObject);
        item = null;
    }
}
