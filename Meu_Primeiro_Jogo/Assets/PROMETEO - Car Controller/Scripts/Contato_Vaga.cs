using UnityEngine;

public class Contato_Vaga : MonoBehaviour {
    public int score = 0;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Vaga_Destaque")) {
            score += 1;
            Debug.Log("Carro estacionou! Pontuação: " + score);

            // Apenas avisa ao GameManager que uma vaga foi usada
            gameManager.VagaEstacionada();
        }
    }
}