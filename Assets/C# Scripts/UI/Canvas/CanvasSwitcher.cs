using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    public void SwitchCanvas(bool state)
        => _canvas.gameObject.SetActive(state);
}
