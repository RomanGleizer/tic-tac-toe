using UnityEngine;

public class DrawHandler : MonoBehaviour
{
    [SerializeField] private WinHandler _winHandler;

    public void ShawDrawCanvas()
    {
        _winHandler.PerformEndGameLogic("Draw");
        _winHandler.PlayClip(_winHandler.WinClip);
    }
}
