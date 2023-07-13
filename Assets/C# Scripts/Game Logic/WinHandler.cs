using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class WinHandler : MonoBehaviour
{
    [SerializeField] private GameField _field;
    [SerializeField] private GameLogicHandler _logicHandler;
    [SerializeField] private Canvas _gameCanvas;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private GameStarter _starter;
    
    private Func<int, int, int, bool> _winConditionWithCrosses;
    private Func<int, int, int, bool> _winConditionWithZeroes;

    public List<(int, int, int)> WinCases { get; private set; }

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
        PerformEndGameLogic(true, false, false, _logicHandler.IsPlayerDoNextMove ? "Computer Wins" : "Player Wins");
    }

    public void PerformEndGameLogic(bool first, bool second, bool third, string text)
    {
        _winCanvas.gameObject.SetActive(first);
        _gameCanvas.gameObject.SetActive(second);
        _starter.MoveText.gameObject.SetActive(third);
        _winnerText.text = text;
    }

    public bool CheckWin(int first, int second, int third)
        => _winConditionWithCrosses(first, second, third) || _winConditionWithZeroes(first, second, third);
}
