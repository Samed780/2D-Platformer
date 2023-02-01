using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    public UnityEvent<float, Vector2> healthDamage;
    public UnityEvent death;
    public UnityEvent<float, float> healthChanged;

    Animator animator;

    [SerializeField] float _maxHealth = 100;
    public float MaxHealth { 
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField] float _currentHealth;
    public float CurrentHealth { 
        get { return _currentHealth; }
        set 
        { 
            _currentHealth = value;
            healthChanged?.Invoke(_currentHealth, _maxHealth);
            if (_currentHealth <= 0)
                IsAlive = false;
        }
    }

    bool _isAlive = true;
    public bool IsAlive { 
        get { return _isAlive; }
        set { 
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);

            if(!value)
                death.Invoke();
        }
    }

    bool isInvincible = false;

    [SerializeField] float timeSinceHit = 0f, invincibilityTime = 0.25f;

    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set { animator.SetBool(AnimationStrings.lockVelocity, value); }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

    }

    public void TakeDamage(float damage, Vector2 pushback)
    {
        if(IsAlive && !isInvincible)
        {
            CurrentHealth -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hit);
            LockVelocity = true;
            healthDamage?.Invoke(damage, pushback);
            CharacterEvent.characterDamaged.Invoke(gameObject, damage);
        }
    }

    public bool Heal(float healAmount)
    {
        if (IsAlive && CurrentHealth < MaxHealth)
        {
            float maxHeal = Mathf.Max(MaxHealth - CurrentHealth, 0);
            float actualHeal = Mathf.Min(maxHeal, healAmount);
            CurrentHealth += actualHeal;
            CharacterEvent.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        else
            return false;

    }
}
