using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField]
    private int knifeCount;
    [Header("Knife Spawning")]
    public Vector2 knifeSpawnPosition;
    [SerializeField]
    private GameObject knifeObject;
    [SerializeField]
    private GameObject levelCompleteAnimation;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if(Instance == null)
        { 
        Instance = this;
        }
    }
    private void Start()
    {
        GameUi.Instance.SetInitialKnives(knifeCount);
        
    }
    public void OnKnifeHit()
    {
        if (knifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }
    public void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
    }
    public void StartGameOverSequence(bool win)
    {
        StartCoroutine(SequenceCoroutine(win));
    }
    private IEnumerator SequenceCoroutine(bool win)
    {
        if (win)
        {
            PlayerPrefs.SetInt("StartGame", 0);
            GameUi.Instance.LogMotor.SetActive(false);
            levelCompleteAnimation.SetActive(true);
            yield return new WaitForSeconds(1.3f);
            GameUi.Instance.levelCompletePanel.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("StartGame", 0);
            yield return new WaitForSeconds(2f);
            GameUi.Instance.ShowRestartButton();
             
        }
    }
    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
       
    }
}
