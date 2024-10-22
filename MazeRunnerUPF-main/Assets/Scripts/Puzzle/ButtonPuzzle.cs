using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    private List<int> correctSequence = new List<int> { 0, 1, 2, 3 }; // Sequência correta
    private List<int> playerSequence = new List<int>(); // Sequência do jogador
    private float buttonCooldown = 0.5f; // Tempo de espera entre pressões de botão
    private float lastPressTime = 0; // Último tempo em que um botão foi pressionado
    private bool isPressing = false; // Controle para evitar múltiplas pressões rápidas

    public Transform player; // Referência ao objeto jogador
    public List<Light> lights; // Lista de luzes a serem acesas
    public Light redLight; // Referência à luz vermelha

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPressing) // Verifica se a tecla "E" foi pressionada
        {
            GameObject closestButton = GetClosestButton();
            if (closestButton != null)
            {
                isPressing = true; // Marca que estamos pressionando um botão
                int buttonIndex = closestButton.transform.GetSiblingIndex(); // Obtém o índice do botão pressionado
                ButtonPressed(buttonIndex); // Registra a pressão do botão
            }
        }
        else if (Input.GetKeyUp(KeyCode.E)) // Libera o controle ao soltar a tecla
        {
            isPressing = false;
        }
    }

    private GameObject GetClosestButton()
    {
        if (player == null)
        {
            Debug.LogError("O objeto jogador não foi atribuído!");
            return null; // Retorna null se o jogador não estiver definido
        }

        float closestDistance = Mathf.Infinity; // Inicializa a menor distância como infinita
        GameObject closestButton = null; // Inicializa o botão mais próximo como null

        // Itera sobre todos os botões filhos
        foreach (Transform button in transform.parent)
        {
            float distance = Vector3.Distance(player.position, button.position); // Calcula a distância do jogador ao botão
            if (distance < closestDistance) // Se esta distância for menor que a menor distância registrada
            {
                closestDistance = distance; // Atualiza a menor distância
                closestButton = button.gameObject; // Define o botão mais próximo
            }
        }

        return closestButton; // Retorna o botão mais próximo
    }

    private void ButtonPressed(int buttonIndex)
    {
        // Verifica se o cooldown foi respeitado
        if (Time.time < lastPressTime + buttonCooldown)
        {
            return; // Ignora se o cooldown estiver ativo
        }

        playerSequence.Add(buttonIndex); // Adiciona à sequência do jogador
        lastPressTime = Time.time; // Atualiza o último tempo de pressão

        Debug.Log("Botão pressionado: " + buttonIndex); // Log do botão pressionado
        Debug.Log("Sequência do jogador após pressionar: " + string.Join(", ", playerSequence)); // Log da sequência do jogador

        // Verifica se a sequência do jogador está completa
        if (playerSequence.Count == correctSequence.Count)
        {
            CheckSequence(); // Verifica a sequência
        }
    }

    private void CheckSequence()
    {
        Debug.Log("Sequência do jogador: " + string.Join(", ", playerSequence)); // Log da sequência do jogador
        Debug.Log("Sequência correta: " + string.Join(", ", correctSequence)); // Log da sequência correta

        // Verifica se a sequência do jogador corresponde à sequência correta
        if (AreSequencesEqual(playerSequence, correctSequence))
        {
            Debug.Log("Sequência correta! Puzzle resolvido."); // Log de sucesso
            OpenDoor(); // Chama a função para abrir a porta
            TurnOnLights(); // Chama a função para acender as luzes
            if (redLight != null)
            {
                redLight.enabled = false; // Desliga a luz vermelha se estiver acesa
            }
        }
        else
        {
            Debug.Log("Sequência incorreta! Puzzle reiniciado."); // Log de falha
            if (redLight != null)
            {
                StartCoroutine(BlinkRedLight()); // Inicia a corrotina para piscar a luz vermelha
            }
            ResetPlayerSequence(); // Reseta a sequência se a pressão estiver errada
        }
    }

    private bool AreSequencesEqual(List<int> playerSeq, List<int> correctSeq)
    {
        for (int i = 0; i < correctSeq.Count; i++)
        {
            if (playerSeq[i] != correctSeq[i]) // Se um botão não corresponder
            {
                return false; // Sequências não são iguais
            }
        }
        return true; // Sequências são iguais
    }

    private void OpenDoor()
    {
        DoorController doorController = FindObjectOfType<DoorController>(); // Encontra o script da porta
        if (doorController != null)
        {
            doorController.OpenDoor(); // Chama o método de abrir a porta
        }
        else
        {
            Debug.LogError("DoorController não encontrado!");
        }
    }

    private void TurnOnLights()
    {
        foreach (Light light in lights) // Para cada luz na lista
        {
            light.enabled = true; // Ativa a luz
        }
        Debug.Log("As luzes foram acesas!"); // Log de sucesso
    }

    private IEnumerator BlinkRedLight()
    {
        for (int i = 0; i < 3; i++) // Pisca 3 vezes
        {
            redLight.enabled = true; // Liga a luz vermelha
            yield return new WaitForSeconds(0.5f); // Espera meio segundo
            redLight.enabled = false; // Desliga a luz vermelha
            yield return new WaitForSeconds(0.5f); // Espera meio segundo
        }
    }

    public void ResetPlayerSequence()
    {
        playerSequence.Clear(); // Limpa a sequência do jogador
        if (redLight != null)
        {
            redLight.enabled = true; // Liga a luz vermelha ao reiniciar
        }
        Debug.Log("Puzzle reiniciado!"); // Log de reinício
    }
}
