using UnityEngine;

public class scoreText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        var wheel = FindAnyObjectByType<FortuneWheelManager>(FindObjectsInactive.Include);
        GetComponent<TMPro.TMP_Text>().text = wheel.CurrentCoints.ToString("0");
    }
}
