using UnityEngine;
using UnityEngine.UI;

public class ModeSelectorController : MonoBehaviour
{
    [Header("Componentes de Sele��o")]
    [SerializeField] private Button btnModoRapido;
    [SerializeField] private Button btnModoDesafio;
    [SerializeField] private Button botaoJogar;

    [Header("Cores Hexadecimais")]
    [Tooltip("Cor dos bot�es de modo quando N�O selecionados.")]
    public string modeUnselectedHex = "#D5BE7A"; // Bege padr�o
    [Tooltip("Cor dos bot�es de modo quando SELECIONADOS.")]
    public string modeSelectedHex = "#00FF00"; // Verde

    [Tooltip("Cor do Bot�o Jogar quando inativo.")]
    public string playInativoHex = "#D41F1B"; // Vermelho Padr�o
    [Tooltip("Cor do Bot�o Jogar quando ativo.")]
    public string playAtivoHex = "#00FF00"; // Verde

    // Vari�veis internas para armazenar o resultado da convers�o
    private Color _modeUnselectedColor;
    private Color _modeSelectedColor;
    private Color _playInativoColor;
    private Color _playAtivoColor;

    private Button _atualSelectedModeButton = null;

    void Awake()
    {
        // 1. Convers�o de Hex para Color
        ConvertHexToColors();
        // 2. Garante o estado inicial: Jogar inativo, Modos desmarcados
        InitializeButtonStates();

        // 3. Adiciona Listeners programaticamente (melhor pr�tica)
        btnModoRapido.onClick.AddListener(() => SelectMode(btnModoRapido));
        btnModoDesafio.onClick.AddListener(() => SelectMode(btnModoDesafio));
    }

    private void ConvertHexToColors()
    {
        // Usa TryParseHtmlString que lida com o formato #RRGGBB(AA)
        ColorUtility.TryParseHtmlString(modeUnselectedHex, out _modeUnselectedColor);
        ColorUtility.TryParseHtmlString(modeSelectedHex, out _modeSelectedColor);
        ColorUtility.TryParseHtmlString(playInativoHex, out _playInativoColor);
        ColorUtility.TryParseHtmlString(playAtivoHex, out _playAtivoColor);
    }

    private void InitializeButtonStates()
    {
        // Bot�es de Modo: cor de n�o selecionado
        SetButtonColor(btnModoRapido, _modeUnselectedColor);
        SetButtonColor(btnModoDesafio, _modeUnselectedColor);

        // Bot�o Jogar: inativo e com cor inativa
        UpdatePlayButtonState(false);
    }

    // Fun��o principal de sele��o que gerencia os dois bot�es de modo
    private void SelectMode(Button clickedButton)
    {
        // 1. Se j� havia um bot�o selecionado, retorna-o � cor desmarcada
        if (_atualSelectedModeButton != null && _atualSelectedModeButton != clickedButton)
        {
            SetButtonColor(_atualSelectedModeButton, _modeUnselectedColor);
        }

        // 2. Define o novo bot�o selecionado e atualiza sua cor
        _atualSelectedModeButton = clickedButton;
        SetButtonColor(_atualSelectedModeButton, _modeSelectedColor);

        // 3. Ativa o Bot�o Jogar
        UpdatePlayButtonState(true);
    }

    // Fun��o utilit�ria para aplicar cor e estado ao Bot�o Jogar
    private void UpdatePlayButtonState(bool isActive)
    {
        botaoJogar.interactable = isActive;

        Color targetColor = isActive ? _playAtivoColor : _playInativoColor;
        SetButtonColor(botaoJogar, targetColor);
    }

    // Fun��o utilit�ria para aplicar uma cor espec�fica ao 'normalColor' de um bot�o
    private void SetButtonColor(Button button, Color newColor)
    {
        var colors = button.colors;
        colors.normalColor = newColor;
        button.colors = colors;
    }

    // Fun��o de execu��o que ser� vinculada ao OnClick do Bot�o Jogar
    public void StartGame()
    {
        string selectedMode = "Nenhum";
        if (_atualSelectedModeButton == btnModoRapido) selectedMode = "Modo R�pido";
        else if (_atualSelectedModeButton == btnModoDesafio) selectedMode = "Modo Desafio";

        Debug.Log($"INICIANDO JOGO: {selectedMode}");

        // *** Coloque aqui a l�gica para carregar a pr�xima cena ou iniciar o jogo. ***
    }
}