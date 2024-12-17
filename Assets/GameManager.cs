using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    
    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có một GameManager duy nhất
        }
    }
    #endregion

    public float currentScore = 0f;
    public float highScore = 0f; // Thêm điểm cao
    public bool isPlaying = false;

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime; // Tăng điểm theo thời gian
        }
    }
    public void StartGame(){
        // Bắt đầu chơi khi nhấn phím Space
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0f; // Reset điểm khi bắt đầu lại

    }

    public void GameOver()
    {
        onGameOver.Invoke();
        // Kiểm tra điểm cao
        if (currentScore > highScore)
        {
            highScore = currentScore;
            Debug.Log("New High Score: " + highScore);
        }
        
        currentScore = 0f;
        isPlaying = false;
        Debug.Log("Game Over! Final Score: " + currentScore);
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighScore()
    {
        return Mathf.RoundToInt(highScore).ToString();
    }
}