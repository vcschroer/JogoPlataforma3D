using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private PlayerData config;
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private int vidasAtuais;

    public float jumpForce = 7f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        CarregarConfiguracoes();
    }

    void CarregarConfiguracoes()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "player_config.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            config = JsonUtility.FromJson<PlayerData>(json);

            transform.localScale = Vector3.one * config.escalaMultiplicador;
            vidasAtuais = config.maxVidas;
            Debug.Log($"Configurações carregadas. Vidas iniciais: {vidasAtuais}");
        }
    }

    public void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (config != null)
        {
            rb.linearVelocity = new Vector3(moveInput.x * config.moveSpeed, rb.linearVelocity.y, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataform") || collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            PerderVida();
        }
    }

    private void PerderVida()
    {
        vidasAtuais--;
        Debug.Log("O Player perdeu uma vida! Vidas restantes: " + vidasAtuais);

        if (vidasAtuais <= 0)
        {
            Debug.Log("Vidas esgotadas. Reiniciando cena...");
            RecarregarCena();
        }
    }

    private void RecarregarCena()
    {
        Scene cenaAtiva = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtiva.name);
    }
}