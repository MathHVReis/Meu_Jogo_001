using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Contato_Vaga : MonoBehaviour {

    // Referência para o script do GameManager
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Vaga_Destaque")) {
            gameManager.score++;
            Debug.Log("Carro estacionou! Pontuação: " + gameManager.score);

            // Destrói objetos colisores
            Destroy(other.transform.root.gameObject);

            gameManager.EscolherProximaVaga();
        }
    }
}