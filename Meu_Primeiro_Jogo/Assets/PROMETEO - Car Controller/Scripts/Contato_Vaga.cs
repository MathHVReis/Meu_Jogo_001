using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Contato_Vaga : MonoBehaviour {

    // Referência para o script do GameManager
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Collider_Destaque")) {
            gameManager.score++;
            Debug.Log("Carro estacionou! Pontuação: " + gameManager.score);

            Destroy(other.gameObject);

            gameManager.EscolherProximaVaga();
        }
    }
}