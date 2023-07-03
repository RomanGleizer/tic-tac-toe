using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CrossesZeroesSetter : MonoBehaviour
{
    [SerializeField] private GameStarter _starter;
    [SerializeField] private GameLogicHandler _logicHandler;
    [SerializeField] private GameField _field;

    private Image _playerDrawFigure;
    private Image _computerDrawFigure;

    public void DrawPlayerElementOnField(int index)
    {
        if (_field.Cages[index].Cross.IsActive() || _field.Cages[index].Zero.IsActive()) return;
        
        var rndCages = new List<Cage>();

        InitializePlayerGameField(index);
        _logicHandler.DoMove(false, true, _playerDrawFigure);
        _starter.ChangeMoveText("Move : Computer");

        DrawComputerElementOnField(); // Косячок туть
    }

    public void DrawComputerElementOnField()
    {
        InitializeComputerGameField();
        StartCoroutine(nameof(DoComputerMove));
    }

    private void InitializePlayerGameField(int index)
    {
        var cage = _field.Cages.ElementAt(index);

        if (_starter.IsPlayerStarted) SetFiguresForGame(cage.Cross, cage.Zero);
        else if (_starter.IsComputerStarted) SetFiguresForGame(cage.Zero, cage.Cross);
    }

    private void InitializeComputerGameField()
    {
        var rndCages = new List<Cage>();
        
        foreach (var rndCage in _field.Cages)
        {
            if (rndCage.Cross.IsActive() || rndCage.Zero.IsActive()) continue;
            rndCages.Add(rndCage);
        }
        
        var cage = rndCages[Random.Range(0, rndCages.Count)];

        if (_starter.IsPlayerStarted) SetFiguresForGame(cage.Cross, cage.Zero);
        else if (_starter.IsComputerStarted) SetFiguresForGame(cage.Zero, cage.Cross);
    }

    public void Draw(Image figure) => figure.gameObject.SetActive(true);

    private void SetFiguresForGame(Image first, Image second)
    {
        _playerDrawFigure = first;
        _computerDrawFigure = second;
    }

    private IEnumerator DoComputerMove()
    {
        yield return new WaitForSeconds(2f);
        _logicHandler.DoMove(true, false, _computerDrawFigure);
        _starter.ChangeMoveText("Move : Player");
    }

    public IEnumerator DrawComputerFigureOnStart()
    {
        yield return new WaitForSeconds(_starter.StartTime);
        if (_starter.IsComputerStarted)
        {
            InitializeComputerGameField();
            StartCoroutine(nameof(DoComputerMove));
        }
    }
}