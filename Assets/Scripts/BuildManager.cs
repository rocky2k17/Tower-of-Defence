
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Buildmanager in scene! ");
            return;
        }
        instance = this;
    }

    

    public GameObject buildeffect;

    private TurrentBluePrint turrentToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return turrentToBuild != null; } }
    public bool HasMoney { get { return PlayerStat.Money >= turrentToBuild.cost; } }



    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turrentToBuild = null;


        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurrentToBuild(TurrentBluePrint turrent)
    {
        turrentToBuild = turrent;
        DeselectNode();

    }

    public TurrentBluePrint GetTurrentToBuild()
    {
        return turrentToBuild;
    }
}
