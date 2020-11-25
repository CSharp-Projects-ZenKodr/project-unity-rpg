﻿using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using Platformer.Mechanics;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class BossController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Transform player;
        public bool isFlipped = false;

        public GameObject bossHealthBar;
        public Health health;

        public Bounds Bounds => _collider.bounds;

        public float knockbackPower = 100;
        public float knockbackDuration = 1;


        public GameObject donut;//to reference the current character

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            health = GetComponent<Health>();
            donut = GameObject.Find("Donut");
            player = donut.GetComponent<CharacterSwapping>().currentCharacter.GetComponent<PlayerController>().transform;
        }

        void Start()
        {
            bossHealthBar = GameObject.Find("Boss Health Bar");
            bossHealthBar.SetActive(false);
        }


        // Old OnCollisionEnter2D
        // void OnCollisionEnter2D(Collision2D collision)
        // {
        //     var player = collision.gameObject.GetComponent<PlayerController>();

        //     if (player != null)
        //     {
        //         var ev = Schedule<PlayerEnemyCollision>();
        //         ev.player = player;
        //         ev.enemy = this;
        //     }
        // }

        public void HealthDecrement(int damage)
        {
            health.Decrement(damage);
            //BossHealthBar.transform.GetChild(0).GetComponent<EnemyHPBar>().SetCurrentHealth(health.currentHP);

            if (health.currentHP == 0)
            {
                Destroy(gameObject);
            }
        }

        public void LookAtPlayer()
        {
            //bossHealthBar.SetActive(true);

            //player = GameObject.FindGameObjectWithTag("Player").transform;
            player = donut.GetComponent<CharacterSwapping>().currentCharacter.GetComponent<PlayerController>().transform;
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }

        void Update()
        {
            //Debug.Log(donut.GetComponent<CharacterSwapping>().currentCharacter.name);
            //Debug.Log("hello");
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
        }
    }
}