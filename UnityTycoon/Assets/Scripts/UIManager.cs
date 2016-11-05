using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text CurrentBalanceText;

    // Use this for initialization
    void Start () {
	}
    void OnEnable()
    {
        GameController.OnUpdateBalance += UpdateUI;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void UpdateUI()
    {
        CurrentBalanceText.text = GameController.Instance.GetCurrentBalance().ToString("C2");

    }
}
