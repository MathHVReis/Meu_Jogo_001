using UnityEngine;

public class Contato_Vaga : MonoBehaviour {
    public int score = 0;

    // Referência para o script do GameManager
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        // Verifica se o objeto que o carro encostou tem a tag "Collider_Destaque"
        if (other.CompareTag("Collider_Destaque")) {
            score += 1;
            Debug.Log("Carro estacionou! Pontuação: " + score);

            // Destrói o objeto do collider com o qual o carro colidiu
            Destroy(other.gameObject);

            // Avisa ao GameManager para escolher um novo alvo
            gameManager.EscolherProximaVaga();
        }
    }
}