using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* This class controls the UI (upgrade/sell buttons) of each node we can place a turret on. */
public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node targetNode;
    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;
    public TextMeshProUGUI sellPrice;

    // Turn on the UI on specified
    public void SetTarget(Node _target)
    {
        targetNode = _target;
        transform.position = targetNode.GetBuildPosition();
        if (!targetNode.isUpgraded)
        {
            upgradeCost.text = "$" + targetNode.turretBlueprint.upgradedCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellPrice.text = "$" + targetNode.turretBlueprint.GetSellPrice();
        ui.SetActive(true);
    }

    /* Hide the upgrade/sell buttons */
    public void Hide()
    {
        ui.SetActive(false);
    }

    /* When user presses Upgrade button, upgrade the turret on the node */
    public void Upgrade()
    {
        targetNode.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        targetNode.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
