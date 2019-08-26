using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    /* Select standard turret for purchase */
    public void SelectStandardTurret()
    {
        Debug.Log("Standard turret selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    /* Select missile launcer for purchase */
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile launcher selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    /* Select laser beamer for purchase */
    public void SelectLaserBeamer()
    {
        Debug.Log("Laser beamer selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
