using UnityEngine;
using System.Collections.Generic;

public class GameField : MonoBehaviour 
{
    public readonly Dictionary<Cage, bool> Cages = new Dictionary<Cage, bool>();
}