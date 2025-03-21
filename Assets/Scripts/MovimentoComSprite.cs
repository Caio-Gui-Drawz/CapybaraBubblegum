using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform balloon;  // O balão (objeto pai) que controla o movimento
    public float swingStrength = 5f;  // Intensidade do movimento de pêndulo
    public float smoothSpeed = 0.1f;  // Suavidade do movimento

    private Rigidbody rb;  // Referência ao Rigidbody do objeto pendurado
    private Vector3 initialOffset;  // Distância inicial entre o balão e o objeto pendurado

    void Start()
    {
        // Obter o Rigidbody do objeto pendurado
        rb = GetComponent<Rigidbody>();

        // Congelar a rotação do Rigidbody para que o objeto pendurado não gire livremente
        rb.freezeRotation = true;

        // Armazenar o offset inicial entre o balão e o objeto pendurado
        initialOffset = transform.position - balloon.position;
    }

    void FixedUpdate()
    {
        // Calcular a posição alvo com base na posição do balão + o offset inicial
        Vector3 targetPosition = balloon.position + initialOffset;

        // Calcular a distância horizontal entre o balão e o objeto pendurado
        float distanceX = targetPosition.x - transform.position.x;

        // Calcular o ângulo do pêndulo com base na distância (quanto mais o balão se move, maior o movimento do pêndulo)
        float targetAngle = Mathf.Clamp(distanceX * swingStrength, -30f, 30f);  // Limita o ângulo de oscilação

        // Interpolação suave do ângulo atual para o ângulo alvo
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, smoothSpeed * Time.deltaTime);

        // Aplicar o movimento pendular ao objeto pendurado (no eixo Z, já que estamos simulando um pêndulo)
        rb.MoveRotation(Quaternion.Euler(0, 0, currentAngle));

        // Manter o objeto pendurado preso ao balão (sem permitir que ele se desloque no eixo X e Y)
        rb.MovePosition(targetPosition);
    }
}
