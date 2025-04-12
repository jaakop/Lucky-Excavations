using System.Collections.Generic;
using UnityEngine;

public class SandPitController : MonoBehaviour
{

    [SerializeField]
    private int numberOfRelicSpots = 5;

    [SerializeField]
    private GameObject relicSpotPrefab;

    [SerializeField]
    private GameObject[] itemPrefabs;

    private List<GameObject> relicSports = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateRelicSpots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateRelicSpots()
    {
        for(int i = 0; i < numberOfRelicSpots; i++)
        {
            var posX = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);       
            var posZ = Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2);

            var relicSpot = Instantiate(relicSpotPrefab, transform);
            relicSpot.transform.position = new Vector3(posX, transform.position.y, posZ);
            relicSpot.GetComponent<RelicSpot>().OnSpotDiscovered.AddListener(OnSpotDiscovered);
            relicSports.Add(relicSpot);
        }
    }

    private void OnSpotDiscovered(Vector3 pos) 
    {
        foreach(var spot in relicSports)
        {
            Destroy(spot);
        }
        relicSports.Clear();

        var item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
        item.transform.rotation = Random.rotation;
        pos.y = 1;
        item.transform.position = pos;

        GenerateRelicSpots();
    }
}
