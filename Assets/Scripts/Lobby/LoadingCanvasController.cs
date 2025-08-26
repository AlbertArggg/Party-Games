using Other;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _cancelButton;
    private NetworkRunnerController _networkRunnerController;

    private void Start()
    {
        _networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
        _networkRunnerController.OnStartRunnerConnection += OnStartedRunnerConnection;
        _networkRunnerController.OnPlayerJoinedSuccessfully += OnPlayerJoinedSuccessfully;
        _cancelButton.onClick.AddListener(() => _networkRunnerController.Shutdown());
        gameObject.SetActive(false);
    }
    
    private void OnStartedRunnerConnection()
    {
        gameObject.SetActive(true);
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _animator, "In", true));
    }

    private void OnPlayerJoinedSuccessfully()
    {
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _animator, "Out", false));
    }

    private void OnDestroy()
    {
        _networkRunnerController.OnStartRunnerConnection -= OnStartedRunnerConnection;
        _networkRunnerController.OnPlayerJoinedSuccessfully -= OnPlayerJoinedSuccessfully;
    }
}
