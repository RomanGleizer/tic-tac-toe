using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Slider _loadGameSlider;
    [SerializeField] private GameObject _loadGameContent;

    public void LoadGame(int id)
    {
        _loadGameContent.SetActive(true);
        StartCoroutine(LoadScene(id));
    }

    public IEnumerator LoadScene(int id)
    {
        var loadingScene = SceneManager.LoadSceneAsync(id);
        loadingScene.allowSceneActivation = false;

        while (!loadingScene.isDone)
        {
            float progress = Mathf.Clamp01(loadingScene.progress / 0.9f);
            _loadGameSlider.value = loadingScene.progress;
            if (progress >= 1f) loadingScene.allowSceneActivation = true;
            yield return null;
        }
    }
}
