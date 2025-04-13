using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tools;

    private Vector3[] initPositions;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        initPositions = new Vector3[tools.Length];

        for(int i = 0; i < tools.Length; i++)
        {
            initPositions[i] = tools[i].transform.position;
        }
    }

    void OnMouseDown()
    {
        for(int i = 0; i < tools.Length; i++)
        {
            tools[i].transform.position = initPositions[i];
        }
        audioSource.Play();
    }
}
