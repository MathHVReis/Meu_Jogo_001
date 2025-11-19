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
        // Opcional: Mudar cor para Amarelo (Alvo)
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Chama para resetar
    public void DesativarVaga()
    {
        estaAtiva = false;
        GetComponent<Renderer>().material.color = Color.gray; // Cor inativa
    }

    // DETECTA A COLISÃO
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se foi o carro (Player) e se esta vaga está ativa
        if (estaAtiva && other.CompareTag("Player"))
        {
            Debug.Log("Carro chegou na vaga 1! Liberando vaga 2...");

            // Avisa o gerente para liberar a vaga 2
            gerenciador.Vaga1Atingida(meuIndice);

            // Desativa para não disparar duas vezes
            estaAtiva = false;
            GetComponent<Renderer>().material.color = Color.green; // Feedback de sucesso na vaga 1
        }
    }
}