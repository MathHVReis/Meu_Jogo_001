using System;
using System.Collections.Generic;

// Estrutura para a entrada de um único jogador
[Serializable]
public class RankingEntry
{
    public string playerName;
    public int score;
    public float time; // Tempo em segundos para fácil ordenação

    // Construtor
    public RankingEntry(string name, int s, float t)
    {
        playerName = name;
        score = s;
        time = t;
    }
}

// Classe que contêm a lista de todas as entradas do ranking
[Serializable]
public class RankingData
{
    // A lista precisa ser pública para ser serializada pelo Unity JSON Utility
    public List<RankingEntry> entries = new List<RankingEntry>();
}