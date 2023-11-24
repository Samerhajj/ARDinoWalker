using UnityEngine;
using UnityEngine.SceneManagement;
using Wikitude;

public class SampleController : MonoBehaviour
{
    protected virtual void Start() {
        QualitySettings.shadowDistance = 60.0f;
    }

    public virtual void OnBackButtonClicked() {
        SceneManager.LoadScene("Main Menu");
    }

    // URLResource events
    public virtual void OnFinishLoading() {
        Debug.Log("URL Resource loaded successfully.");
    }

    public virtual void OnErrorLoading(Error error) {
        PrintError("Error loading URL Resource!", error);
    }

    // Tracker events
    public virtual void OnTargetsLoaded() {
        Debug.Log("Targets loaded successfully.");
    }

    public virtual void OnErrorLoadingTargets(Error error) {
        PrintError("Error loading targets!", error);
    }

    public virtual void OnCameraError(Error error) {
        PrintError("Camera Error!", error);
    }

    protected virtual void Update() {
        // Also handles the back button on Android
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnBackButtonClicked();
        }
    }

    protected void PrintError(string message, Error error) {
        Debug.LogError(message + "\nErrorCode: " + error.Code + "\nErrorDomain" + error.Domain + "\nErrorMessage: " + error.Message);
    }
}
