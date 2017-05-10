#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


internal class LockAttribute : PropertyAttribute
{
    internal string Pin;

    internal LockAttribute()
    {
        Pin = string.Empty;
    }

    internal LockAttribute(string pin = "")
    {
        Pin = pin;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LockAttribute))]
class LockAttributeDrawer : PropertyDrawer
{
    private bool isUnlocked = false;
    private bool enterPin = false;
    private string passErrorMessage = string.Empty;
    private string passcode;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (enterPin)
            EnterPinGUI(position, property, label);
        else
            LockGUI(position, property, label);
        //GUI.Box(position, "Locked");
        //base.OnGUI(position, property, label);
    }

    private void EnterPinGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect buttonSubmitRect = new Rect(position);
        Rect buttonCancelRect = new Rect(position);

        position.width *= .7f;

        buttonCancelRect.x += position.width;
        buttonCancelRect.width = (buttonCancelRect.width - position.width)*.5f;

        buttonSubmitRect.x = buttonCancelRect.x + buttonCancelRect.width;
        buttonSubmitRect.width = buttonCancelRect.width;

        passcode = EditorGUI.PasswordField(position, string.Format("Passcode {0}", passErrorMessage), passcode);
        if (GUI.Button(buttonCancelRect, "Cancel"))
        {
            enterPin = false;
        }
        if (GUI.Button(buttonSubmitRect, "Submit"))
        {
            if (passcode.Equals(PIN))
            {
                isUnlocked = true;
                enterPin = false;
            }
            else
            {
                passErrorMessage = "Wrong password";
            }
        }
    }

    private void LockGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect buttonRect = new Rect(position);

        position.width *= .85f;

        buttonRect.x += position.width;
        buttonRect.width = buttonRect.width - position.width;

        GUI.enabled = isUnlocked;

        EditorGUI.PropertyField(position, property);

        GUI.enabled = true;
        if (GUI.Button(buttonRect, isUnlocked ? "Lock" : "Unlock"))
        {
            if (string.IsNullOrEmpty(PIN))
                isUnlocked = !isUnlocked;
            else
                enterPin = true;
            passErrorMessage = passcode = string.Empty;

        }
    }

    private string PIN
    {
        get { return ((LockAttribute) attribute).Pin; }

    }
}
#endif

