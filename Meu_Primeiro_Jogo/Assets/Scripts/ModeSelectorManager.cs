using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelectorController : MonoBehaviour {
    [Header("Componentes de Seleção")]
    [SerializeField] private Button btnModoRapido;
    [SerializeField] private Button btnModoDesafio;
    [SerializeField] private Button botaoJogar;

    [Header("Textos dos Botões")]
    [SerializeField] private Text textoBotaoJogar;

    [Header("Cores dos Botões")]
    [SerializeField] private Color novaCor;
    [SerializeField] private Color corPadrao;
    [SerializeField] private Color corPadraoTexto;
    [SerializeField] private Color corPadraoJogar;

    public void habilitarJogoRapido() {
        desabilitarBotoes();
        Image imagemR = btnModoRapido.GetComponent<Image>();
        Image imagemJ = botaoJogar.GetComponent<Image>();
        Text textoRapido = btnModoRapido.GetComponentInChildren<Text>();
        imagemR.color = novaCor;
        imagemJ.color = novaCor;
        if (textoRapido != null) {
            textoRapido.color = Color.black;
        }
    }

    public void habilitarJogoDesafio() {
        desabilitarBotoes();
        Image imagemD = btnModoDesafio.GetComponent<Image>();
        Image imagemJ = botaoJogar.GetComponent<Image>();
        Text textoDesafio = btnModoDesafio.GetComponentInChildren<Text>();
        imagemD.color = novaCor;
        imagemJ.color = novaCor;
        if (textoDesafio != null) {
            textoDesafio.color = Color.black;
        }
    }

    public void desabilitarBotoes() {
        Image imagemDesafio = btnModoDesafio.GetComponent<Image>();
        Image imagemRapido = btnModoRapido.GetComponent<Image>();
        Image imagemJogar = botaoJogar.GetComponent<Image>();

        Text textoRapido = btnModoRapido.GetComponentInChildren<Text>();
        Text textoDesafio = btnModoDesafio.GetComponentInChildren<Text>();

        imagemDesafio.color = corPadrao;
        imagemRapido.color = corPadrao;
        imagemJogar.color = corPadraoJogar;

        if (textoRapido != null) {
            textoRapido.color = corPadraoTexto;
        }
        if (textoDesafio != null) {
            textoDesafio.color = corPadraoTexto;
        }
    }

    public void startGame() {

        Color corDesejada;
        if (!ColorUtility.TryParseHtmlString("#20CC26", out corDesejada)) {
            corDesejada = novaCor;
        }

        // --- Aplica a cor ao botão JOGAR (feedback visual) ---
        Image imagemJogar = botaoJogar.GetComponent<Image>();
        imagemJogar.color = corDesejada;

        // Use a referência que você vinculou no Inspector
        if (textoBotaoJogar != null) {
            textoBotaoJogar.color = Color.black;
        }
        else {
            // Tenta buscar o texto se a referência no Inspector falhar
            Text textoJogar = botaoJogar.GetComponentInChildren<Text>();
            if (textoJogar != null) {
                textoJogar.color = Color.black;
            }
        }

        // Verifica se o MODO RÁPIDO está selecionado
        if (btnModoRapido.GetComponent<Image>().color == novaCor) {
            SceneManager.LoadScene("Game_Rapido");
            return; // Sai da função após carregar a cena
        }
        // Verifica se o MODO DESAFIO está selecionado
        else if (btnModoDesafio.GetComponent<Image>().color == novaCor) {
            SceneManager.LoadScene("Game_Desafio");
            return; // Sai da função após carregar a cena
        }

        Debug.LogWarning("Nenhum modo de jogo foi selecionado.");
    }
}