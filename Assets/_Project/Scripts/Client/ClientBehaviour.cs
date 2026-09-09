using UnityEngine;
using PLIbox.Extensions;
using System.Collections.Generic;
using System.Collections;

public class ClientBehaviour : MonoBehaviour
{
    [Header("Anomalies")]
    [SerializeField] private ClientVisualAnomalies _visualAnomalies;
    [SerializeField] private ClientSoundDifferences _soundAnomalies;
    [SerializeField] private ClientDialog _clientDialog;

    [Space(5)]
    [SerializeField] private List<FakeClientSO> _fakeClientList;

    [Header("Movements")]
    [SerializeField] private float _moveDuration = 2f;

    private float _gameWidth;

    private FakeClientSO _fakeClientSO = null;

    public bool IsFakeClient
    {
        get;
        private set;
    }

    private void Awake()
    {
        //Choose if is fake or not
        IsFakeClient = RandomExtensions.RandomBool();
        if (IsFakeClient)
            _fakeClientSO = _fakeClientList.GetRandomItem();

        _visualAnomalies.SetupSprite(_fakeClientSO ? _fakeClientSO.InvalidVisual : null);
        if (_fakeClientSO != null && _fakeClientSO.InvalidSound.Sound != null)
            _soundAnomalies.TriggerSoundEffect(_fakeClientSO.InvalidSound);
        else
            _soundAnomalies.TriggerSoundEffect();

        // largeur du jeu (avec la cam)
        Camera cam = Camera.main;
        float height = cam.orthographicSize;
        _gameWidth = height * cam.aspect;
    }

    public void OnMouseDown()
    {
        TriggerClientDialog();
    }

    public void TriggerClientDialog() => _clientDialog.PlayDialog(_fakeClientSO ? _fakeClientSO.InvalidDialog : null);

    public void Move(bool IsEntry)
    {
        StartCoroutine(MoveClient(IsEntry));
    }

    private IEnumerator MoveClient(bool IsEntry)
    {
        if (ClientPlacementPoint.Instance == null)
        {
            EndAnimation();
            yield break;
        }

        Transform ClientTargetPoint = ClientPlacementPoint.Instance.transform;

        float leftX = Camera.main.transform.position.x - _gameWidth;
        float rightX = Camera.main.transform.position.x + _gameWidth;

        float startingX = IsEntry ? leftX : ClientTargetPoint.position.x;
        Vector2 startPoint = new Vector2(startingX, ClientTargetPoint.position.y);

        float targetX = IsEntry ? ClientTargetPoint.position.x : rightX;
        Vector2 targetPoint = new Vector2(targetX, ClientTargetPoint.position.y);

        float time = 0;
        while (time < _moveDuration)
        {
            time += Time.deltaTime;
            float t = time / _moveDuration;

            transform.position = Vector2.Lerp(startPoint, targetPoint, t);

            yield return null;
        }
        EndAnimation();

        void EndAnimation()
        {
            if (!IsEntry)
                Destroy(gameObject);
            else
                TriggerClientDialog();
        }
    }
}