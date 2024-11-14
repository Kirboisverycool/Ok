using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float loadSceneDelay;

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLoseScene()
    {
        StartCoroutine(DelayLoseScene());
    }

    IEnumerator DelayLoseScene()
    {
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene(2);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(3);
    }
}
