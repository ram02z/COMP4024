using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        // Change scene to the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void LoadLearnScene()
    {
        // Change scene to the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LearnScene");
    }
}
