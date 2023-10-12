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
    #region Events
    public static UnityEvent OnPassiveSkillInput = new UnityEvent();
    public static UnityEvent OnWeaponSkillInput = new UnityEvent();
    public static UnityEvent OnInteractInput = new UnityEvent();
    public static UnityEvent OnRollOverInput = new UnityEvent();
    #endregion
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
        input.Player.RollOver.performed += ctx => OnRollOverInput.Invoke();
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
        input.Player.RollOver.performed -= ctx => OnRollOverInput.Invoke();

    }

    #endregion
    #region SetMethods
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        moveVector = Vector2.zero;
    }
    private void SetMousePos(InputAction.CallbackContext value)
    {
        mousePos = value.ReadValue<Vector2>();
    }
    private void OnAttackPerformed(InputAction.CallbackContext value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        isAttacking = true;
    }
    private void OnAttackCancelled(InputAction.CallbackContext value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
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
