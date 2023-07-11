using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    public void RestartGame()
        => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
