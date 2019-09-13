using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUI : MonoBehaviour
{
    public GameObject ui;
    private Node targetNode;
    public Button buyButton;

    // Turn on the UI on specified
    public void SetTarget(Node _target)
    {
        targetNode = _target;
        transform.position = targetNode.GetBuildPosition();
        if (!targetNode.isUpgraded)
        {

        }

        ui.SetActive(true);
    }

    /* Hide the buttons */
    public void Hide()
    {
        ui.SetActive(false);
    }

    /* When user presses button, buy the turret on the node */
    public void Buy()
    {
        targetNode.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

}
