using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RebindUI : MonoBehaviour
{
    public InputActionReference inputAction;
    public int bindingIndex;
    public TextMeshProUGUI displayText;

    private InputActionRebindingExtensions.RebindingOperation _rebindOperation;

    void Start()
    {
        UpdateDisplay();
    }

    public void StartRebind()
    {
        inputAction.action.actionMap.Disable();
        displayText.text = "Press a key...";

        _rebindOperation = inputAction.action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse/position")
            .OnComplete(operation =>
            {
                operation.Dispose();
                inputAction.action.actionMap.Enable();
                UpdateDisplay();
                SaveBindings();
            })
            .Start();
    }

    void UpdateDisplay()
    {
        displayText.text =
            InputControlPath.ToHumanReadableString(
                inputAction.action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    void SaveBindings()
    {
        PlayerPrefs.SetString(
            "rebinds",
            inputAction.action.actionMap.SaveBindingOverridesAsJson());
    }
}