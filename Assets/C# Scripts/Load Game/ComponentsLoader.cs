using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentsLoader : MonoBehaviour
{
    [SerializeField] private GameField _gameField;
    [SerializeField] private GameStarter _starter;
    [SerializeField] private GameLogicHandler _logicHandler;
    [SerializeField] private CrossesZeroesSetter _drawer;
    [SerializeField] private WinHandler _winHandler;

    private void Awake() 
    {
        for (int i = 0; i < _gameField.transform.childCount; i++)
        {
            var cage = _gameField.transform.GetChild(i).GetComponent<Cage>();
            _gameField.Cages.Add(cage);
        }

        _starter.StartCoroutine(nameof(_starter.SelectGameStarter));
        _logicHandler.StartCoroutine(nameof(_logicHandler.SetAbilityStartGame));
        _drawer.StartCoroutine(nameof(_drawer.DrawComputerFigureOnStart));
        _winHandler.InitializeWinCases();
    }
}