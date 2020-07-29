using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public int SceneNumberToLoad;
    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneNumberToLoad);
    }
    public void LoadLevel(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

}

