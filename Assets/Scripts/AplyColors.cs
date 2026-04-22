using UnityEngine;

public class ApplyColors : MonoBehaviour
{
    [Header("Configuração de Dados")]
    public ColorData baseDeCores; 

    public enum TipoObjeto { Plataforma1, Plataforma2, Plataforma3, Fundo }
    [Header("Selecione o Tipo deste Objeto")]
    public TipoObjeto tipo;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            switch (tipo)
            {
                case TipoObjeto.Plataforma1:
                    renderer.material.color = baseDeCores.corPlataforma1;
                    break;
                case TipoObjeto.Plataforma2:
                    renderer.material.color = baseDeCores.corPlataforma2;
                    break;
                case TipoObjeto.Plataforma3:
                    renderer.material.color = baseDeCores.corPlataforma3;
                    break;
                case TipoObjeto.Fundo:
                    renderer.material.color = baseDeCores.corFundo;
                    break;
            }
        }
    }
}