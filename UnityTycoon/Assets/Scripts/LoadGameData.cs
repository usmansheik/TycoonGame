using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.UI;

public class LoadGameData : MonoBehaviour {

    public delegate void LoadDataComplete();
    public static event LoadDataComplete OnLoadDataComplete;

    public Text CompanyNameText;
    public TextAsset GameData;
    public GameObject StorePrefab;
    public GameObject StorePanel;

    public GameObject ManagerPrefab;
    public GameObject ManagerPanel;

    private XmlDocument xmlDoc;
    
	// Use this for initialization
	void Start () {
        LoadData();

        if(OnLoadDataComplete!=null)
        {
            OnLoadDataComplete();
        }
                }
	public void LoadData()
    {



         xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(GameData.text);

        //Load GameManagerData
        LoadManagerData();
        //LoadStores
        LoadStores();
      
    }
    public void LoadManagerData()
    {
        ////Load Starting balane
        float StartingBalance = float.Parse(xmlDoc.GetElementsByTagName("StartingBalance")[0].InnerText);
        GameController.Instance.AddToBalance(StartingBalance);
        ////Load Company Name

        string Companyname = xmlDoc.GetElementsByTagName("CompanyName")[0].InnerText;
        CompanyNameText.text = Companyname;
    }
   
    public void LoadStores()
    {
        /////Load Store
        XmlNodeList StoreList = xmlDoc.GetElementsByTagName("store");
        foreach (XmlNode StoreInfo in StoreList)
        {
            LoadStoreChilds(StoreInfo);


        }
    }
    public void LoadStoreChilds(XmlNode StoreInfo)
    {

        GameObject NewStore = (GameObject)Instantiate(StorePrefab);
        store storeObj = NewStore.GetComponent<store>();
        XmlNodeList StoreNodes = StoreInfo.ChildNodes;
        foreach (XmlNode StoreNode in StoreNodes)
        {
            SetStoreObject(StoreNode,storeObj,NewStore);
        }
        NewStore.transform.SetParent(StorePanel.transform);

        
        storeObj.SetNetStoreCost(storeObj.BaseStoreCost);
    }
    public void SetStoreObject(XmlNode StoreNode,store storeObj,GameObject NewStore)
    {

        if (StoreNode.Name == "name")
        {

            Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<Text>();
            StoreText.text = StoreNode.InnerText;
            storeObj.StoreName = StoreNode.InnerText;
        }
        if (StoreNode.Name == "image")
        {
            Sprite newsprite = Resources.Load<Sprite>(StoreNode.InnerText);
            Image StoreImage = NewStore.transform.Find("ImageButtonClick").GetComponent<Image>();
            StoreImage.sprite = newsprite;
        }

        if (StoreNode.Name == "ProfitBalance")
        {
            storeObj.ProfitBalance = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "BaseStoreCost")
        {
            storeObj.BaseStoreCost = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "Timer")
        {
            storeObj.Timer = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreMultiplier")
        {
            storeObj.StoreMultiplier = float.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "StoreTimerDivision")
        {
            storeObj.StoreTimerDivision = int.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name == "storeCount")
        {
            storeObj.storeCount = int.Parse(StoreNode.InnerText);
        }
        if (StoreNode.Name=="ManagerCost")
        {
            CreateManager(StoreNode,storeObj);

        }

    }
    void CreateManager(XmlNode StoreNode,store storeObj)
    {
        GameObject NewManager = (GameObject)Instantiate(ManagerPrefab);
        NewManager.transform.SetParent(ManagerPanel.transform);

        Text ManagerNameText = NewManager.transform.Find("ManagerNameText").GetComponent<Text>();
        ManagerNameText.text = storeObj.StoreName;
        storeObj.ManagerCost= float.Parse(StoreNode.InnerText);
        Button ManagerButton = NewManager.transform.Find("UnlockManagerButton").GetComponent<Button>();

        Text ButtonText = ManagerButton.transform.Find("UnlockManagerButtonText").GetComponent<Text>();


        ButtonText.text = "Unlock" + storeObj.ManagerCost.ToString("C2");
        UIStore UiManager = storeObj.GetComponent<UIStore>();
        UiManager.ManagerButton = ManagerButton;
        ManagerButton.onClick.AddListener(storeObj.UnlockManger);
    }
}
   
	

