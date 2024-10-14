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
    private Animator animator;
    private string previousState;
    private bool isHeavyAttack;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        grounded = true;
        player = InitPlayer.player;
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        isHeavyAttack = false;
    }

    void Update()
    {
        player.Update();
        Player_VFX();
    }
    private void FixedUpdate()
    {
        IsHeavyAttack();
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

    private void OnTriggerEnter2D(Collider2D other) // player need attack push can deal damage.
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Monster monster = other.GetComponent<MonsterController>().initMonster.monster;
            if(monster != null)
            {
                if (isHeavyAttack)
                {
                    monster.TakeHit(player.attackDamage * 3.5f);
                    isHeavyAttack = false;
                }
                else
                    monster.TakeHit(player.attackDamage);
            }
        }
    }
    private void IsHeavyAttack()
    {
        if(InitPlayer.isWarrior)
        {
            if (Input.GetButton("HeavyAttack") && Warrior.holdTime < Warrior.requireHoldTime)
            {
                Warrior.holdTime += Time.deltaTime;
                if (Warrior.holdTime >= Warrior.requireHoldTime)
                {
                    isHeavyAttack = true;
                }
            }
        }
    }
    private void PlayerPushAttackForce()
    {
        player.PlayerAttackPush();
    }
    private void Player_VFX()
    {
        string nameState;
        if ((Character.horizontalInPut > .5f || Character.horizontalInPut < -.5f) && grounded)
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
