using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ComponentsExtensions
{
    public static T GetAddComponent<T>(this GameObject gameObject) where T: Component => 
        gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();

    public static Rigidbody GetRigidbody(this GameObject gameObject) => 
        gameObject.GetAddComponent<Rigidbody>();

    public static T RandomListElement<T>(this List<T> list) => 
        list[Random.Range(0, list.Count)];

    public static T RandomArrayElement<T>(this T[] array) => 
        array[Random.Range(0, array.Length)];

    public static void ChangeAlpha(this Image image, float alpha) => 
        image.color = new Color(image.color.r,image.color.g,image.color.b,alpha);
}
