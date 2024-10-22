using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Arraste o painel de Game Over no Inspector
    public TMP_Text gameOverText;
    public GameObject lifeCanvas;
    public GameObject level;
    private IGameStatus _gameStatus;

    private void Awake()
    {
        ServiceLocator.Reset();

        _gameStatus = new GameStatus();
        ServiceLocator.RegisterService(_gameStatus);
        ServiceLocator.RegisterService<ICheckPointSystem>(new CheckPointSystem());
    }

    void Start()
    {
        _gameStatus.OnWinGame += HandleWinGame;
        _gameStatus.OnGameOver += HandleGameOver;
        gameOverPanel.SetActive(false);
    }

    private void HandleWinGame()
    {
         SetGameOverState("Você Ganhou!");
    }
    
    private void HandleGameOver()
    {
        SetGameOverState("Você Perdeu!");
    }

    private void Update()
    {
        // Verifica se o jogo está em estado de Game Over e se a tecla R é pressionada
        if (gameOverPanel.activeSelf)
        {
            Debug.Log("Game Over está ativo.");
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Tecla R pressionada.");
                RestartGame();
            }
        }
    }


     private void SetGameOverState(string message)
    {
        lifeCanvas.SetActive(false); // Oculta a tela de vidas
        level.SetActive(false); // Pausa ou oculta o nível
        gameOverPanel.SetActive(true); // Exibe a tela de Game Over
        gameOverText.text = message;
        Time.timeScale = 0f; // Pausa o jogo

        Update();
    }

    private void RestartGame(){
        Debug.Log("Reiniciando o jogo...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        gameOverPanel.SetActive(false);
    }
}