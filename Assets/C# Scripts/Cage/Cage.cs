using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    public int Number => transform.GetSiblingIndex();

    public Image Cross => transform.GetChild(0).GetComponent<Image>();

    public Image Zero => transform.GetChild(1).GetComponent<Image>();

    public bool IsCrossActive => Cross.IsActive();

    public bool IsZeroActive => Zero.IsActive();
}