using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private GameField _field;
    [SerializeField] private GameLogicHandler _logicHandler;
    [SerializeField] private CrossesZeroesSetter _drawer;
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private GameStarter _starter;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;
    [SerializeField] private SoundSwitcher _soundSwitcher;
    
    private Func<int, int, int, bool> _winConditionWithCrosses;
    private Func<int, int, int, bool> _winConditionWithZeroes;

    public List<(int, int, int)> WinCases { get; private set; }
    public AudioClip WinClip => _winClip;

    public void InitializeWinCases()
    {
        _winConditionWithCrosses = (x, y, z) 
            => _field.Cages[x].IsCrossActive && _field.Cages[y].IsCrossActive && _field.Cages[z].IsCrossActive;

        _winConditionWithZeroes = (x, y, z) 
            => _field.Cages[x].IsZeroActive && _field.Cages[y].IsZeroActive && _field.Cages[z].IsZeroActive;
        
        WinCases = new List<(int, int, int)>()
        {
            ( 0, 1, 2 ),
            ( 3, 4, 5 ),
            ( 6, 7, 8 ),
            ( 0, 3, 6 ),
            ( 1, 4, 7 ),
            ( 2, 5, 8 ),
            ( 0, 4, 8 ),
            ( 2, 4, 6 ),
        };
    }

    public void ShowWinCanvas()
    {
        if (GameLoader.IsPlayerPlayWithComputer)
            PerformEndGameLogic(_logicHandler.IsPlayerDoNextMove ? "Computer Wins" : "Player Wins");
        else PerformEndGameLogic(_drawer.IsFirstPlayerDoMove ? "Player 2 Wins" : "Player 1 Wins");

        if (_logicHandler.IsPlayerDoNextMove && GameLoader.IsPlayerPlayWithComputer) PlayClip(_loseClip);
        else PlayClip(_winClip);
    }

    public void PlayClip(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.volume = 1;
        _audio.PlayOneShot(clip);
        StartCoroutine(nameof(ReturnOldVolumeValue));
    }

    public void PerformEndGameLogic(string text)
    {   
        _winCanvas.gameObject.SetActive(true);
        _gameCanvas.gameObject.SetActive(false);
        _starter.MoveText.gameObject.SetActive(false);
        _winnerText.text = text;
    }
    public bool CheckWin(int first, int second, int third)
        => _winConditionWithCrosses(first, second, third) || _winConditionWithZeroes(first, second, third);

    public IEnumerator ReturnOldVolumeValue()
    {
        yield return new WaitForSeconds(2f);
        _audio.volume = _soundSwitcher.CurrentVolume;
    }
}
