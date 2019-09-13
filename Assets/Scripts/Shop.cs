using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint bananaTurret;
    public TurretBlueprint unicornBeamer;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    /* Select banana for purchase */
    public void SelectBananaTurret()
    {
        Debug.Log("Banana selected");
        buildManager.SelectTurretToBuild(bananaTurret);
    }

    /* Select banana for purchase */
    public void SelectUnicornBeamer()
    {
        Debug.Log("Unicorn selected");
        buildManager.SelectTurretToBuild(unicornBeamer);
    }
}
