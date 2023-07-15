using UnityEngine;

public class DrawHandler : MonoBehaviour
{
    [SerializeField] private WinHandler _winHandler;

    public void ShawDrawCanvas()
    {
        _winHandler.PerformEndGameLogic(true, false, false, "Draw");
    }
}
