using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public bool inputEnabled = true;

    public PlayerControls playerControls;

    private InputAction move;
    private InputAction interact;
    private InputAction primaryAction;
    private InputAction secondaryAction;
    private InputAction restart;
    private InputAction mousePos;
    private InputAction mouseDelta;
    private InputAction panActivateBtn;
    private InputAction zoom;

    [field: SerializeField] public Vector2 MoveDir { get; private set; }
    [field: SerializeField] public bool IsInteracting { get; private set; }
    [field: SerializeField] public bool IsHoldingPrimaryAction { get; private set; }
    [field: SerializeField] public bool IsTapPrimaryAction { get; private set; }
    [field: SerializeField] public bool IsHoldingSecondaryAction { get; private set; }
    [field: SerializeField] public bool IsTapSecondaryAction { get; private set; }
    [field: SerializeField] public bool Restart { get; private set; }
    [field: SerializeField] public Vector2 MousePos { get; private set; }
    [field: SerializeField] public Vector2 MouseDelta { get; private set; }
    [field: SerializeField] public bool IfPan { get; private set; }
    [field: SerializeField] public float Zoom { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();

        move = playerControls.Player.Move;
        interact = playerControls.Player.Interact;
        primaryAction = playerControls.Player.PrimaryAction;
        secondaryAction = playerControls.Player.SecondaryAction;
        restart = playerControls.Player.Restart;
        mousePos = playerControls.Player.MousePos;
        mouseDelta = playerControls.Player.MouseDelta;
        panActivateBtn = playerControls.Player.PanActivate;
        zoom = playerControls.Player.Zoom;
    }

    private void OnEnable()
    {
        move.Enable();
        interact.Enable();
        primaryAction.Enable();
        secondaryAction.Enable();
        restart.Enable();
        mousePos.Enable();
        mouseDelta.Enable();
        panActivateBtn.Enable();
        zoom.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        interact.Disable();
        primaryAction.Disable();
        secondaryAction.Disable();
        restart.Disable();
        mousePos.Disable();
        mousePos.Disable();
        mouseDelta.Disable();
        panActivateBtn.Disable();
        zoom.Disable();
    }

    private void Update()
    {
        if (!inputEnabled)
            return;

        MoveDir = move.ReadValue<Vector2>();
        IsInteracting = interact.WasPressedThisFrame();
        IsHoldingPrimaryAction = primaryAction.IsPressed();
        IsTapPrimaryAction = primaryAction.WasPressedThisFrame();
        IsHoldingSecondaryAction = secondaryAction.IsPressed();
        IsTapSecondaryAction = secondaryAction.WasPressedThisFrame();
        Restart = restart.WasPressedThisFrame();
        MousePos = mousePos.ReadValue<Vector2>();
        MouseDelta = -mouseDelta.ReadValue<Vector2>();
        IfPan = panActivateBtn.IsPressed();
        Zoom = Mathf.Clamp(zoom.ReadValue<float>(), -1, 1);
    }
}