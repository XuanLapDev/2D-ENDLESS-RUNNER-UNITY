using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUi;
    [SerializeField] private GameObject gameOverUI;
    GameManager gm;
    private void Start(){
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverUI);
    }

    public void PlayButtonHandler(){
        gm.StartGame();


    }

    public void ActivateGameOverUI(){
        gameOverUI.SetActive(true);
    }
    private void OnGUI(){
        scoreUI.text = gm.PrettyScore();
    }
}