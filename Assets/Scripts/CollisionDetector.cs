using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("O Personagem colidiu com o objeto: " + collision.gameObject.name);

        if (collision.gameObject.tag == "Plataform")
        {
            Debug.LogWarning("Plataforma detectada via Colisor.");
        }
    }
}
