using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDateDetails", menuName = "OL_DS_2D/Game Date Details", order = 0)]
public class GameDateDetails : ScriptableObject
{
    [field: SerializeField] public int startingYear { get; private set; }
    [field: SerializeField, Range(1, 12)] public int startingMonth { get; private set; }
    [field: SerializeField, Range(1, 31)] public int startingDay { get; private set; }
    [field: SerializeField] public int endYear { get; private set; }
    [field: SerializeField, Range(1, 12)] public int endMonth { get; private set; }
    [field: SerializeField, Range(1, 31)] public int endDay { get; private set; }
}
