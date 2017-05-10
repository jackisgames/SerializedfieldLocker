using Helpers;
using UnityEngine;

class LockExample:MonoBehaviour
{
    [Header("Magic Numbers Please do not touch!")]
    [SerializeField]
    private int noLockVariable;
    [SerializeField,Lock]
    private int noPinLockedVariable;
    [SerializeField,Lock("111")]
    private int withPinLockedVariable;
}