using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStore : MonoBehaviour {
    public Text BuybuttonText;
    public Slider progressSlider;
    public Text storeCountText;
    public Button BuyButton;
    public store Store;
    public Button ManagerButton;
    // Use this for initialization

    void OnEnable()
    {
        GameController.OnUpdateBalance += UpdateUI;
        LoadGameData.OnLoadDataComplete += UpdateUI;
    }
    public void ManagerUnlock()
    {
        Text ButtonText = ManagerButton.transform.Find("UnlockManagerButtonText").GetComponent<Text>();
        ButtonText.text = "PURCHASED!";
    }
    void Awake()
    {
        Store = transform.GetComponent<store>();
    }
    void Start () {
        storeCountText.text = Store.storeCount.ToString();
        BuybuttonText.text = "Buy " + Store.GetNetStoreCost().ToString("C2");


    }

    // Update is called once per frame
    void Update () {
        progressSlider.value = Store.getCurrentTimer() / Store.GetStoreTimer();
    }
    void UpdateUI()
    {

        CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();

        Debug.Log(Store.GetNetStoreCost());
        if (!Store.StoreUnlock && !GameController.Instance.CanBuy(Store.GetNetStoreCost()))
        {
            cg.interactable = false;
            cg.alpha = 0.2f;
        }
        else
        {
            cg.interactable = true;
            cg.alpha = 1;
            Store.StoreUnlock = true;

            if (GameController.Instance.CanBuy(Store.GetNetStoreCost()))
            {
                BuyButton.interactable = true;
            }
            else
            {
                BuyButton.interactable = false;
            }

            BuybuttonText.text = "Buy " + Store.GetNetStoreCost().ToString("C2");

            //Update Manager Button if store afforded
           

        }
        if (!Store.ManagerUnlock&& GameController.Instance.CanBuy(Store.ManagerCost))
        {
            ManagerButton.interactable = true;
        }
        else
        {
            ManagerButton.interactable = false;
        }

    }
    public void BuyStoreClick()
    {
        if (GameController.Instance.CanBuy(Store.GetNetStoreCost()))
        {
            Store.BuyStore();
            storeCountText.text = Store.storeCount.ToString();
            UpdateUI();

        }
    }
    public void OnStartTimerCliclk()
    {
        Store.OnStartTimer();
    }
}
