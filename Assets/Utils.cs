using UnityEngine;
using UnityEngine.UIElements;

public static class Utils
{
    
}


public static class TagUtils
{
    public static readonly string PLAYER_TAG = "Player";
}

public static class Overrides
{
    public static bool IsEmpty(this string str) {
        return str == null || str == string.Empty;
    }
}

public static class Validation
{
    public static bool ValidateRootElement(UIDocument document, string className) {
        if (document?.rootVisualElement == null) {
            Debug.LogError($"Missing document for {className}!!");
            return false;
        }
        return true;
    }
}
