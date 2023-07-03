using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentsLoader : MonoBehaviour
{
    [SerializeField] private GameField _gameField;
    [SerializeField] private GameStarter _starter;
    [SerializeField] private GameLogicHandler _logicHandler;
    [SerializeField] private CrossesZeroesSetter _drawer;

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
        StartCoroutine(nameof(DrawComputerFigure));
    }

    private IEnumerator DrawComputerFigure()
    {
        yield return new WaitForSeconds(_starter.StartTime);
        if (_starter.IsComputerStarted) 
        {   
            yield return new WaitForSeconds(3f);
            _drawer.DrawConputerElementOnField(Random.Range(0, Cages.Count));
        }   
    }
}