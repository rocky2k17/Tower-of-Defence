
using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurrentBluePrint standardTurrent;
    public TurrentBluePrint missileLauncher;
    public TurrentBluePrint laserBeamer;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurrent()
    {
        Debug.Log("Standard Turrent Selected");
        buildManager.SelectTurrentToBuild(standardTurrent);
    }
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildManager.SelectTurrentToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        buildManager.SelectTurrentToBuild(laserBeamer);
    }
}
