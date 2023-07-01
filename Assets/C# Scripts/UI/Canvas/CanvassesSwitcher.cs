using UnityEngine;

public class CanvassesSwitcher : MonoBehaviour
{
    [SerializeField] private Canvas[] _canvasses;
    [SerializeField] private bool[] _statuses;

    public void SwitchCanvases()
    {
        for (int i = 0; i < _canvasses.Length; i++)
            _canvasses[i].gameObject.SetActive(_statuses[i]);
    }
}
