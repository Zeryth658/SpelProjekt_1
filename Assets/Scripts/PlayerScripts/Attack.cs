using UnityEngine;
using UnityEngine.InputSystem;
public class Attack : MonoBehaviour
{
    public GameObject melee;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;
    private PlayerInput playerInput;
    private InputAction atkAction;

    private void Awake()
    {
        atkAction = playerInput.actions["Attack"];
    }

    // Update is called once per frame
    private void Update()
    {
        CheckMeleeTimer();

        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
        {
            OnAttack();
        }
    }

    private void OnAttack()
    {
        if (!isAttacking)
        {
            melee.SetActive(true);
            isAttacking = true;
        }
    }

    private void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking = false;
                melee.SetActive(false);
            }
        }
    }
}
