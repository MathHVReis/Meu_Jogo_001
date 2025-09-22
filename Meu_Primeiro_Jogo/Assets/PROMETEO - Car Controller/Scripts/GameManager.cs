using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

    public float gameTime = 0f;
    public int score = 0;

    public TextMeshProUGUI timerText; // Nova variável para o texto do tempo
    public TextMeshProUGUI scoreText; // Nova variável para o texto da pontuação

    private void Start() {
        Time.timeScale = 1;
        EscolherProximaVaga();
    }

    private void Update() {
        if (Time.timeScale != 0) {
            gameTime += Time.deltaTime;

            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            string timeString;

            if (gameTime < 60f) {
                timeString = string.Format("{0:00}.{1:00}",
                                           timeSpan.Seconds,
                                           timeSpan.Milliseconds / 10);

                timerText.text = "   " + timeString;
            }
            else {
                timeString = string.Format("{0:0}:{1:00}.{2:00}",
                                   timeSpan.Minutes,
                                   timeSpan.Seconds,
                                   timeSpan.Milliseconds / 10);

                timerText.text = timeString;
            }
            scoreText.text = "Pontuação: " + score; // Atualiza o texto da pontuação
        }
        // Verifica se o jogo terminou
        if (score >= 12 || gameTime >= 120.0f) {
            Time.timeScale = 0;

            // Pega o tempo final formatado do GameManager
            string tempoFinal = GetTempoFinal();

            if (score >= 12) {
                Debug.Log("Parabéns! Você alcançou o final do jogo! Tempo Final: " + tempoFinal + "s!");
                Debug.Log("Pontuação Final: " + score);
            }
            else {
                Debug.Log("Fim de jogo! Seu tempo acabou! Pontuação Final: " + score + " pontos!");
            }
        }
    }

    // Nova função para obter o tempo final formatado
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
        GameObject[] vagasDisponiveis = GameObject.FindGameObjectsWithTag("Collider_Vaga");

        if (vagasDisponiveis.Length > 0) {
            int indiceAleatorio = UnityEngine.Random.Range(0, vagasDisponiveis.Length);
            GameObject novaVagaAlvo = vagasDisponiveis[indiceAleatorio];

            novaVagaAlvo.tag = "Collider_Destaque";

            Debug.Log("Nova vaga de destaque: " + novaVagaAlvo.name);
        }
        else {
            Debug.LogWarning("Não há mais vagas disponíveis para serem o alvo!");
        }
    }
}