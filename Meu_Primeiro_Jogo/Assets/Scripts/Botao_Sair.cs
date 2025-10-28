using UnityEngine;

public class Botao_Sair : MonoBehaviour {
    // Esta fun��o ser� chamada quando o bot�o for clicado
    public void FecharJogo() {
        // 1. Log para console (ajuda no debug)
        Debug.Log("Saindo do jogo...");

        // 2. O comando que realmente fecha o aplicativo
        Application.Quit();

        // 3. Condi��o para o Editor (opcional, apenas para ver a fun��o no console do Unity)
#if UNITY_EDITOR
        // Se estiver no editor, para de rodar o jogo
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}