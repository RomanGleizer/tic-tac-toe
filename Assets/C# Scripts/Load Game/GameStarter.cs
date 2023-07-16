using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Animator _controller;
    [SerializeField] private TextMeshProUGUI _playerText;
    [SerializeField] private TextMeshProUGUI _computerText;
    [SerializeField] private TextMeshProUGUI _moveText;
    [SerializeField] private Button _pauseButton;

    public bool IsPlayerStarted { get; private set; }

    public bool IsComputerStarted { get; private set; }

    public float StartTime { get; private set; }

    public TextMeshProUGUI MoveText => _moveText;

    public IEnumerator SelectGameStarter()
    {
        StartTime = Random.Range(5f, 11f);
        yield return new WaitForSeconds(StartTime);

        IsPlayerStarted = _playerText.IsActive();
        IsComputerStarted = _computerText.IsActive();
        _controller.enabled = false;
        _pauseButton.gameObject.SetActive(true);

        if (IsPlayerStarted) _moveText.text = "Move : Player";
        else _moveText.text = "Move : Computer";

        ChangeTextesStatuses(
            new List<(TextMeshProUGUI, bool)> { (_playerText, false), (_computerText, false), (_moveText, true), }
        );
    }

    private void ChangeTextesStatuses(IEnumerable<(TextMeshProUGUI, bool)> textes)
    {
        foreach (var elem in textes)
            elem.Item1.gameObject.SetActive(elem.Item2);
    }

    public void ChangeMoveText(string newText) => _moveText.text = newText;
}