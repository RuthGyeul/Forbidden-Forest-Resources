using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    Image loadingBar; //get loading bar

    private void Start()
    {
        loadingBar.fillAmount = 0; //set loadingbar to 0
        StartCoroutine(ReadyScene()); //start load scene coroutine
    }

    IEnumerator ReadyScene()
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("GameScene"); //call game scene
        asyncScene.allowSceneActivation = false; //don't change scene yet
        float timeC = 0;
        while (!asyncScene.isDone) //if loading is not done
        {
            yield return null;
            timeC += Time.deltaTime/4; //set time dutration
            if (asyncScene.progress >= 0.9f) //if progress is 90%
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, timeC); //give limit
                if (loadingBar.fillAmount == 1.0f) //if bar is 100% filled
                {
                    asyncScene.allowSceneActivation = true; //change scene
                }
            } else {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC); //fill bar as progress goes
                if (loadingBar.fillAmount >= asyncScene.progress) //when timeout
                {
                    timeC = 0f; //reset time
                }
            }
        }
    }
}
