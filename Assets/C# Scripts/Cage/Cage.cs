using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    [SerializeField] private Image _cross;
    [SerializeField] private Image _zero;

    public int Number => transform.GetSiblingIndex();

    public Image Cross => _cross;

    public Image Zero => _zero;
}