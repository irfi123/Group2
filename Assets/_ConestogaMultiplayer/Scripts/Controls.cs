using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlsTutorial : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainScene");
    }
}