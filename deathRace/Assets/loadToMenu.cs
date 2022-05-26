using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadToMenu : MonoBehaviour
{


    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());

    }

    IEnumerator ExampleCoroutine()
    {

        yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Menu");

    }

}