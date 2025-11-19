using UnityEngine;

public class GerenciadorGaragem : MonoBehaviour
{
    [Header("Vagas com o Script 'VagaSensor'")]
    public ContatoVaga_Desafio[] conjunto1; // Arraste as vagas do Conjunto 1 aqui

    [Header("Objetos Visuais do Conjunto 2")]
    public GameObject[] conjunto2; // Arraste as vagas do Conjunto 2 aqui

    [Header("Feedback Visual")]
    public Color corLiberadaConjunto2 = Color.green;
    public Color corBloqueadaConjunto2 = Color.red;

    void Start()
    {
        // Configuração automática dos índices nas vagas do Conjunto 1
        for (int i = 0; i < conjunto1.Length; i++)
        {
            // Passa 'this' (o gerente) e o número 'i' para a vaga saber quem ela é
            conjunto1[i].Configurar(this, i);

            // Garante que o conjunto 2 comece "bloqueado"
            conjunto2[i].GetComponent<Renderer>().material.color = corBloqueadaConjunto2;
        }
    }

    // Chame esta função no seu Botão de "Sortear"
    public void IniciarSorteio()
    {
        // 1. Reseta tudo antes de começar
        ResetarTudo();

        // 2. Sorteia
        int indiceSorteado = Random.Range(0, conjunto1.Length);
        Debug.Log("Vaga sorteada: " + indiceSorteado);

        // 3. Ativa APENAS a vaga do Conjunto 1 para esperar a colisão
        conjunto1[indiceSorteado].AtivarVaga();
    }

    // Esta função é chamada AUTOMATICAMENTE pelo script da VagaSensor
    public void Vaga1Atingida(int indice)
    {
        // O carro bateu na vaga correta do Conjunto 1.
        // Agora liberamos a vaga correspondente no Conjunto 2.

        GameObject vaga2 = conjunto2[indice];

        // Lógica de liberação (Mudar cor, abrir portão, tocar som, etc)
        vaga2.GetComponent<Renderer>().material.color = corLiberadaConjunto2;

        Debug.Log("Vaga do Conjunto 2 liberada no índice: " + indice);
    }

    void ResetarTudo()
    {
        for (int i = 0; i < conjunto1.Length; i++)
        {
            conjunto1[i].DesativarVaga();
            conjunto2[i].GetComponent<Renderer>().material.color = corBloqueadaConjunto2;
        }
    }
}