using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class store : MonoBehaviour {
   public int storeCount;
    public string StoreName;
    public float BaseStoreCost;
    public float ProfitBalance;
    public float Timer = 3;
    public bool ManagerUnlock;
    public float StoreMultiplier;
    public int StoreTimerDivision;
    public bool StoreUnlock;
    private float NetStoreCost;
    private float CurrentTimer = 0;
    private bool StartTimer;
   public float ManagerCost;
    public float ManagerName;
    // Use this for initialization
   

    void Start () {
        
        NetStoreCost = BaseStoreCost;
        StartTimer = false;
	}
    public void SetNetStoreCost(float amt)
    {
         NetStoreCost=amt;
    }
    public float GetNetStoreCost()
    {
        return NetStoreCost;
    }
	public float getCurrentTimer()
    {
        return CurrentTimer;
    }
    public float GetStoreTimer()
    {
        return Timer;
    }
	// Update is called once per frame
	void Update () {
        if (StartTimer)
        {
            Debug.Log("StarterTimer");

            CurrentTimer += Time.deltaTime;
            if (CurrentTimer > Timer)
            {
                Debug.Log("Timer");
                if(ManagerUnlock==false)
                StartTimer = false;
                CurrentTimer = 0f;
             GameController.Instance.AddToBalance(ProfitBalance * storeCount);
            }
        }
	}
   
    public void BuyStore()
    {
        //  if (CurrentBalance >= BaseStoreCost)
        // {
      
            storeCount = storeCount + 1;
            Debug.Log(storeCount);
            GameController.Instance.AddToBalance(-NetStoreCost);

            NetStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier,storeCount));

            Debug.Log(NetStoreCost);
            if (storeCount % StoreTimerDivision == 0)
            {
                Timer = Timer / 2;
            }
        
        //  }
    }
     
    public void UnlockManger()
    {
        if (ManagerUnlock)
            return;
        if (GameController.Instance.CanBuy(ManagerCost))
        {
            GameController.Instance.AddToBalance(-ManagerCost);
            ManagerUnlock = true;
            this.transform.GetComponent<UIStore>().ManagerUnlock();

        }
    }
    public void OnStartTimer()
    {
        Debug.Log("Store On Click");
        if (StartTimer == false && storeCount>0)
        {
            StartTimer = true;
        }
    }
}
