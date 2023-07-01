using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsLoader : MonoBehaviour
{
    [SerializeField] private GameField _gameField;

    public GameField Field => _gameField;

    private void Awake() 
    {
        for (int i = 0; i < _gameField.transform.childCount; i++)
        {
            var cage = _gameField.transform.GetChild(i).GetComponent<Cage>();
            _gameField.Cages.Add(cage, false);
        }
    }
}