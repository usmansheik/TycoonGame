using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text CurrentBalanceText;
    public GameObject MangerPanel;
    public enum State{
        Main,Mangers
    }
    public State CurrentState;
    // Use this for initialization
    void Start () {
        CurrentState = State.Main;
	}
    void OnEnable()
    {
        GameController.OnUpdateBalance += UpdateUI;
    }

   public void onClickManger()
    {
        if (CurrentState == State.Main)
        {
            onShowManger();
            MangerPanel.SetActive(true);
        }
        else
        {
            onShowMain();
            MangerPanel.SetActive(false);
        }
    }
	void onShowManger()
    {
        CurrentState = State.Mangers;


    }
    void onShowMain()
    {
        CurrentState = State.Main;


    }
    // Update is called once per frame
    void Update () {
	
	}
    
    public void UpdateUI()
    {
        CurrentBalanceText.text = GameController.Instance.GetCurrentBalance().ToString("C2");

    }
}
