using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Map Design");
    }

    public void GoBack()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void GoControls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
