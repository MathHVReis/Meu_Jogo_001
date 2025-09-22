using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Este m�todo � chamado pelo carro para escolher a pr�xima vaga
    public void EscolherProximaVaga() {
        // Encontra todos os objetos com a tag "Collider_Vaga"
        GameObject[] vagasDisponiveis = GameObject.FindGameObjectsWithTag("Collider_Vaga");

        // Checa se h� vagas dispon�veis para escolher
        if (vagasDisponiveis.Length > 0) {
            // Escolhe uma vaga aleat�ria da lista
            int indiceAleatorio = Random.Range(0, vagasDisponiveis.Length);
            GameObject novaVagaAlvo = vagasDisponiveis[indiceAleatorio];

            // Muda a tag da nova vaga para "Collider_Destaque"
            novaVagaAlvo.tag = "Collider_Destaque";

            Debug.Log("Nova vaga de destaque: " + novaVagaAlvo.name);
        }
        else {
            Debug.LogWarning("N�o h� mais vagas dispon�veis para serem o alvo!");
        }
    }
}