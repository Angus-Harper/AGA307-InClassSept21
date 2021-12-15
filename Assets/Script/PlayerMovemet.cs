using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemet : Singleton<PlayerMovemet>
{
    public CharacterController controller;

    public int health = 100;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Audio Stuff")]
    public AudioSource footstepAudio;
    public AudioSource effectsAudio;
    public float stepRate = 0.5f;
    float stepCooldown;

    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        stepCooldown -= Time.deltaTime;
        if ((move.x != 0 || move.z != 0) && stepCooldown < 0)
        {
            footstepAudio.clip = _AM.GetFootsteps();
            footstepAudio.pitch = Random.Range(0.9f, 1.1f);
            footstepAudio.Play();
            stepCooldown = stepRate;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Hit(int _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            GameEvent.ReportPlayerDied(this.gameObject);
        }
        else
        {
            GameEvent.ReportPlayerHit(this.gameObject);
        }
    }
}
