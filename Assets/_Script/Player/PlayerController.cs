using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    public Character player;
    public Vector3 lastPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private float castDistance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    public static bool grounded;
    public GameObject damagePopUp;
    enum VFX_State {RUN , JUMP, JUMPLAND};
    VFX_State nameVFX;
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

/*    private void OnDrawGizmos() draw groundBoxCast
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckSize);
    }*/

    private void OnTriggerEnter2D(Collider2D other) // player need attack push can deal damage.
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Monster monster = other.GetComponent<MonsterController>().initMonster.monster;
            Debug.Log(other.transform.localPosition);
            GameObject newDamagePopUp =  Instantiate(damagePopUp, other.transform.localPosition, Quaternion.identity);
            newDamagePopUp.transform.position = new Vector2(other.transform.localPosition.x, other.transform.localPosition.y + 1f);
            Debug.Log(newDamagePopUp.transform.position);
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
        if ((player.horizontalInPut > .5f || player.horizontalInPut < -.5f) && animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            nameState = VFX_State.RUN.ToString();
        }
        else if(body.velocity.y > .1f && animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            nameState = VFX_State.JUMP.ToString();
        }
        else if(grounded && body.velocity.y < -.01f)
        {
            nameState = VFX_State.JUMPLAND.ToString();
        }
        else
        {
            nameState = "";
        }
        if (nameState != previousState)
        {
            StateAudioClip(nameState);
            previousState = nameState;
        }
    }
    private void StateAudioClip(string nameState)
    {
        if(nameState == "")
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
        player.isAttacking = false;
    }
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }
    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}
