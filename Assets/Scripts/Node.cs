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

    [Header("Optional")]
    public GameObject turret;

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

    /* determine exactly where to build the turret */
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    /* Build a turret */
    private void OnMouseDown()
    {
        // Don't build a turret if it's underneath the icon or if it's null
        if (EventSystem.current.IsPointerOverGameObject()
            || !buildManager.CanBuild)
            return;

        // Can't build on already occupied node
        if (turret != null)
        {
            Debug.Log("Can't build here! - TODO: Display onscreen.");
            return;
        }

        buildManager.BuildTurretOn(this);
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
