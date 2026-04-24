using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoScreen : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}