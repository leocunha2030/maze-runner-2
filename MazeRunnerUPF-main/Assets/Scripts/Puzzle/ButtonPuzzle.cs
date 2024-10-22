using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    private List<int> correctSequence = new List<int> { 0, 1, 2, 3 }; // Sequ�ncia correta
    private List<int> playerSequence = new List<int>(); // Sequ�ncia do jogador
    private float buttonCooldown = 0.5f; // Tempo de espera entre press�es de bot�o
    private float lastPressTime = 0; // �ltimo tempo em que um bot�o foi pressionado
    private bool isPressing = false; // Controle para evitar m�ltiplas press�es r�pidas

    public Transform player; // Refer�ncia ao objeto jogador
    public List<Light> lights; // Lista de luzes a serem acesas
    public Light redLight; // Refer�ncia � luz vermelha

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPressing) // Verifica se a tecla "E" foi pressionada
        {
            GameObject closestButton = GetClosestButton();
            if (closestButton != null)
            {
                isPressing = true; // Marca que estamos pressionando um bot�o
                int buttonIndex = closestButton.transform.GetSiblingIndex(); // Obt�m o �ndice do bot�o pressionado
                ButtonPressed(buttonIndex); // Registra a press�o do bot�o
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
            Debug.LogError("O objeto jogador n�o foi atribu�do!");
            return null; // Retorna null se o jogador n�o estiver definido
        }

        float closestDistance = Mathf.Infinity; // Inicializa a menor dist�ncia como infinita
        GameObject closestButton = null; // Inicializa o bot�o mais pr�ximo como null

        // Itera sobre todos os bot�es filhos
        foreach (Transform button in transform.parent)
        {
            float distance = Vector3.Distance(player.position, button.position); // Calcula a dist�ncia do jogador ao bot�o
            if (distance < closestDistance) // Se esta dist�ncia for menor que a menor dist�ncia registrada
            {
                closestDistance = distance; // Atualiza a menor dist�ncia
                closestButton = button.gameObject; // Define o bot�o mais pr�ximo
            }
        }

        return closestButton; // Retorna o bot�o mais pr�ximo
    }

    private void ButtonPressed(int buttonIndex)
    {
        // Verifica se o cooldown foi respeitado
        if (Time.time < lastPressTime + buttonCooldown)
        {
            return; // Ignora se o cooldown estiver ativo
        }

        playerSequence.Add(buttonIndex); // Adiciona � sequ�ncia do jogador
        lastPressTime = Time.time; // Atualiza o �ltimo tempo de press�o

        Debug.Log("Bot�o pressionado: " + buttonIndex); // Log do bot�o pressionado
        Debug.Log("Sequ�ncia do jogador ap�s pressionar: " + string.Join(", ", playerSequence)); // Log da sequ�ncia do jogador

        // Verifica se a sequ�ncia do jogador est� completa
        if (playerSequence.Count == correctSequence.Count)
        {
            CheckSequence(); // Verifica a sequ�ncia
        }
    }

    private void CheckSequence()
    {
        Debug.Log("Sequ�ncia do jogador: " + string.Join(", ", playerSequence)); // Log da sequ�ncia do jogador
        Debug.Log("Sequ�ncia correta: " + string.Join(", ", correctSequence)); // Log da sequ�ncia correta

        // Verifica se a sequ�ncia do jogador corresponde � sequ�ncia correta
        if (AreSequencesEqual(playerSequence, correctSequence))
        {
            Debug.Log("Sequ�ncia correta! Puzzle resolvido."); // Log de sucesso
            OpenDoor(); // Chama a fun��o para abrir a porta
            TurnOnLights(); // Chama a fun��o para acender as luzes
            if (redLight != null)
            {
                redLight.enabled = false; // Desliga a luz vermelha se estiver acesa
            }
        }
        else
        {
            Debug.Log("Sequ�ncia incorreta! Puzzle reiniciado."); // Log de falha
            if (redLight != null)
            {
                StartCoroutine(BlinkRedLight()); // Inicia a corrotina para piscar a luz vermelha
            }
            ResetPlayerSequence(); // Reseta a sequ�ncia se a press�o estiver errada
        }
    }

    private bool AreSequencesEqual(List<int> playerSeq, List<int> correctSeq)
    {
        for (int i = 0; i < correctSeq.Count; i++)
        {
            if (playerSeq[i] != correctSeq[i]) // Se um bot�o n�o corresponder
            {
                return false; // Sequ�ncias n�o s�o iguais
            }
        }
        return true; // Sequ�ncias s�o iguais
    }

    private void OpenDoor()
    {
        DoorController doorController = FindObjectOfType<DoorController>(); // Encontra o script da porta
        if (doorController != null)
        {
            doorController.OpenDoor(); // Chama o m�todo de abrir a porta
        }
        else
        {
            Debug.LogError("DoorController n�o encontrado!");
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
        playerSequence.Clear(); // Limpa a sequ�ncia do jogador
        if (redLight != null)
        {
            redLight.enabled = true; // Liga a luz vermelha ao reiniciar
        }
        Debug.Log("Puzzle reiniciado!"); // Log de rein�cio
    }
}
