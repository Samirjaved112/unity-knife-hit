using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUi : MonoBehaviour
{
    public static GameUi Instance;

    [SerializeField]
    private GameObject restartButton;

    [Header("Knife count display")]
    [SerializeField]
    private GameObject panelKnives;
    [SerializeField]
    private GameObject iconKnife;
    [SerializeField]
    private Color usedKnifeColor;

    [Header("Game UI Panels")]
    public GameObject levelCompletePanel;
    public GameObject LogMotor;
    public GameObject startPanel;
    

    private void Awake()
    {
        PlayerPrefs.SetInt("IsFirstTime", 0);
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    public void OnStartButton()
    {
        startPanel.SetActive(false);
        GameController.Instance.SpawnKnife();
        LogMotor.SetActive(true);
        PlayerPrefs.SetInt("StartGame", 1);
    }
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
        LogMotor.SetActive(false);
    }
    public void SetInitialKnives(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(iconKnife, panelKnives.transform);
        }
    }
    private int KnifeIndexToChange = 0;
    public void DecrementKnifeCount()
    {
        panelKnives.transform.GetChild(KnifeIndexToChange++)
            .GetComponent<Image>().color = usedKnifeColor;

    }
}
