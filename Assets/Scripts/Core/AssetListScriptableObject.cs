using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetListScriptableObject<T> : ScriptableObject
{
    [Header("List")]
    public List<T> assetList;
}
