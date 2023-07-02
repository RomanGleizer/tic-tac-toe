using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CrossesZeroesSetter : MonoBehaviour
{
    [SerializeField] private ComponentsLoader _loader;
    [SerializeField] private GameStarter _starter;
    [SerializeField] private GameLogicHandler _logicHandler;

    private Image _playerDrawFigure;
    private Image _computerDrawFigure;
    
    public void DrawElementOnField(int index)
    {
        var isEdgeShaded = _loader.Statuses.ElementAt(index);
        var cage = _loader.Cages.ElementAt(index);

        if (!isEdgeShaded && _starter.IsPlayerStarted) SetFigures(cage.Cross, cage.Zero);
        else if (!isEdgeShaded && _starter.IsComputerStarted) SetFigures(cage.Zero, cage.Cross);
        
        if (_logicHandler.IsPlayerDoNextMove) _logicHandler.DoMove(false, true, _playerDrawFigure);
        
        isEdgeShaded = true;
    }

    public void Draw(Image figure) => figure.gameObject.SetActive(true);

    private void SetFigures(Image first, Image second)
    {
        _playerDrawFigure = first;
        _computerDrawFigure = second;
    }
}