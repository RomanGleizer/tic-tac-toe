using System.Collections;
using UnityEngine;

public class GameLogicHandler : MonoBehaviour
{
    [SerializeField] private GameStarter _starter;
    [SerializeField] private CrossesZeroesSetter _figuresSetter;

    public bool IsPlayerDoNextMove { get; private set; }
    
    public bool IsComputerDoNextMove { get; private set; }

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