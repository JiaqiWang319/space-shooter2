using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelsMenuExistingUser : MonoBehaviour {

    public InputField userName;
    public InputField userPassword;
    string username;
    string password;

    private void Start()
    {
        username = null;
        password = null;
    }

    // Update is called once per frame
    public void LoadLevelMenu()
    {
        // check username and password against the database
        if (userName.text == null || userPassword.text == null)
        {
            
        }
        else if (userName.text != null)// && userPassword.text != null)
        {
            SceneManager.LoadScene(1);
        }
    }
}
