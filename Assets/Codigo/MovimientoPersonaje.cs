using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidadHorizontal = 5.0f;
    [SerializeField] private float fuerzaSalto = 5.0f;
    [SerializeField] private Transform verificarPiso;
    [SerializeField] private LayerMask capaPiso;
    private Rigidbody2D cuerpoRigido;
    private Vector2 posicionInicial;
    private Quaternion rotacionInicial;

    private void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        cuerpoRigido = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        cuerpoRigido.velocity = new Vector2(horizontalInput * velocidadHorizontal, cuerpoRigido.velocity.y);
        if (Input.GetKey(KeyCode.Space) && EnElPiso())
            cuerpoRigido.velocity = new Vector2(0, fuerzaSalto);
    }

    private bool EnElPiso()
    {
        return Physics2D.OverlapCircle(verificarPiso.position, 0.2f, capaPiso);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Muerte")
            Reiniciar();
    }

    private void Reiniciar()
    {
        cuerpoRigido.velocity = Vector2.zero;
        cuerpoRigido.angularDrag = 0;
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
    }
}
