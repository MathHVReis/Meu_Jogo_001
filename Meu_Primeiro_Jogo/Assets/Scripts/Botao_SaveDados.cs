using UnityEngine;
using TMPro;

public class Botao_SaveDados : MonoBehaviour
{
    public TMP_InputField inputNomePlayer;
    public GM_Desafio gameManager;

    private const string PlayerNameKey = "PlayerName";

    public void salvarDados()
    {
        string nomePlayer = inputNomePlayer.text;

        // Verificação de Nome Vazio
        if (string.IsNullOrWhiteSpace(nomePlayer))
        {
            nomePlayer = "Anônimo";
            inputNomePlayer.text = nomePlayer;
        }

        // Salva o nome
        PlayerPrefs.SetString(PlayerNameKey, nomePlayer);
        PlayerPrefs.Save();
        Debug.Log($"Nome do Player salvo como: {nomePlayer}");

        // Inicia o Jogo
        if (gameManager != null)
        {
            gameManager.StartGame();
        }
        else
        {
            Debug.LogError("Referência ao GameManager não definida em Botao_SaveDados! O jogo não irá iniciar.");
        }
    }
}