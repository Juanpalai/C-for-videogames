using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Canvas menuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
           
        }
    }

    public void ShowMenu()
    { 
        menuCanvas.enabled = true;
    }

    public void HideMenu()
    {
        menuCanvas.enabled=false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Aplication.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
