using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int numRequiredItems;

    private int itemsSold = 0;

    [SerializeField]
    private GameObject gameOverCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemsSold = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void OnItemSold()
    {
        itemsSold++;

        if(itemsSold >= numRequiredItems)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
