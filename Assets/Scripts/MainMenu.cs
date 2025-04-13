using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool canStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseGame();
        }
        else if(Input.anyKeyDown)
        {
            if(Input.GetMouseButtonDown(0))
                return;

            StartGame();
        }
    }

    public void StartGame()
    {
        if(!canStart)
            return;
        SceneManager.LoadScene(1);
    }

    public void SetCanStart(bool canStart)
    {
        this.canStart = canStart;
    }

    public void CloseGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }
}
