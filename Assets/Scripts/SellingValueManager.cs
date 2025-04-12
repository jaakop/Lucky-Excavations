using UnityEngine;
using UnityEngine.Serialization;

public class SellingValueManager : MonoBehaviour
{
    [FormerlySerializedAs("CracksVisibility")]
    [Header("Visuals")]
    private Renderer _objectRenderer;
    private MaterialPropertyBlock _propertyBlock;
    private static readonly int CracksTex = Shader.PropertyToID("_CracksTex");
    private static readonly int CracksVisibility = Shader.PropertyToID("_CracksVisibility");

    private void Start()
    {
        _objectRenderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
        
        UpdateDamageVisual();
    }
    
    void UpdateDamageVisual()
    {
        //TODO
    }
    
    [Header("Damage Properties")]
    [SerializeField]
    private float minimumDamage = 10f;      // This number is ADDED to the calculation (not used for clamping!)
    [SerializeField]
    private float damageMultiplier = 2f;
    private float _durability = 100f;       // How much damage the item can sustain

    [Header("Selling Value")]
    [SerializeField]
    private float dirtiness = 100f;         // % of the item surface that is "dirty" and needs to be cleaned
    [SerializeField]
    private float cleaningStep = 20f; // The amount of surface that gets "cleaned" with a single "PerformCleaning()" call
    [SerializeField]
    private float maxSellingValue = 1000f;  // The selling value when the item has 100% durability and is fully clean
    
    public float GetSellingValue()
    {
        return maxSellingValue * _durability * 0.01f * (1 - dirtiness * 0.01f);
    }

    public void PerformCleaning()
    {
        dirtiness -= cleaningStep;
        if(dirtiness < 0)
            dirtiness = 0;
    }
    public void HandleCollisionDamage(float velocityMagnitude)
    {
        _durability -= CalculateCollisionDamage(velocityMagnitude);
        Debug.Log("Collided with a wall at speed: " + velocityMagnitude + ". Damage taken: " + CalculateCollisionDamage(velocityMagnitude));

        if (_durability <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Item destroyed");
        }
    }
    private float CalculateCollisionDamage(float velocityMagnitude)
    {
        return velocityMagnitude * damageMultiplier + minimumDamage;
    }
}
