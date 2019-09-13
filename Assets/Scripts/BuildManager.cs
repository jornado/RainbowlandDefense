using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // singleton reference to self
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;

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

    public void SelectNode(Node node)
    {
        if (node == selectedNode)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void SelectNodeForUpgrade(Node node)
    {
        if (node == selectedNode)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    /* Set which turret we should build */
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
