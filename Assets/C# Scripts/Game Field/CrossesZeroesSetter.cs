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

    private void Update() 
    {
        foreach (var c in _winHandler.WinCases)
            if (_winHandler.CheckWin(c.Item1, c.Item2, c.Item3))
            {
                _winHandler.ShowWinCanvas();
                Time.timeScale = 0;
            }
    }

    public void DrawPlayerElementOnField(int index)
    {
        var currentCage = _field.Cages[index];
        if (currentCage.IsCrossActive || currentCage.IsZeroActive) return;
        DoMove(_starter.IsPlayerStarted ? currentCage.Cross : currentCage.Zero, false, true, "Move : Computer");
        DrawComputerElementOnField();
    }

    public void DrawComputerElementOnField()
    {
        if (_field.Cages.All(x => x.IsCrossActive || x.IsZeroActive)) return;
        StartCoroutine(nameof(DoComputerMove));
    }

    private void DoMove(Image figure, bool firstOrder, bool secondOrder, string text)
    {
        figure.gameObject.SetActive(true);
        _logicHandler.ChangeOrder(firstOrder, secondOrder);
        _starter.ChangeMoveText(text);
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
        DoMove(_starter.IsComputerStarted ? randomCage.Cross : randomCage.Zero, true, false, "Move : Player");
    }

    public IEnumerator DrawComputerFigureOnStart()
    {
        yield return new WaitForSeconds(_starter.StartTime);
        if (_starter.IsComputerStarted) StartCoroutine(nameof(DoComputerMove));
    }
}