using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    public Image progressBar;
    CanvasGroup canvasGroup;

    public static LoadingScreen instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        canvasGroup = GetComponentInChildren<CanvasGroup>();
        Utility.SetCanvasGroupEnabled(canvasGroup, false);

    }
    IEnumerator Coroutine (string name)
    {
        Utility.SetCanvasGroupEnabled(canvasGroup, true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;
            yield return null;
        }

        Utility.SetCanvasGroupEnabled(canvasGroup, false);
    }

    public void LoadScene (string name)
    {
        StartCoroutine(Coroutine(name));
    }

}
