using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlowMotionController : MonoBehaviour
{
    [SerializeField] private float slowMotionScale = 0.1f; // Time scale during slow motion
    [SerializeField] private float slowMotionDuration = 10f; // Duration of slow motion

    private bool isSlowMotionActive = false;

    private CustomInput input;

    private void Awake()
    {
        input = new CustomInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.SlowMo.performed += OnSlowMoPerformed; // Subscribe to SlowMo action
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.SlowMo.performed -= OnSlowMoPerformed; // Unsubscribe to SlowMo action
    }

    private void OnSlowMoPerformed(InputAction.CallbackContext context)
    {
        if (!isSlowMotionActive)
        {
            ActivateSlowMo();
        }
    }

    private void ActivateSlowMo()
    {
        isSlowMotionActive = true;
        Time.timeScale = slowMotionScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Adjust fixedDeltaTime to maintain physics consistency
        StartCoroutine(DeactivateSlowMoAfterDelay());
    }

    private IEnumerator DeactivateSlowMoAfterDelay()
    {
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        Time.timeScale = 1f; // Reset time scale
        Time.fixedDeltaTime = 0.02f; // Reset fixedDeltaTime
        isSlowMotionActive = false;
    }
}
