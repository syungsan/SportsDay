using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CommonFuncs
{
    // parent直下の子オブジェクトをforeachループで取得する
    public static Transform[] GetChildren(this Transform parent)
    {
        // 子オブジェクトを返却する配列作成
        var children = new Transform[parent.childCount];
        var childIndex = 0;

        // 子オブジェクトを順番に配列に格納
        foreach (Transform child in parent)
        {
            children[childIndex++] = child;
        }

        // 子オブジェクトが格納された配列
        return children;
    }

    // parent直下の子オブジェクトLinqで再帰的に列挙する
    public static IEnumerable<Transform> EnumChildrenRecursive(this Transform parent)
    {
        return parent
            .GetComponentsInChildren<Transform>() // 親を含む子を再帰的に取得
            .Skip(1); // 親をスキップする
    }

    /// <summary>
    /// 指定した文字列から指定した文字を全て削除する
    /// </summary>
    /// <param name="s">対象となる文字列。</param>
    /// <param name="characters">削除する文字の配列。</param>
    /// <returns>sに含まれている全てのtargetStrs文字が削除された文字列。</returns>
    public static string RemoveChars(string s, string[] remStrs)
    {
        System.Text.StringBuilder buf = new System.Text.StringBuilder(s);
        foreach (string remStr in remStrs)
        {
            buf.Replace(remStr.ToString(), "");
        }
        return buf.ToString();
    }

    // 2次元List[i][j]の特定要素を検索しiの値を返す
    public static int SearchIndexInList2D(List<List<GameObject>> list2d, GameObject target)
    {
        int ret = 0;

        for (int i = 0; i < list2d.Count; i++)
        {
            if (list2d[i].Contains(target))
            {
                ret = i;
                break;
            }  
        }
        return ret;
    }
}
