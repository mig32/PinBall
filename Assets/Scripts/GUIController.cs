using System.Collections.Generic;
using System.Linq;
using LazyBalls;
using LazyBalls.Dialogs;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private LocalizeTextWithIntParam scoreText;
    [SerializeField] private LocalizeTextWithIntParam ballsText;
    [SerializeField] private RectTransform dialogsContainer;
    [SerializeField] private List<DialogBase> dialogList;
    
    private static GUIController _instance;
    public static GUIController Instance() => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Assert(false, "GUIController must be single");
            return;
        }

        _instance = this;
    }
    
    private void Start()
    {
        PlayerInfo.Instance().ScoreChanged += UpdateScore;
        PlayerInfo.Instance().BallsChanged += UpdateBalls;
        UpdateScore();
        UpdateBalls();
        ShowDialog(DialogType.Start);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }

        if (PlayerInfo.Instance() != null)
        {
            PlayerInfo.Instance().ScoreChanged -= UpdateScore;
            PlayerInfo.Instance().BallsChanged -= UpdateBalls;
        }
    }
    
    public void ShowDialog(DialogType type)
    {
        var prefab = dialogList.FirstOrDefault(it => it.GetDialogType() == type);
        Instantiate(prefab, dialogsContainer);
    }

    private void UpdateScore()
    {
        scoreText.SetParam(PlayerInfo.Instance().Score);
    }

    private void UpdateBalls()
    {
        ballsText.SetParam(PlayerInfo.Instance().Balls);
    }
}
