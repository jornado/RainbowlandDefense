using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // singleton reference to self
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    public GameObject buildEffect;

    // property: can we build? only if a turret is selected
    public bool CanBuild { get { return turretToBuild != null; } }
    // whether player has enough money to build selected turret
    public bool HasEnoughMoney {  get {
            return PlayerStats.Money >= turretToBuild.cost;  } }

    /* Instatiate this BuildManager singleton */
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager??");
            return;
        }
        instance = this;
    }

    /* Build turret on specified node */
    public void BuildTurretOn(Node node)
    {
        // don't build if we don't have enough money
        if (!HasEnoughMoney)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        // remove the cost from the player's total money
        PlayerStats.Money -= turretToBuild.cost;

        // actually build the turret
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab,
            node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        // create a build particle effect
        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built. Money left " + PlayerStats.Money);
    }

    /* Set which turret we should build */
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
