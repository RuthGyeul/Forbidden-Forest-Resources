using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    Image loadingBar; //get lading bar

    private void Start()
    {
        loadingBar.fillAmount = 0; //set loadingbar to 0%
        StartCoroutine(LoadAsyncScene()); //start loading coroutine
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LaodingScene"); //set loading scene
    }
    IEnumerator LoadAsyncScene()
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("GameScene"); //set scene to change
        asyncScene.allowSceneActivation = false;
        float timeC = 0;
        while (!asyncScene.isDone)
        {
            yield return null;
            timeC += Time.deltaTime/5; //set time dutration
            if (asyncScene.progress >= 0.9f) //if progress is 90%
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, timeC); //limit
                if (loadingBar.fillAmount == 1.0f) //if bar is filled
                {
                    asyncScene.allowSceneActivation = true; //change scene
                }
            } else {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC); //set fill limit
                if (loadingBar.fillAmount >= asyncScene.progress) //timeout
                {
                    timeC = 0f;
                }
            } 
        }
    }
}
