using UnityEngine;
using TMPro;

public class Botao_SaveDados : MonoBehaviour
{
    public TMP_InputField inputNomePlayer;
    public GameManager gameManager;
    public void salvarDados()
    {
        string nomePlayer = inputNomePlayer.text;
        PlayerPrefs.SetString("PlayerName", nomePlayer);
        PlayerPrefs.Save();
        Debug.Log("Nome do Player salvo como: {nomePlayer}");
        //Dados para serem salvos: NomePlayer, TempoTotal, Pontua��oFinal
        //Adicionar canvas espec�fico para deixar o nomePlayer encima do carro 

        //Inicia o jogo, que cont�m a contagem regressiva e reativa o tempo
        if (gameManager != null) {
            gameManager.StartGame();
        } else {
            Debug.LogError("Refer�ncia ao GameManager n�o definida em Botao_SaveDados!");
        }
    }
}
