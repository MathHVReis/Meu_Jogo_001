using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GM_Desafio : MonoBehaviour
{
    public float gameTime = 0f;
    public int score = 0;

    // Elementos PlayerPrefs e Controles
    public GameObject playerPrefsPanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;

    // Missões
    public List<GameObject> missoesInicioList;
    public List<GameObject> missoesFimList;
    public GameObject[] missaoInicioDisponivel;
    public GameObject[] missaoFimDisponivel;

    // Constante para a chave do PlayerPrefs e limite do ranking
    private const string RankingKey = "GameRankingData";
    private const int MaxRankingEntries = 3; // Limite para o Top 3
    private const string PlayerNameKey = "PlayerName";

    // Elementos da tela de Fim de Jogo
    public GameObject endGamePanel;
    public TextMeshProUGUI finalTitleText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    // Elementos ADICIONAIS para o Ranking
    [Header("Ranking UI Elements")]
    public TextMeshProUGUI rankingTextTop1;
    public TextMeshProUGUI rankingTextTop2;
    public TextMeshProUGUI rankingTextTop3;

    private void Start()
    {
        // Pausa o jogo no início para mostrar a tela de PlayerPrefs
        Time.timeScale = 0;
        playerPrefsPanel.SetActive(true);
        endGamePanel.SetActive(false); // Garante que a tela de game over esteja invisível
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            gameTime += Time.deltaTime;

            // --- Lógica de Interpolação de Cor do Timer ---
            if (gameTime >= 120f && gameTime < 150f)
            {
                float lerpFactor = (gameTime - 120f) / 30f;
                timerText.color = Color.Lerp(Color.white, Color.yellow, lerpFactor);
            }
            else if (gameTime >= 150f && gameTime < 180f)
            {
                float lerpFactor = (gameTime - 150f) / 30f;
                timerText.color = Color.Lerp(Color.yellow, Color.red, lerpFactor);
            }
            else if (gameTime >= 180f)
            {
                timerText.color = Color.red;
            }
            else
            {
                timerText.color = Color.white;
            }

            // --- Lógica de Formatação do Tempo ---
            TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
            string timeString;

            if (gameTime < 60f)
            {
                timeString = string.Format("{0:00}.{1:00}", timeSpan.Seconds, timeSpan.Milliseconds / 10);
                timerText.text = "   " + timeString;
            }
            else
            {
                timeString = string.Format("{0:0}:{1:00}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
                timerText.text = timeString;
            }
            scoreText.text = "Pontuação: " + score;
        }
        
        // --- Condição de Fim de Jogo ---
        // Garante que o EndGame só seja chamado uma vez
        if ((score >= 4 || gameTime >= 180.0f) && !endGamePanel.activeSelf)
        {
            Debug.Log("Jogo terminado!");
            EndGame();
        }
    }

    // --- Métodos de Início de Jogo ---
    public void StartGame()
    {
        playerPrefsPanel.SetActive(false);
        Time.timeScale = 1;
        // Inicializa a lista de vagas de inicio de fim das missões
        missoesInicioList = new List<GameObject>(missaoInicioDisponivel);
        missoesFimList = new List<GameObject>(missaoFimDisponivel);
        StartCoroutine(CountdownToStart());
    }

    System.Collections.IEnumerator CountdownToStart()
    {
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
        EscolherProximoObjetivo();
    }

    // --- Métodos de Vagas (INICIO)---
    public void EscolherProximoObjetivo()
    {
        // Lógica para redefinir tags (se a missão anterior foi desabilitada e não destruída)
        GameObject[] vagasDestaqueAtuais = GameObject.FindGameObjectsWithTag("Missao_Destaque");
        foreach (GameObject vaga in vagasDestaqueAtuais)
        {
            Transform[] allTransforms = vaga.GetComponentsInChildren<Transform>(true);
            foreach (Transform childTransform in allTransforms)
            {
                childTransform.gameObject.tag = "Missao_Vazia";
            }
        }

        if (missoesInicioList.Count > 0)
        {
            int indiceAleatorio = UnityEngine.Random.Range(0, missoesInicioList.Count);
            GameObject novaMissaoAlvo = missoesInicioList[indiceAleatorio];

            novaMissaoAlvo.SetActive(true);

            // Define a tag "Vaga_Destaque" nos colisores filhos
            Transform[] allTransformsNew = novaMissaoAlvo.GetComponentsInChildren<Transform>(true);
            foreach (Transform childTransform in allTransformsNew)
            {
                childTransform.gameObject.tag = "Missao_Destaque";
            }

            missoesInicioList.RemoveAt(indiceAleatorio);

            Debug.Log("Nova missão de destaque: " + novaMissaoAlvo.name);
        }
        else
        {
            Debug.LogWarning("Não há mais missões disponíveis para serem o alvo!");
            // Se as missões acabaram, e o score atingiu o objetivo, o jogo termina aqui.
            if (score >= 4 && !endGamePanel.activeSelf)
            {
                EndGame();
            }
        }
    }

    // --- Métodos de Vagas do Conjunto (FIM)---
    public void EscolherProximaVagaConjunto()
    {
        if (missoesFimList.Count > 0)
        {
            int indiceAleatorio = UnityEngine.Random.Range(0, missoesFimList.Count); //REMOVER
            GameObject proximaVagaConjunto = missoesFimList[indiceAleatorio];

            proximaVagaConjunto.SetActive(true);

            // Define a tag "Vaga_Destaque" nos colisores filhos
            Transform[] allTransformsNew = proximaVagaConjunto.GetComponentsInChildren<Transform>(true);
            foreach (Transform childTransform in allTransformsNew)
            {
                childTransform.gameObject.tag = "Missao_Destaque";
            }

            missoesInicioList.RemoveAt(indiceAleatorio);

            Debug.Log("Nova missão de destaque: " + novaMissaoAlvo.name);
        }
        else
        {
            Debug.LogWarning("Não há mais missões disponíveis para serem o alvo!");
            // Se as missões acabaram, e o score atingiu o objetivo, o jogo termina aqui.
            if (score >= 4 && !endGamePanel.activeSelf)
            {
                EndGame();
            }
        }
    }

    // --- LÓGICA DO RANKING E ENDGAME ---

    public string GetTempoFinal()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        if (gameTime < 60f)
        {
            return string.Format("{0:00}.{1:00}", timeSpan.Seconds, timeSpan.Milliseconds / 10);
        }
        else
        {
            return string.Format("{0:0}:{1:00}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
        }
    }

    private RankingData LoadRanking()
    {
        if (PlayerPrefs.HasKey(RankingKey))
        {
            string json = PlayerPrefs.GetString(RankingKey);
            return JsonUtility.FromJson<RankingData>(json);
        }
        return new RankingData();
    }

    private bool IsNewTop3Record(float newTime, RankingData currentRanking)
    {
        // Se a lista está vazia ou incompleta, é um recorde.
        if (currentRanking.entries.Count < MaxRankingEntries)
        {
            return true;
        }

        // Verifica se o novo tempo é menor que o tempo do 3º colocado (índice 2)
        float thirdPlaceTime = currentRanking.entries[MaxRankingEntries - 1].time;

        if (newTime < thirdPlaceTime)
        {
            return true;
        }

        return false;
    }

    private void AddScoreToRanking(string playerName, int finalScore, float finalTime)
    {
        // Se não alcançou o objetivo de 12 pontos, não entra no ranking de tempo.
        if (finalScore < 12)
        {
            UpdateRankingDisplay(LoadRanking());
            return;
        }

        RankingData ranking = LoadRanking();

        // 1. Cria e adiciona a nova entrada
        RankingEntry newEntry = new RankingEntry(playerName, finalScore, finalTime);
        ranking.entries.Add(newEntry);

        // 2. Ordena a lista: Do menor tempo para o maior
        ranking.entries = ranking.entries.OrderBy(e => e.time).ToList();

        // 3. Mantém apenas o Top 3
        if (ranking.entries.Count > MaxRankingEntries)
        {
            ranking.entries.RemoveRange(MaxRankingEntries, ranking.entries.Count - MaxRankingEntries);
        }

        // 4. Salva a nova lista
        string json = JsonUtility.ToJson(ranking);
        PlayerPrefs.SetString(RankingKey, json);
        PlayerPrefs.Save();

        UpdateRankingDisplay(ranking);
    }

    private void UpdateRankingDisplay(RankingData ranking)
    {
        TextMeshProUGUI[] rankingTexts = new TextMeshProUGUI[] { rankingTextTop1, rankingTextTop2, rankingTextTop3 };

        for (int i = 0; i < MaxRankingEntries; i++)
        {
            if (i < ranking.entries.Count)
            {
                RankingEntry entry = ranking.entries[i];

                // Formatação do tempo
                TimeSpan timeSpan = TimeSpan.FromSeconds(entry.time);
                string timeString;

                if (entry.time < 60f)
                {
                    timeString = string.Format("{0:00}.{1:00}", timeSpan.Seconds, timeSpan.Milliseconds / 10);
                }
                else
                {
                    timeString = string.Format("{0:0}:{1:00}.{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
                }

                // Saída final: Posição: Nome - Tempo (ex: 1º: Matheus - 1:00.00s)
                rankingTexts[i].text = $"{i + 1}º: {entry.playerName} - {timeString}s";
            }
            else
            {
                rankingTexts[i].text = $"{i + 1}º: ---";
            }
        }
    }

    // Função que exibe a tela de Fim de Jogo
    private void EndGame()
    {
        Time.timeScale = 0;
        endGamePanel.SetActive(true);
        string tempoFinal = GetTempoFinal();

        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        string playerName = PlayerPrefs.GetString(PlayerNameKey, "Anônimo");
        Debug.Log($"Nome do jogador CARREGADO para o ranking: {playerName}");

        if (score >= 12)
        {
            RankingData currentRanking = LoadRanking();
            bool isNewRecord = IsNewTop3Record(gameTime, currentRanking);

            if (isNewRecord)
            {
                finalTitleText.text = "NOVO RECORDE!";
                Debug.Log("Parabéns! Novo Recorde Top 3!");
            }
            else
            {
                finalTitleText.text = "Parabéns!";
            }

            // Adiciona, ordena e salva recorde
            AddScoreToRanking(playerName, score, gameTime);
        }
        else
        {
            finalTitleText.text = "Fim de Jogo!";
            Debug.Log("Fim de jogo! Seu tempo acabou! Pontuação Final: " + score + " pontos!");
            // Exibe o ranking anterior
            UpdateRankingDisplay(LoadRanking());
        }

        finalScoreText.text = "Pontuação Final: " + score;
        finalTimeText.text = "Tempo Final: " + tempoFinal + "s";
    }

    // Método para voltar ao menu
    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Método para limpar o ranking
    public void ClearRankingData()
    {
        PlayerPrefs.DeleteKey(RankingKey);
        PlayerPrefs.Save();
        Debug.Log("Ranking data limpo!");
    }
}