using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentsLoader : MonoBehaviour
{
    [SerializeField] private GameField _gameField;
    [SerializeField] private GameStarter _starter;
    [SerializeField] private GameLogicHandler _logicHandler;

    public GameField Field => _gameField;

    public Dictionary<Cage, bool>.KeyCollection Cages => Field.Cages.Keys;

    public Dictionary<Cage, bool>.ValueCollection Statuses => Field.Cages.Values;

    private void Awake() 
    {
        for (int i = 0; i < _gameField.transform.childCount; i++)
        {
            var cage = _gameField.transform.GetChild(i).GetComponent<Cage>();
            _gameField.Cages.Add(cage, false);
        }

        _starter.StartCoroutine(nameof(_starter.SelectGameStarter));
        _logicHandler.StartCoroutine(nameof(_logicHandler.SetAbilityMakeMove));
    }
}