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
    [SerializeField] private float _fadeInDuration = 0.3f;
    [SerializeField] private float _timeBetweenCustomers = 0.5f;

    private float _gameWidth;

    private FakeClientSO _fakeClientSO = null;

    public bool HasDoneMoving
    {
        get;
        private set;
    }

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
        if (PauseManager.IsPaused)
            return;

        TriggerClientDialog();
    }

    public void TriggerClientDialog() => _clientDialog.PlayDialog(_fakeClientSO ? _fakeClientSO.InvalidDialog : null);

    public void Move(bool IsEntry, bool accepted = true)
    {
        //Shut up if goes out
        if (!IsEntry)
            _soundAnomalies.StopSoundEffect();

        StartCoroutine(MoveClient(IsEntry, accepted));
    }

    private IEnumerator MoveClient(bool IsEntry, bool accepted = true)
    {
        if (ClientPlacementPoint.Instance == null)
        {
            EndAnimation();
            yield break;
        }

        //yield return new WaitForSeconds(0.6f);

        HasDoneMoving = false;
        Transform ClientTargetPoint = ClientPlacementPoint.Instance.transform;

        float leftX = Camera.main.transform.position.x - _gameWidth;
        float rightX = Camera.main.transform.position.x + _gameWidth;

        Vector2 startPoint;


        startPoint = ClientTargetPoint.position;

        float targetX = accepted ? leftX : rightX;
        Vector2 targetPoint = new Vector2(targetX, ClientTargetPoint.position.y);

        // arrivée
        SpriteRenderer visual = GetComponentInChildren<SpriteRenderer>();
        Vector3 startingScale = new Vector3(transform.localScale.x - 0.2f, transform.localScale.x - 0.2f, transform.localScale.x - 0.2f);
        Vector3 targetScale = transform.localScale;


        if (IsEntry)
        {
            transform.position = startPoint;
            yield return new WaitForSeconds(_timeBetweenCustomers);
        }

        float time = 0;
        while (time < _moveDuration)
        {
            time += Time.deltaTime;
            float t = time / _moveDuration;

            

            if (IsEntry && time < _fadeInDuration)
            {
                float d = time / _fadeInDuration;

                if (visual != null)
                {
                    Color c = visual.color;
                    c.a = Mathf.Lerp(0, 1, d);
                    visual.color = c;
                }
                transform.localScale = Vector3.Lerp(startingScale, targetScale, d);
            }
            else if (!IsEntry)
            {
                transform.position = Vector2.Lerp(startPoint, targetPoint, t);
            }
            else if (IsEntry && time >= _fadeInDuration)
                EndAnimation();


            yield return null;
        }
        EndAnimation();

        void EndAnimation()
        {
            HasDoneMoving = true;
            if (!IsEntry)
                Destroy(gameObject);
            else
                TriggerClientDialog();
        }
    }
}