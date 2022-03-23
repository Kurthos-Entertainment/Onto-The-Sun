using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyEventSystem;

public class Decay : MonoBehaviour
{
    public float Condition = 100f;
    public AudioClip DestroySound;
    public AudioClip ScrubSound;
    public Color MaxConditionColor = Color.white;
    public Color MinConditionColor = Color.black;
    public ParticleSystem BreakEffect;
    public ParticleSystem ScrubEffect;

    [SerializeField] private GameObject blink;
    [SerializeField] private float blinkDurationSeconds = 0.1f;
    [SerializeField] private float defaultDecay;
    [SerializeField] private float unstableHintRatio = 0.2f;

    private AudioSource audioSource;
    private new BoxCollider2D collider;
    private GameState gameState;
    private new Renderer renderer;
    private Text text;
    private SpriteRenderer sprite;
    private bool isDead = false;
    private bool isBlinking = false;

    private void Awake()
    {
        Debug.Assert(BreakEffect != null);
        Debug.Assert(DestroySound != null);
        Debug.Assert(ScrubEffect != null);
        Debug.Assert(ScrubSound != null);

        Events.onGameOver.AddListener(HandleGameOver);
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        Debug.Assert(gameState != null);

        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        defaultDecay = Random.Range(gameState.DecayMinInitial, gameState.DecayMaxInitial);

        //text = GetComponentInChildren<Text>();
        //Debug.Assert(text != null);
    }

    private void Update()
    {
        ApplyDecay();
        CheckCondition();
        if (text != null)
        {
            text.text = Condition.ToString();
        }
    }

    private void ApplyDecay()
    {
        Condition = Mathf.Lerp(Condition, Condition - defaultDecay * gameState.DecayMultiplicator, Time.deltaTime);
        renderer.material.SetColor("_Color", Color.Lerp(MinConditionColor, MaxConditionColor, Condition / gameState.BeamMaxCondition));
    }

    private IEnumerator Blink()
    {
        blink.SetActive(true);
        yield return new WaitForSeconds(blinkDurationSeconds);
        blink.SetActive(false);
        yield return new WaitForSeconds(blinkDurationSeconds);
        isBlinking = false;
    }

    private void CheckCondition()
    {
        if (Condition <= 0f)
        {
            if (!isDead)
            {
                StopCoroutine("Blink");
                blink.SetActive(false);
                isDead = true;
                collider.enabled = false;
                sprite.enabled = false;
                BreakEffect.Play();
                audioSource.PlayOneShot(DestroySound);
                Destroy(gameObject, 5f);
            }
        }
        else if ((Condition / gameState.BeamMaxCondition) < unstableHintRatio)
        {
            if (!isBlinking)
            {
                isBlinking = true;
                StartCoroutine("Blink");
            }
        }
        else
        {
            StopCoroutine("Blink");
            blink.SetActive(false);
            isBlinking = false;
        }
    }

    private void Scrub()
    {
        if (Input.GetMouseButton(0))
        {
            ScrubEffect.Play();
            audioSource.PlayOneShot(ScrubSound);
            Condition = Mathf.Clamp(Condition + gameState.ScrubRepair, 0, gameState.BeamMaxCondition);
            Events.onBeamScrubbed.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D collisionRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collisionRigidbody != null)
        {
            if (collisionRigidbody.velocity.magnitude > 0.3f)
            {
                Condition -= collisionRigidbody.velocity.magnitude * 50;
            }
        }
    }

    private void OnMouseEnter()
    {
        Scrub();
    }

    private void HandleGameOver()
    {
        enabled = false;
    }

    private void OnDestroy()
    {
        Events.onGameOver.RemoveListener(HandleGameOver);
    }
}