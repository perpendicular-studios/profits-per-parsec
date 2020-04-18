using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetListScriptableObject<T> : ScriptableObject
{
    [Header("Asset List")]
    public List<T> assetList;
}
