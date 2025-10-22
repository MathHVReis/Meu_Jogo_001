using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelectorController : MonoBehaviour
{
    [Header("Componentes de Seleção")]
    [SerializeField] private Button btnModoRapido;
    [SerializeField] private Button btnModoDesafio;
    [SerializeField] private Button botaoJogar;

    public Color novaCor;
    public Color corPadrao;

    public void habilitarJogoRapido() {
        desabilitarBotoes();
        Image imagem = btnModoRapido.GetComponent<Image>();
        imagem.color = novaCor;
    }

    public void habilitarJogoDesafio() {
        desabilitarBotoes();
        Image imagem = btnModoDesafio.GetComponent<Image>();
        imagem.color = novaCor;
    }

    public void desabilitarBotoes()
    {
        Image imagemDesafio = btnModoDesafio.GetComponent<Image>();
        Image imagemRapido = btnModoRapido.GetComponent<Image>();

        imagemDesafio.color = corPadrao;
        imagemRapido.color = corPadrao;
    }

    public void startGame()
    {
        Color corAtual = btnModoRapido.GetComponent<Image>().color;
        Color corDesejada;

        if (ColorUtility.TryParseHtmlString("#20CC26", out corDesejada))
            {
            SceneManager.LoadScene("Game");
        }
    }
}