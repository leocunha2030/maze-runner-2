using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float rotationAngle = 90f; // Ângulo de rotação da porta ao abrir
    public float speed = 1f; // Velocidade da rotação

    private Quaternion closedRotation; // Rotação inicial da porta
    private Quaternion openRotation; // Rotação quando a porta está aberta

    private void Start()
    {
        // Salva a rotação inicial da porta
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + rotationAngle, transform.eulerAngles.z); // Define a rotação para abrir a porta
    }

    public void OpenDoor()
    {
        // Inicia a coroutine para abrir a porta
        StartCoroutine(RotateDoor(openRotation));
        Debug.Log("A porta foi aberta!"); // Log para confirmar que a porta foi aberta
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        // Enquanto a porta não atingir a rotação desejada
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            // Rotaciona a porta suavemente para a rotação alvo
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            yield return null; // Espera até o próximo quadro
        }

        // Garante que a porta termine exatamente na rotação alvo
        transform.rotation = targetRotation;
    }
}
