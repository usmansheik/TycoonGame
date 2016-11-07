using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;

    private float CurrentBalance=0;
    public static GameController Instance;
    // Use this for initialization
    void Start () {
       
        if (OnUpdateBalance != null)
        {
            OnUpdateBalance();
        }
    }
	
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
    public void AddToBalance(float amount)
    {
        CurrentBalance = CurrentBalance + amount;
        if (OnUpdateBalance != null)
        {
            OnUpdateBalance();
        }
      //  UIManager.Instance.UpdateUI();

    }
    public bool CanBuy(float amount)
    {
        if (amount > CurrentBalance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public float GetCurrentBalance()
    {
        return CurrentBalance;
    }
}
