using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMain() {

        //load main level
        SceneManager.LoadScene(1);
    
    }

    public void ExitApp()
    {
        Application.Quit();
        Debug.Log("GoodBye!");
    }
}
