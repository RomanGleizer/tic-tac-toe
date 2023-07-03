using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicHandler : MonoBehaviour
{
    [SerializeField] private GameStarter _starter;
    [SerializeField] private CrossesZeroesSetter _figuresSetter;

    public bool IsPlayerDoNextMove { get; private set; }
    
    public bool IsComputerDoNextMove { get; private set; }

    public void DoMove(bool first, bool second, Image figure)
    {
        _figuresSetter.Draw(figure);
        ChangeOrder(first, second);
    }

    public void ChangeOrder(bool first, bool second)
    {
        IsPlayerDoNextMove = first;
        IsComputerDoNextMove = second;
    }

    public IEnumerator SetAbilityStartGame()
    {
        yield return new WaitForSeconds(_starter.StartTime);

        if (_starter.IsPlayerStarted) IsPlayerDoNextMove = true;
        else IsComputerDoNextMove = true;
    }
}