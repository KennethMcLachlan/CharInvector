using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicCombo : MonoBehaviour
{
    private Animator _anim;
    public float cooldownTime = 2f;
    private float nextAttackTime = 0f;
    public static int numOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        CooldownSystem();

    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        numOfClicks++;
        if (numOfClicks == 1)
        {
            _anim.SetBool("Attack1", true);
        }

        numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);

        if (numOfClicks >= 2 && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            _anim.SetBool("Attack1", false);
            _anim.SetBool("Attack2", true);
        }

        if (numOfClicks >= 3 && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            _anim.SetBool("Attack2", false);
            _anim.SetBool("Attack3", true);
        }

    }

    private void CooldownSystem()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            _anim.SetBool("Attack1", false);
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            _anim.SetBool("Attack2", false);
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            _anim.SetBool("Attack3", false);
            numOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numOfClicks = 0;
        }

        if (Time.time > nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }
}
