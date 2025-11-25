using UnityEngine;

public class ContatoVaga_Desafio : MonoBehaviour
{
    [HideInInspector]
    public int meuIndice; // O Gerenciador vai preencher isso automaticamente

    private GerenciadorGaragem gerenciador;
    private bool estaAtiva = false; // Só dispara se for a vaga sorteada

    public void Configurar(GerenciadorGaragem manager, int indice)
    {
        gerenciador = manager;
        meuIndice = indice;
    }

    // Chama quando o Gerenciador diz que esta é a vaga alvo
    public void AtivarVaga()
    {
        estaAtiva = true;
        // Mostra o GameObject da vaga (ativa a visualização)
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    // Chama para resetar
    public void DesativarVaga()
    {
        estaAtiva = false;
        // Desativa todo o GameObject que representa a vaga (esconde)
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    // DETECTA A COLISÃO
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se foi o carro (Player) e se esta vaga está ativa
        if (estaAtiva && other.CompareTag("Player"))
        {
            Debug.Log("Carro chegou na vaga 1! Liberando vaga 2...");

            // Avisa o gerente para liberar a vaga 2 (safe check)
            if (gerenciador != null)
            {
                gerenciador.Vaga1Atingida(meuIndice);
            }
            else
            {
                Debug.LogWarning($"ContatoVaga_Desafio: gerenciador não atribuído para vaga {meuIndice}");
            }

            // Desativa para não disparar duas vezes
            estaAtiva = false;

            // Mantém o GameObject ativo para feedback de sucesso (ou desative, se preferir)
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }
}