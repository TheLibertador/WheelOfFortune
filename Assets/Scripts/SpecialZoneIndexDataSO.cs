using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewIndexData", menuName = "ScriptableObjects/SpecialZoneIndexDataScriptableObject", order = 1)]
public class SpecialZoneIndexDataSO : ScriptableObject
{
    public int _silverZoneIndex;
    public int _goldZoneIndex;
}
