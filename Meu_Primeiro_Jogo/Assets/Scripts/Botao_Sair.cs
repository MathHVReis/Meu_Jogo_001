using UnityEngine;

public class Botao_Sair : MonoBehaviour {
    // Esta função será chamada quando o botão for clicado
    public void FecharJogo() {
        // 1. Log para console (ajuda no debug)
        Debug.Log("Saindo do jogo...");

        // 2. O comando que realmente fecha o aplicativo
        Application.Quit();

        // 3. Condição para o Editor (opcional, apenas para ver a função no console do Unity)
#if UNITY_EDITOR
        // Se estiver no editor, para de rodar o jogo
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}