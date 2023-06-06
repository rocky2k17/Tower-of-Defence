 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;

    public Vector3 offSetPossition;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turrent;

    [HideInInspector]
    public TurrentBluePrint turrentBluePrint;

    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;
    BuildManager buildManager;

   void Start()
    {
        
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance; 
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + offSetPossition; 
    } 

     void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turrent != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurrent(buildManager.GetTurrentToBuild());


    }
    void BuildTurrent(TurrentBluePrint blueprint)
    {
        if (blueprint == null || blueprint.prefab == null) return;

        if (PlayerStat.Money < blueprint.cost)
        {
            Debug.Log("Not Enough Money!!");
            return;
        }

        PlayerStat.Money -= blueprint.cost;
        GameObject _turrent = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turrent = _turrent;

        turrentBluePrint = blueprint;

        GameObject effect = Instantiate(buildManager.buildeffect,GetBuildPosition(), Quaternion.identity);

        Destroy(effect, 5f);

        

        Debug.Log("Turrent Build! ");
    }

    public void UpgradeTurrent()
    {
        if (PlayerStat.Money < turrentBluePrint.upgradeCost)
        {
            Debug.Log("Not Enough Money To Upgrade!!");
            return;
        }

        PlayerStat.Money -= turrentBluePrint.upgradeCost;


        //Get rid of old turrent
        Destroy(turrent);
        //if (turrent != null) Destroy(turrent);
        GameObject _turrent = (GameObject)Instantiate(turrentBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turrent = _turrent;


        //Build a new One
        GameObject effect = Instantiate(buildManager.buildeffect, GetBuildPosition(), Quaternion.identity);

        Destroy(effect, 5f);
        isUpgraded = true;
        Debug.Log("Turrent Upgraded! ");
    }

    void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if(!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

  void OnMouseExit()
    {


        rend.material.color = startColor; 
    }
}
