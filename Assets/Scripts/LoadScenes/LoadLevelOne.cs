using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelOne : MonoBehaviour {

	// Update is called once per frame
	public void LoadLevel1 ()
    {
        SceneManager.LoadScene(2);
    }
/*
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    */
}
