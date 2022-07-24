using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CommonFuncs
{
    // parent�����̎q�I�u�W�F�N�g��foreach���[�v�Ŏ擾����
    public static Transform[] GetChildren(this Transform parent)
    {
        // �q�I�u�W�F�N�g��ԋp����z��쐬
        var children = new Transform[parent.childCount];
        var childIndex = 0;

        // �q�I�u�W�F�N�g�����Ԃɔz��Ɋi�[
        foreach (Transform child in parent)
        {
            children[childIndex++] = child;
        }

        // �q�I�u�W�F�N�g���i�[���ꂽ�z��
        return children;
    }

    // parent�����̎q�I�u�W�F�N�gLinq�ōċA�I�ɗ񋓂���
    public static IEnumerable<Transform> EnumChildrenRecursive(this Transform parent)
    {
        return parent
            .GetComponentsInChildren<Transform>() // �e���܂ގq���ċA�I�Ɏ擾
            .Skip(1); // �e���X�L�b�v����
    }

    /// <summary>
    /// �w�肵�������񂩂�w�肵��������S�č폜����
    /// </summary>
    /// <param name="s">�ΏۂƂȂ镶����B</param>
    /// <param name="characters">�폜���镶���̔z��B</param>
    /// <returns>s�Ɋ܂܂�Ă���S�Ă�targetStrs�������폜���ꂽ������B</returns>
    public static string RemoveChars(string s, string[] remStrs)
    {
        System.Text.StringBuilder buf = new System.Text.StringBuilder(s);
        foreach (string remStr in remStrs)
        {
            buf.Replace(remStr.ToString(), "");
        }
        return buf.ToString();
    }

    // 2����List[i][j]�̓���v�f��������i�̒l��Ԃ�
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
