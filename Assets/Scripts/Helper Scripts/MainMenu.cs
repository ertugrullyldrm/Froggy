using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            int a = PlayerPrefs.GetInt("SavedScene");
            if (a == 0)
            {
                PlayerPrefs.SetInt("SavedScene", 1);
                PlayerPrefs.Save();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonEvent()
    {

        if (PlayerPrefs.HasKey("SavedScene"))
        {

            int savedSceneIndex = PlayerPrefs.GetInt("SavedScene");
            Debug.Log(savedSceneIndex);
            SceneManager.LoadScene(savedSceneIndex);

        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
