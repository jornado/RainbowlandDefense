using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make public vars visible in inspector
[System.Serializable]
public class TurretBlueprint
{
    // a little fancier than just a prefab
	public GameObject prefab;
	public int cost;

    public GameObject upgradedPrefab;
    public int upgradedCost;

    public int GetSellPrice()
    {
        return cost / 2;
    }
}
