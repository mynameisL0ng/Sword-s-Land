using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character player;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private float castDistance;
    public static bool grounded;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private Rigidbody2D body;
    private string previousState;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        grounded = true;
        player = InitPlayer.player;
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
    }

    void Update()
    {
        player.Update();
        Player_VFX();
    }
    private void FixedUpdate()
    {
        isGrounded();
    }
    private bool isGrounded()
    {
        return grounded = Physics2D.BoxCast(transform.position, groundCheckSize, 0, -transform.up, castDistance, groundLayer);
    }

    /*private void OnDrawGizmos() draw ground check;
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckSize);
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            player.PlayerDealDamage();
        }
    }
    private void PlayerPushAttackForce()
    {
        player.PlayerAttackPush();
    }
    private void Player_VFX()
    {
        string nameState;
        if ((Character.horizontalInPut > .35f || Character.horizontalInPut < -.35f) && grounded)
        {
            nameState = Character.AnimatorState.RUN.ToString();
        }
        else if(body.velocity.y > .1f)
        {
            nameState = Character.AnimatorState.JUMP.ToString();
        }
        else
        {
            nameState = Character.AnimatorState.IDLE.ToString();
        }
        if (nameState != previousState)
        {
            StateAudioClip(nameState);
            previousState = nameState;
        }
    }
    private void StateAudioClip(string nameState)
    {
        if(nameState == "IDLE")
        {
            audioSource.enabled = false;
        }
        else
        {
            for(int i = 0; i < audioClips.Length; i++)
            {
                if (audioClips[i].name == nameState)
                {
                    AudioSourceLoop(nameState);
                    audioSource.clip = audioClips[i];
                    audioSource.enabled = true;
                    audioSource.Play();
                    break;
                }
            }
        }
    }
    private void AudioSourceLoop(string nameState)
    {
        if(nameState == "RUN") audioSource.loop = true;
        else audioSource.loop = false;
    }
    public void PlayerEndAttacking()
    {
        Character.isAttacking = false;
    }
}
