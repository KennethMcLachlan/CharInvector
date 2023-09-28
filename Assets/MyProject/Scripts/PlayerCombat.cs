using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //combat combo
    int comboCounter;
    float cooldownTime = 0.1f;
    float lastClicked;
    float lastComboEnd;

    //Character info

    [SerializeField]
    Weapon currentWeapon;
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( currentWeapon != null)
        {
            Attack(currentWeapon.weaponName);
        }
    }

    void Attack(string weapon)
    {
        //Animation Enter Bool
        if(Input.GetButtonDown("Fire1") && Time.time - lastComboEnd > cooldownTime)
        {
            comboCounter++;
            comboCounter = Mathf.Clamp(comboCounter, 0, currentWeapon.comboLength);

            //Create Attack Names

            for (int i = 0; i < comboCounter; i++)
            {
                if(i == 0)
                {
                    if (comboCounter == 1 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Movement"))
                    {
                        animator.SetBool("AttackStart", true);
                        Debug.Log("AttartStart has run");
                        animator.SetBool(/*weapon + */"Attack" + (i + 1), true);
                        Debug.Log("Attack" + i + "is true");
                        lastClicked = Time.time;
                    }
                }
                else
                {
                    if (comboCounter >= (i + 1) && animator.GetCurrentAnimatorStateInfo(0).IsName(/*weapon + */"Attack" + i))
                    {
                        animator.SetBool(/*weapon + */"Attack" + (i + 1), true);
                        animator.SetBool(/*weapon + */"Attack" + i, true);
                        lastClicked = Time.time;
                    }
                }
            }

        }

        //Animation Exit Bool Reset

        for (int i = 0; i < currentWeapon.comboLength; i++)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsName(/*weapon + */"Attack" + (i + 1)))
            {
                comboCounter = 0;
                lastComboEnd = Time.time;
                animator.SetBool(/*weapon + */"Attack" + (i + 1), false);
                animator.SetBool("AttackStart", false);
                Debug.Log("AttackStart is false");
                return;
            }
            //else
            //{
            //    animator.SetBool("AttackStart", true);
            //}

            
        }
    }
}
