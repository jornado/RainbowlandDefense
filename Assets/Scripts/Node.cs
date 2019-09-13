using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    // turret needs to be raised slightly above node
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded;

    private Renderer rend;
    // save the start color
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        // Init variables
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    /* Determine exactly where to build the turret */
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    /* Build a turret */
    private void OnMouseDown()
    {
        // Don't build a turret if it's underneath the icon or if it's null
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Select the node if there's already something on it
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }


        if (!buildManager.CanBuild)
            return;

        // Place the turret
        BuildTurret(buildManager.GetTurretToBuild());
    }

    /* Instantiate a turret on this node */
    void InstantiateTurret(TurretBlueprint blueprint, GameObject prefab)
    {
        GameObject _turret = (GameObject)Instantiate(prefab,
            GetBuildPosition(), Quaternion.identity);
        // set this as our current turret
        turret = _turret;
        turretBlueprint = blueprint;
        // also create a particle effect
        CreateEffect(buildManager.buildEffect);
        Debug.Log("Turret instantiated!");
    }
    
    /* Subtract the cost of the turret from our total, if we have enough */
    bool MaybeSubtractCost(int cost)
    {
        // don't build if we don't have enough money
        if (PlayerStats.Money < cost)
        {
            Debug.Log("Not enough money to build that!");
            return false;
        }

        // remove the cost from the player's total money
        PlayerStats.Money -= cost;

        return true;
    }

    /* Build turret on specified node */
    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (!MaybeSubtractCost(blueprint.cost))
            return;

        // actually build the turret
        InstantiateTurret(blueprint, blueprint.prefab);
    }

    /* Replace the turret on this node with its upgraded version */
    public void UpgradeTurret()
    {
        if (!MaybeSubtractCost(turretBlueprint.upgradedCost))
            return;

        // get rid of previous turret first
        Destroy(turret);
        InstantiateTurret(turretBlueprint, turretBlueprint.upgradedPrefab);
        // now we can upgrade
        isUpgraded = true;
        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellPrice();

        // get rid of turret
        Destroy(turret);
        isUpgraded = false;
        turretBlueprint = null;
        // also create a particle effect
        CreateEffect(buildManager.sellEffect);
        Debug.Log("Turret sold!");
    }

    /* Create a build particle effect */
    void CreateEffect(GameObject effectPrefab)
    {
        GameObject effectIns = (GameObject)Instantiate(effectPrefab, GetBuildPosition(), Quaternion.identity);
        Destroy(effectIns, 5f);
    }

    /* Highlight the node with a color when you mouse over it */
    private void OnMouseEnter()
    {
        // Don't change color if it's underneath the icon or if turret's null
        if (EventSystem.current.IsPointerOverGameObject()
            || !buildManager.CanBuild)
            return;

        if (buildManager.HasEnoughMoney)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    /* Reset to initial color on mouse exit */
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
