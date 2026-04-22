using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public EnemyData dadosInimigo;
    public float distanciaPatrulha = 3f;
    private Vector3 posicaoInicial;
    private int direcao = 1;

    void Start() => posicaoInicial = transform.position;

    void Update()
    {
        transform.Translate(Vector3.right * direcao * dadosInimigo.velocidade * Time.deltaTime);

        if (Vector3.Distance(posicaoInicial, transform.position) > distanciaPatrulha)
        {
            direcao *= -1; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Inimigo tirou uma vida do Player!");
        }
    }
}