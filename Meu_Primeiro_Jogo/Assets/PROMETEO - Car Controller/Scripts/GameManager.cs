using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float gameTime = 0f;
    public int score = 0;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public List<GameObject> vagasDisponiveisList;

    // Este array serve apenas para você arrastar os objetos no Inspector
    public GameObject[] vagaDisponivel;

    private void Start()
    {
        // Inicializa a lista com os objetos do array no Start()
        vagasDisponiveisList = new List<GameObject>(vagaDisponivel);

        Time.timeScale = 1;
        EscolherProximaVaga();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            gameTime += Time.deltaTime;

            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            string timeString;

            if (gameTime < 60f)
            {
                timeString = string.Format("{0:00}.{1:00}",
                                            timeSpan.Seconds,
                                            timeSpan.Milliseconds / 10);
                timerText.text = "   " + timeString;
            }
            else
            {
                timeString = string.Format("{0:0}:{1:00}.{2:00}",
                                            timeSpan.Minutes,
                                            timeSpan.Seconds,
                                            timeSpan.Milliseconds / 10);
                timerText.text = timeString;
            }
            scoreText.text = "Pontuação: " + score;
        }

        if (score >= 12 || gameTime >= 120.0f)
        {
            Time.timeScale = 0;
            string tempoFinal = GetTempoFinal();

            if (score >= 12)
            {
                Debug.Log("Parabéns! Você alcançou o final do jogo! Tempo Final: " + tempoFinal + "s!");
                Debug.Log("Pontuação Final: " + score);
            }
            else
            {
                Debug.Log("Fim de jogo! Seu tempo acabou! Pontuação Final: " + score + " pontos!");
            }
        }
    }

    public string GetTempoFinal()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        if (gameTime < 60f)
        {
            return string.Format("{0:00}.{1:00}",
                                  timeSpan.Seconds,
                                  timeSpan.Milliseconds / 10);
        }
        else
        {
            return string.Format("{0:0}:{1:00}.{2:00}",
                                  timeSpan.Minutes,
                                  timeSpan.Seconds,
                                  timeSpan.Milliseconds / 10);
        }
    }

    public void EscolherProximaVaga()
    {
        // Limpa as tags das vagas de destaque anteriores
        GameObject[] vagasDestaqueAtuais = GameObject.FindGameObjectsWithTag("Vaga_Destaque");
        foreach (GameObject vaga in vagasDestaqueAtuais)
        {
            Transform[] allTransforms = vaga.GetComponentsInChildren<Transform>(true);
            foreach (Transform childTransform in allTransforms)
            {
                childTransform.gameObject.tag = "Vaga_Vazia";
            }
        }

        // Verifica se ainda há vagas disponíveis
        if (vagasDisponiveisList.Count > 0)
        {
            // Sorteia uma nova vaga da lista
            int indiceAleatorio = UnityEngine.Random.Range(0, vagasDisponiveisList.Count);
            GameObject novaVagaAlvo = vagasDisponiveisList[indiceAleatorio];

            // Ativa e muda a tag da nova vaga e seus filhos
            novaVagaAlvo.SetActive(true);
            Transform[] allTransformsNew = novaVagaAlvo.GetComponentsInChildren<Transform>(true);
            foreach (Transform childTransform in allTransformsNew)
            {
                childTransform.gameObject.tag = "Vaga_Destaque";
            }

            // Remove a vaga sorteada da lista para que não seja sorteada novamente
            vagasDisponiveisList.RemoveAt(indiceAleatorio);

            Debug.Log("Nova vaga de destaque: " + novaVagaAlvo.name);
        }
        else
        {
            Debug.LogWarning("Não há mais vagas disponíveis para serem o alvo!");
        }
    }
}