using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _singleton;

    public static bool IsInstanceValid => _singleton != null;

    public static T Instance
    {
        get
        {
            EnsureInstanceIsValid();
            return _singleton;
        }
    }

    private static void EnsureInstanceIsValid()
    {
        if (!IsInstanceValid)
        {
            RegisterSingleton(FindAnyObjectByType<SingletonMono<T>>(FindObjectsInactive.Include));
            // No lazy initialization supported, you are responsible for creating the instance yourself.
        }
    }

    [SerializeField] private bool _immortal = true;

    protected virtual void Awake()
    {
        TryToRegisterMyself();
        RemoveMyselfIfDuplicated();
    }

    private void TryToRegisterMyself()
    {
        if (!IsInstanceValid)
        {
            RegisterSingleton(this);
        }
    }

    private static void RegisterSingleton(SingletonMono<T> newInstance)
    {
        if (newInstance == null) return;
        _singleton = (T)(MonoBehaviour)newInstance;
        newInstance.Init();
        if (newInstance._immortal)
        {
            DontDestroyOnLoad(newInstance.gameObject);
        }
    }

    public abstract void Init();

    private void RemoveMyselfIfDuplicated()
    {
        if (_singleton != this)
        {
            Destroy(gameObject);
        }
    }
}