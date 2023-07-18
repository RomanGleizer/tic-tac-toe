using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Slider _loadGameSlider;
    [SerializeField] private GameObject _loadGameContent;

    public static bool IsPlayerPlayWithComputer = false;

    public void LoadGame(int id)
    {
        if (gameObject.GetComponent<PlayWithComputerButton>()) IsPlayerPlayWithComputer = true;
        _loadGameContent.SetActive(true);
        LoadScene(id);
    }

    public async void LoadScene(int id)
    {
        var loadingScene = SceneManager.LoadSceneAsync(id);
        loadingScene.allowSceneActivation = false;
        
        while (_loadGameSlider.value < 1f)
        {
            await Task.Delay(500);
            var progress = Mathf.Clamp01(loadingScene.progress / 0.9f);
            _loadGameSlider.value = loadingScene.progress;
            if (progress >= 1f) loadingScene.allowSceneActivation = true;
        }
    }
}
