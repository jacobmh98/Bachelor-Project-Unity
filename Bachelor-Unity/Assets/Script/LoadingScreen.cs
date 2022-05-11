using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject optionsScreen;
    public Slider slider;
    public Image panel;
    public Sprite sprite;
    Controller controller = Controller.getInstance();

    public void loadingScene(int sceneIndex)
    {

        panel.sprite = sprite;
        //StartCoroutine(WaitingTime());
        StartCoroutine(LoadAsynchronously(sceneIndex));


    }

    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(1);


        
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        yield return new WaitForSeconds(5);
        controller.PointLoader();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            print(progress);
            slider.value = progress;
            yield return null;
        }
        
    }
}
