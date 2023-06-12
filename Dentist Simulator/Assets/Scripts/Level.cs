using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private OralCavity _oralCavity;
    [SerializeField] private OralCavityChanger _oralCavityChanger;
    [SerializeField] private GameObject _nextPanel;
    [SerializeField] private GameClient _gameClient;

    public ServerOptions Options;

    private void Start()
    {
        _gameClient = new GameClient(Options);
        PrepareDiseases();
    }

    private void OnEnable()
    {
        _oralCavity.OnWon += Win;
    }

    private void OnDisable()
    {
        _oralCavity.OnWon -= Win;
    }

    private void PrepareDiseases()
    {
        _oralCavity.Prepare();
    }

    private async void Win()
    {
        _oralCavityChanger.Change();
        await _gameClient.InformLevelFinished();
        _nextPanel.SetActive(true);
    }
}
