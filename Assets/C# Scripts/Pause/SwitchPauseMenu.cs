using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _pause;
    [SerializeField] private Canvas _game;
    [SerializeField] private CanvassesSwitcher _switcher;

    public void OpenPauseMenu()
    {
        _switcher.SwitchCanvases();
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        _switcher.SwitchCanvases();
        Time.timeScale = 1;
    }
}
