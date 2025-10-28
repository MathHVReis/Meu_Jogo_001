using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {
    public float gameTime = 0f;
    public int score = 0;

    //Elementos PlayerPrefs
    public GameObject saveNamePanel;
    /*
    public TMP_InputField inputNomePlayer;
    */

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;

    public List<GameObject> vagasDisponiveisList;
    public GameObject[] vagaDisponivel;

    // Elementos da tela de Fim de Jogo
    public GameObject endGamePanel;
    public TextMeshProUGUI finalTitleText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    private void Start() {
        Time.timeScale = 0;
        saveNamePanel.SetActive(true);
        endGamePanel.SetActive(false); // Garante que a tela de game over esteja invisível
    }

    private void Update() {
        if (Time.timeScale != 0) {
            gameTime += Time.deltaTime;

            // Lógica de interpolação de cor em duas etapas
            if (gameTime >= 60f && gameTime < 90f) {
                // Etapa 1: Mistura do branco para o amarelo (de 1:00 a 1:30)
                float lerpFactor = (gameTime - 60f) / 30f;
                timerText.color = Color.Lerp(Color.white, Color.yellow, lerpFactor);
            }
            else if (gameTime >= 90f && gameTime < 120f) {
                // Etapa 2: Mistura do amarelo para o vermelho (de 1:30 a 2:00)
                float lerpFactor = (gameTime - 90f) / 30f;
                timerText.color = Color.Lerp(Color.yellow, Color.red, lerpFactor);
            }
            else if (gameTime >= 120f) {
                // Garante que a cor final seja vermelha
                timerText.color = Color.red;
            }
            else {
                // Cor inicial do cronômetro (abaixo de 1:00)
                timerText.color = Color.white;
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            string timeString;

            if (gameTime < 60f) {
                timeString = string.Format("{0:00}.{1:00}",
                              timeSpan.Seconds,
                              timeSpan.Milliseconds / 10);
                timerText.text = "   " + timeString;
            }
            else {
                timeString = string.Format("{0:0}:{1:00}.{2:00}",
                              timeSpan.Minutes,
                              timeSpan.Seconds,
                              timeSpan.Milliseconds / 10);
                timerText.text = timeString;
            }
            scoreText.text = "Pontuação: " + score;
        }

        if (score >= 12 || gameTime >= 120.0f) {
            Debug.Log("Jogo terminado!");
            EndGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 0;
        saveNamePanel.SetActive(false);
        vagasDisponiveisList = new List<GameObject>(vagaDisponivel);
        StartCoroutine(CountdownToStart());
    }

    System.Collections.IEnumerator CountdownToStart() {
        Time.timeScale = 0;
        countdownText.gameObject.SetActive(true);

        countdownText.color = Color.red;
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1);

        countdownText.color = new Color32(255, 165, 0, 255);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);

        countdownText.color = Color.yellow;
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);

        countdownText.color = Color.green;
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1);

        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1;
        EscolherProximaVaga();
    }

    public string GetTempoFinal() {
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        if (gameTime < 60f) {
            return string.Format("{0:00}.{1:00}",
                        timeSpan.Seconds,
                        timeSpan.Milliseconds / 10);
        }
        else {
            return string.Format("{0:0}:{1:00}.{2:00}",
                        timeSpan.Minutes,
                        timeSpan.Seconds,
                        timeSpan.Milliseconds / 10);
        }
    }

    public void EscolherProximaVaga() {
        GameObject[] vagasDestaqueAtuais = GameObject.FindGameObjectsWithTag("Vaga_Destaque");
        foreach (GameObject vaga in vagasDestaqueAtuais) {
            Transform[] allTransforms = vaga.GetComponentsInChildren<Transform>(true);
            foreach (Transform childTransform in allTransforms) {
                childTransform.gameObject.tag = "Vaga_Vazia";
            }
        }

        if (vagasDisponiveisList.Count > 0) {
            int indiceAleatorio = UnityEngine.Random.Range(0, vagasDisponiveisList.Count);
            GameObject novaVagaAlvo = vagasDisponiveisList[indiceAleatorio];

            novaVagaAlvo.SetActive(true);
            Transform[] allTransformsNew = novaVagaAlvo.GetComponentsInChildren<Transform>(true);
            foreach (Transform childTransform in allTransformsNew) {
                childTransform.gameObject.tag = "Vaga_Destaque";
            }

            vagasDisponiveisList.RemoveAt(indiceAleatorio);

            Debug.Log("Nova vaga de destaque: " + novaVagaAlvo.name);
        }
        else {
            Debug.LogWarning("Não há mais vagas disponíveis para serem o alvo!");
        }
    }

    // Nova função que exibe a tela de Fim de Jogo
    private void EndGame() {
        Time.timeScale = 0;
        endGamePanel.SetActive(true); // Exibe o painel
        string tempoFinal = GetTempoFinal();

        // Desabilita os textos de pontuação e tempo
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        if (score >= 12) {
            finalTitleText.text = "Parabéns!";
            Debug.Log("Parabéns! Você alcançou o final do jogo! Tempo Final: " + tempoFinal + "s!");
            Debug.Log("Pontuação Final: " + score);
        }
        else {
            finalTitleText.text = "Fim de Jogo!";
            Debug.Log("Fim de jogo! Seu tempo acabou! Pontuação Final: " + score + " pontos!");
        }

        // Atualiza os textos da tela final com a pontuação e o tempo
        finalScoreText.text = "Pontuação Final: " + score;
        finalTimeText.text = "Tempo Final: " + tempoFinal + "s";
    }

    // Método para voltar ao menu (utilizando botão)
    public void GoToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}