using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    public Character player;
    public Vector3 lastPosition;
    public float nerfDamagePercent; // will attack player can deal on enemy

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private float castDistance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;


    public AudioClip audioSkillDefautl;
    public static bool grounded;
    public GameObject damagePopUp;
    public GameObject effectLvlUp;

    enum VFX_State {RUN , JUMP, JUMPLAND, TAKEHIT};
    VFX_State nameVFX;
    public string nameState;

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
        float damageRange = Random.Range(player.attackDamage - (player.attackDamage * nerfDamagePercent), player.attackDamage);
        if(other.gameObject.CompareTag("Enemy"))
        {
            Monster monster = other.GetComponent<MonsterController>().initMonster.monster;
            if (monster != null)
            {
                if (isHeavyAttack)
                {
                    monster.TakeHit(player.attackDamage * 3.5f);
                    damagePopUp.GetComponent<TextMeshPro>().text = (player.attackDamage * 3.5).ToString();
                    isHeavyAttack = false;
                }
                else
                {
                    monster.TakeHit(damageRange);
                    damagePopUp.GetComponent<TextMeshPro>().text = ((int)damageRange).ToString();
                }
                Damage_PopUp(other.transform.position);
            }
        }
    }
    void Damage_PopUp(Vector2 EnemyPosition)
    {
        Vector2 popupPosition = EnemyPosition;
        popupPosition.x = Random.Range(EnemyPosition.x - 1.5f, EnemyPosition.x + 1.5f);
        popupPosition.y = Random.Range(EnemyPosition.y - 1.5f, EnemyPosition.y + 1.5f);
        Instantiate(damagePopUp, popupPosition, Quaternion.identity);
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

    public void IsNotHeavyAttack()
    {
        isHeavyAttack = false;
    }

    private void PlayerPushAttackForce()
    {
        player.PlayerAttackPush();
    }
    private void Player_VFX()
    {
        if ((player.horizontalInPut > .5f || player.horizontalInPut < -.5f) && animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            nameState = VFX_State.RUN.ToString();
        }
        else if(body.velocity.y > .1f && animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            nameState = VFX_State.JUMP.ToString();
        }
        else if(grounded && player.horizontalInPut == 0)
        {
            nameState = VFX_State.JUMPLAND.ToString();
        }
        else
        {
            nameState = "";
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("TakeHit"))
        {
            nameState = VFX_State.TAKEHIT.ToString();
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

    public void AudioSkillDefault() // warrior skill default audio
    {
        audioSource.enabled = true;
        audioSource.loop = false;
        audioSource.clip = audioSkillDefautl;
        audioSource.Play();
    }
    public void EffectLevelUp()
    {
        Instantiate(effectLvlUp, transform.position, Quaternion.identity);
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
