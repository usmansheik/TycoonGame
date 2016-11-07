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
    private XmlDocument xmlDoc;
    int x = 200;
    int y = 320;
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

        NewStore.transform.position = new Vector3(x, y, 0);

        y = y - 100;
        storeObj.SetNetStoreCost(storeObj.BaseStoreCost);
    }
    public void SetStoreObject(XmlNode StoreNode,store storeObj,GameObject NewStore)
    {

        if (StoreNode.Name == "name")
        {

            Text StoreText = NewStore.transform.Find("StoreNameText").GetComponent<Text>();
            StoreText.text = StoreNode.InnerText;
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
    }
   
	
}
