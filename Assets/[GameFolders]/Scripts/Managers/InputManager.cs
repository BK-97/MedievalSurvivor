using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    #region Params
    GameInputs input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 mousePos;
    public LayerMask groundLayer;
    private bool isAttacking;
    #endregion
    public static UnityEvent OnPassiveSkillInput = new UnityEvent();
    public static UnityEvent OnWeaponSkillInput = new UnityEvent();
    public static UnityEvent OnInteractInput = new UnityEvent();
    #region MonoBehaviourMethods
    private void Awake()
    {
        input = new GameInputs();
    }
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(AddInputListeners);
        GameManager.Instance.OnStageFail.AddListener(RemoveInputListeners);
        GameManager.Instance.OnStageSuccess.AddListener(RemoveInputListeners);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(AddInputListeners);
        GameManager.Instance.OnStageFail.RemoveListener(RemoveInputListeners);
        GameManager.Instance.OnStageSuccess.RemoveListener(RemoveInputListeners);
    }
    private void AddInputListeners()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Rotate.performed += SetMousePos;
        input.Player.Attack.performed += OnAttackPerformed;
        input.Player.Attack.canceled += OnAttackCancelled;
        input.Player.WeaponChange.performed += ctx => WeaponController.OnWeaponChange.Invoke();
        input.Player.PassiveSkill.performed += ctx => OnPassiveSkillInput.Invoke();
        input.Player.WeaponSkill.performed += ctx => OnWeaponSkillInput.Invoke();
        input.Player.Interact.performed += ctx => OnInteractInput.Invoke();
    }
    private void RemoveInputListeners()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.Rotate.performed -= SetMousePos;
        input.Player.Attack.performed -= OnAttackPerformed;
        input.Player.Attack.canceled -= OnAttackCancelled;
        input.Player.WeaponChange.performed -= ctx => WeaponController.OnWeaponChange.Invoke();
        input.Player.PassiveSkill.performed -= ctx => OnPassiveSkillInput.Invoke();
        input.Player.WeaponSkill.performed -= ctx => OnWeaponSkillInput.Invoke();
        input.Player.Interact.performed -= ctx => OnInteractInput.Invoke();
    }
    private void Update()
    {
        if(GameManager.Instance.IsGameStarted&&!LevelManager.Instance.IsLevelStarted)
        {
            if (Input.GetMouseButtonDown(0))
                LevelManager.Instance.StartLevel();
        }
    }
    #endregion
    #region SetMethods
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
    private void SetMousePos(InputAction.CallbackContext value)
    {
        mousePos = value.ReadValue<Vector2>();
    }
    private void OnAttackPerformed(InputAction.CallbackContext value)
    {

        isAttacking = true;
    }
    private void OnAttackCancelled(InputAction.CallbackContext value)
    {

        isAttacking = false;
    }

    #endregion
    #region GetMethods
    public Vector3 GetMouseWorldPos()
    {
        Vector3 mouseWorldPos = Utilities.GetMouseWorldPosition(groundLayer);
        return mouseWorldPos;
    }
    public Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        return direction;
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }
    #endregion

}
