using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CrossesZeroesSetter : MonoBehaviour
{
    [SerializeField] private ComponentsLoader _loader;

    public void DrawElementOnField(int index)
    {
        var isEdgeShaded = _loader.Field.Cages.Values.ElementAt(index);

        if (!isEdgeShaded)
        {
            var cage = _loader.Field.Cages.Keys.ElementAt(index);
            cage.Cross.gameObject.SetActive(true);
            isEdgeShaded = true;
        }
    }
}
