using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CrossesZeroesSetter : MonoBehaviour
{
    [SerializeField] private GameStarter _starter;
    [SerializeField] private GameLogicHandler _logicHandler;
    [SerializeField] private GameField _field;
    [SerializeField] private WinHandler _winHandler;
    [SerializeField] private DrawHandler _drawHandler;

    private bool _isWasWin;
    private bool _isFirstPlayerDoMove = true;

    public bool IsFirstPlayerDoMove => _isFirstPlayerDoMove;

    public void DrawPlayerElementOnField(int index)
    {
        var currentCage = _field.Cages[index];
        if (GameLoader.IsPlayerPlayWithComputer)
        {
            if ((currentCage.IsCrossActive || currentCage.IsZeroActive) || _logicHandler.IsComputerDoNextMove) return;
            DoMoveInGameWithComputer(
                _starter.IsPlayerStarted ? currentCage.Cross : currentCage.Zero, 
                false, 
                true, 
                "Move : Computer");
            StartCoroutine(nameof(DoComputerMove));
        }
        else DoMoveInGameWithoutComputer(_isFirstPlayerDoMove ? currentCage.Cross : currentCage.Zero);

        DoWinCheck();
    }

    private void DoMoveInGameWithComputer(Image figure, bool firstOrder, bool secondOrder, string text)
    {
        DrawFigure(figure);
        _logicHandler.ChangeOrderInGameWithComputer(firstOrder, secondOrder);
        _starter.ChangeMoveText(text);
    }

    private void DoMoveInGameWithoutComputer(Image figure)
    {
        DrawFigure(figure);
        _isFirstPlayerDoMove = !_isFirstPlayerDoMove;
        _starter.ChangeMoveText(_isFirstPlayerDoMove ? "Move : Player 1" : "Move : Player 2");
    }

    private void DrawFigure(Image figure)
    {
        if (!_starter.MoveText.IsActive()) return;
        figure.gameObject.SetActive(true);  
    }

    private void DoWinCheck()
    {
        foreach (var c in _winHandler.WinCases)
        {
            var isPlayerOrComputerWins = _winHandler.CheckWin(c.Item1, c.Item2, c.Item3);
            var isCagesAreFilled = _field.Cages.All(x => x.IsCrossActive || x.IsZeroActive);
            
            if (isPlayerOrComputerWins)
            {
                _isWasWin = true;
                _winHandler.ShowWinCanvas();
                break;
            }
        }

        if (_field.Cages.All(x => x.IsCrossActive || x.IsZeroActive) && _isWasWin == false)
            _drawHandler.ShawDrawCanvas();
    }

    private Cage GetRandomClearCage()
    {
        return _field.Cages
            .Where(x => !(x.IsCrossActive || x.IsZeroActive))
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefault();
    }

    private IEnumerator DoComputerMove()
    {
        yield return new WaitForSeconds(2f);
        var randomCage = GetRandomClearCage();
        DoMoveInGameWithComputer(_starter.IsComputerStarted ? randomCage.Cross : randomCage.Zero, true, false, "Move : Player");
    }

    public IEnumerator DrawComputerFigureOnStart()
    {
        yield return new WaitForSeconds(_starter.StartTime);
        if (_starter.IsComputerStarted) StartCoroutine(nameof(DoComputerMove));
    }
}