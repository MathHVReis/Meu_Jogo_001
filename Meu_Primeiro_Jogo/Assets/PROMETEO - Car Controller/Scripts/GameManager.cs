using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    // Lista de todas as posições possíveis para as vagas
    public Transform[] posicoesPossiveis;

    // Lista para guardar as posições disponíveis
    private List<Transform> posicoesDisponiveis;

    // Esta função é chamada pelo script do carro
    public void VagaEstacionada() {
        // Passo 1: Encontra todas as vagas com a tag "Vaga_Destaque"
        GameObject[] vagasParaMover = GameObject.FindGameObjectsWithTag("Vaga_Destaque");

        // Passo 2: Prepara e embaralha a lista de posições disponíveis
        posicoesDisponiveis = new List<Transform>(posicoesPossiveis);
        Shuffle(posicoesDisponiveis);

        // Passo 3: Move cada uma das vagas encontradas para uma nova posição
        for (int i = 0; i < vagasParaMover.Length; i++) {
            if (i < posicoesDisponiveis.Count) {
                // Acha o objeto principal da vaga (pai, se o collider for um filho)
                Transform vagaCompleta = vagasParaMover[i].transform.parent != null ? vagasParaMover[i].transform.parent : vagasParaMover[i].transform;

                // Move a vaga para a nova posição
                vagaCompleta.position = posicoesDisponiveis[i].position;
                vagaCompleta.rotation = posicoesDisponiveis[i].rotation;
            }
            else {
                Debug.LogWarning("Mais vagas do que posições disponíveis!");
                break;
            }
        }
    }

    // Método para "embaralhar" a lista de posições
    private void Shuffle(List<Transform> list) {
        for (int i = 0; i < list.Count; i++) {
            Transform temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}