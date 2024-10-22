using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float rotationAngle = 90f; // �ngulo de rota��o da porta ao abrir
    public float speed = 1f; // Velocidade da rota��o

    private Quaternion closedRotation; // Rota��o inicial da porta
    private Quaternion openRotation; // Rota��o quando a porta est� aberta

    private void Start()
    {
        // Salva a rota��o inicial da porta
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + rotationAngle, transform.eulerAngles.z); // Define a rota��o para abrir a porta
    }

    public void OpenDoor()
    {
        // Inicia a coroutine para abrir a porta
        StartCoroutine(RotateDoor(openRotation));
        Debug.Log("A porta foi aberta!"); // Log para confirmar que a porta foi aberta
    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        // Enquanto a porta n�o atingir a rota��o desejada
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            // Rotaciona a porta suavemente para a rota��o alvo
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
            yield return null; // Espera at� o pr�ximo quadro
        }

        // Garante que a porta termine exatamente na rota��o alvo
        transform.rotation = targetRotation;
    }
}
