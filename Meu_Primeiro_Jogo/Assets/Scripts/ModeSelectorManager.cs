using UnityEngine;
using UnityEngine.UI;

public class ModeSelectorController : MonoBehaviour
{
    [Header("Componentes de Seleção")]
    [SerializeField] private Button btnModoRapido;
    [SerializeField] private Button btnModoDesafio;
    [SerializeField] private Button botaoJogar;

    [Header("Cores Hexadecimais")]
    [Tooltip("Cor dos botões de modo quando NÃO selecionados.")]
    public string modeUnselectedHex = "#D5BE7A"; // Bege padrão
    [Tooltip("Cor dos botões de modo quando SELECIONADOS.")]
    public string modeSelectedHex = "#00FF00"; // Verde

    [Tooltip("Cor do Botão Jogar quando inativo.")]
    public string playInativoHex = "#D41F1B"; // Vermelho Padrão
    [Tooltip("Cor do Botão Jogar quando ativo.")]
    public string playAtivoHex = "#00FF00"; // Verde

    // Variáveis internas para armazenar o resultado da conversão
    private Color _modeUnselectedColor;
    private Color _modeSelectedColor;
    private Color _playInativoColor;
    private Color _playAtivoColor;

    private Button _atualSelectedModeButton = null;

    void Awake()
    {
        // 1. Conversão de Hex para Color
        ConvertHexToColors();
        // 2. Garante o estado inicial: Jogar inativo, Modos desmarcados
        InitializeButtonStates();

        // 3. Adiciona Listeners programaticamente (melhor prática)
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
        // Botões de Modo: cor de não selecionado
        SetButtonColor(btnModoRapido, _modeUnselectedColor);
        SetButtonColor(btnModoDesafio, _modeUnselectedColor);

        // Botão Jogar: inativo e com cor inativa
        UpdatePlayButtonState(false);
    }

    // Função principal de seleção que gerencia os dois botões de modo
    private void SelectMode(Button clickedButton)
    {
        // 1. Se já havia um botão selecionado, retorna-o à cor desmarcada
        if (_atualSelectedModeButton != null && _atualSelectedModeButton != clickedButton)
        {
            SetButtonColor(_atualSelectedModeButton, _modeUnselectedColor);
        }

        // 2. Define o novo botão selecionado e atualiza sua cor
        _atualSelectedModeButton = clickedButton;
        SetButtonColor(_atualSelectedModeButton, _modeSelectedColor);

        // 3. Ativa o Botão Jogar
        UpdatePlayButtonState(true);
    }

    // Função utilitária para aplicar cor e estado ao Botão Jogar
    private void UpdatePlayButtonState(bool isActive)
    {
        botaoJogar.interactable = isActive;

        Color targetColor = isActive ? _playAtivoColor : _playInativoColor;
        SetButtonColor(botaoJogar, targetColor);
    }

    // Função utilitária para aplicar uma cor específica ao 'normalColor' de um botão
    private void SetButtonColor(Button button, Color newColor)
    {
        var colors = button.colors;
        colors.normalColor = newColor;
        button.colors = colors;
    }

    // Função de execução que será vinculada ao OnClick do Botão Jogar
    public void StartGame()
    {
        string selectedMode = "Nenhum";
        if (_atualSelectedModeButton == btnModoRapido) selectedMode = "Modo Rápido";
        else if (_atualSelectedModeButton == btnModoDesafio) selectedMode = "Modo Desafio";

        Debug.Log($"INICIANDO JOGO: {selectedMode}");

        // *** Coloque aqui a lógica para carregar a próxima cena ou iniciar o jogo. ***
    }
}