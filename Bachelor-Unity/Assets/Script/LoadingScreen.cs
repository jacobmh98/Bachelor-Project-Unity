using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Image panel;
    public Sprite sprite;
    Controller controller = Controller.getInstance();

    public void loadingScene(int sceneIndex)
    {
        print("loadingScene");
        panel.sprite = sprite;
        //System.Threading.Thread.Sleep(5000);
        //StartCoroutine(LoadAsynchronously(sceneIndex));
        controller.PointLoader();
        SceneManager.LoadScene(sceneIndex);

    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
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
