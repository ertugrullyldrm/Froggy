using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text movesSize;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;

    
    private bool isMoveFinish = false;

    private void OnEnable()
    {
        FrogHandler.OnClickFrog += FrogHandler_OnClickFrog;
    }

    private void FrogHandler_OnClickFrog()
    {
        GameManager.Instance.moves--;
        movesSize.text = GameManager.Instance.moves + "Moves ";
        if (GameManager.Instance.moves == 0)
        {
            isMoveFinish = true;
            GameManager.Instance.isGameFinish = true;
        }
    }

    private void OnDisable()
    {
        FrogHandler.OnClickFrog -= FrogHandler_OnClickFrog;
    }
    void Start()
    {
        movesSize.text =GameManager.Instance.moves +" Moves ";
    }

    void Update()
    {
        if (GameManager.Instance.FrogCount == 0)
        {
            GameManager.Instance.isGameFinish = true;

           
            WinPanel.SetActive(true);
            
            
        }
        
            
          StartCoroutine(OpenLosePanel());

        



    }


    private IEnumerator OpenLosePanel()
    {
        if (isMoveFinish && GameManager.Instance.FrogCount != 0)
        {
            yield return new WaitForSeconds(2f);
            if (WinPanel.active == false)
                LosePanel.SetActive(true);

        }

        
        


    }


    public void NextLevel()
    {
        
       

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        PlayerPrefs.SetInt("SavedScene", nextSceneIndex);
        PlayerPrefs.Save();
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
            PlayerPrefs.SetInt("SavedScene", nextSceneIndex);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    public void GoMainMenu()
    {
       if(GameManager.Instance.FrogCount==0)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            GameManager.Instance.RestartGame();
            PlayerPrefs.SetInt("SavedScene", nextSceneIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
            GameManager.Instance.RestartGame();
        }

        else
        {
            SceneManager.LoadScene(0);
            GameManager.Instance.RestartGame();
        }
        
       
        

    }

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.RestartGame();
        SceneManager.LoadScene(currentSceneIndex);
    }




}
