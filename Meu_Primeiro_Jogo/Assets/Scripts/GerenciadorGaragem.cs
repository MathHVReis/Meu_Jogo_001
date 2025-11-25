using UnityEngine;

public class GerenciadorGaragem : MonoBehaviour
{
    [Header("Vagas com o Script 'VagaSensor'")]
    public ContatoVaga_Desafio[] conjunto1; // Arraste as vagas do Conjunto 1 aqui

    [Header("Objetos do Conjunto 2")]
    public GameObject[] conjunto2; // Arraste as vagas do Conjunto 2 aqui

    [Header("Feedback Visual (antigo)")]
    public Color corLiberadaConjunto2 = Color.green;
    public Color corBloqueadaConjunto2 = Color.red;

    void Start()
    {
        // Proteções contra null
        if (conjunto1 == null) conjunto1 = new ContatoVaga_Desafio[0];
        if (conjunto2 == null) conjunto2 = new GameObject[0];

        // Configuração automática dos índices nas vagas do Conjunto 1
        int len = Mathf.Min(conjunto1.Length, conjunto2.Length);
        for (int i = 0; i < len; i++)
        {
            if (conjunto1[i] != null)
            {
                // Passa 'this' (o gerente) e o número 'i' para a vaga saber quem ela é
                conjunto1[i].Configurar(this, i);
            }

            // Garante que o conjunto 2 comece "bloqueado" — desativa o GameObject
            if (conjunto2[i] != null && conjunto2[i].activeSelf)
            {
                conjunto2[i].SetActive(false);
            }
        }

        // If arrays have different lengths, still configure remaining conjunto1 entries
        for (int i = len; i < conjunto1.Length; i++)
        {
            if (conjunto1[i] != null)
            {
                conjunto1[i].Configurar(this, i);
            }
        }
    }

    // Chame esta função no seu Botão de "Sortear"
    public void IniciarSorteio()
    {
        // 1. Reseta tudo antes de começar
        ResetarTudo();

        if (conjunto1 == null || conjunto1.Length == 0)
        {
            Debug.LogWarning($"[{nameof(GerenciadorGaragem)}] Não há vagas em conjunto1 para sortear.");
            return;
        }

        // 2. Sorteia
        int indiceSorteado = Random.Range(0, conjunto1.Length);
        Debug.Log("Vaga sorteada: " + indiceSorteado);

        // 3. Ativa APENAS a vaga do Conjunto 1 para esperar a colisão
        if (conjunto1[indiceSorteado] != null)
        {
            // Certifica-se de que o objeto esteja ativo para detectar colisões
            if (!conjunto1[indiceSorteado].gameObject.activeSelf)
            {
                conjunto1[indiceSorteado].gameObject.SetActive(true);
            }

            conjunto1[indiceSorteado].AtivarVaga();
        }
        else
        {
            Debug.LogWarning($"[{nameof(GerenciadorGaragem)}] vaga sorteada é null no índice {indiceSorteado}.");
        }
    }

    // Alias caso exista chamada com a grafia diferente
    public void Iniciarsorteio()
    {
        IniciarSorteio();
    }

    // Esta função é chamada AUTOMATICAMENTE pelo script da VagaSensor
    public void Vaga1Atingida(int indice)
    {
        // Valida índice e existência do alvo
        if (conjunto2 == null || indice < 0 || indice >= conjunto2.Length)
        {
            Debug.LogWarning($"[{nameof(GerenciadorGaragem)}] Índice inválido para liberação: {indice}");
            return;
        }

        GameObject vaga2 = conjunto2[indice];
        if (vaga2 == null)
        {
            Debug.LogWarning($"[{nameof(GerenciadorGaragem)}] conjunto2[{indice}] é null.");
            return;
        }

        // Em vez de mudar material, ativamos o GameObject para liberar a vaga
        if (!vaga2.activeSelf)
        {
            vaga2.SetActive(true);
        }

        Debug.Log("Vaga do Conjunto 2 liberada no índice: " + indice);
    }

    void ResetarTudo()
    {
        if (conjunto1 != null)
        {
            for (int i = 0; i < conjunto1.Length; i++)
            {
                if (conjunto1[i] != null)
                {
                    conjunto1[i].DesativarVaga();
                }
            }
        }

        if (conjunto2 != null)
        {
            for (int i = 0; i < conjunto2.Length; i++)
            {
                if (conjunto2[i] != null && conjunto2[i].activeSelf)
                {
                    conjunto2[i].SetActive(false);
                }
            }
        }
    }
}