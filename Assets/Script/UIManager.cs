using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform lifeTran;

    [SerializeField]
    private GameObject lifePrefab;

    [SerializeField]
    private List<GameObject> lifesList = new List<GameObject>();

    [SerializeField]
    private GameObject playerInfoSet;


    /// <summary>
    /// �v���C���[�p��UI�ݒ�(���C�t�p�A�C�R���̐����Ȃ�)
    /// </summary>
    /// <param name="maxHp"></param>
    public void SetPlayerInfo(int maxHp)
    {
        StartCoroutine(GenerateLife(maxHp));

        // TODO �e���̍ő�l��ݒ�
    }

    /// <summary>
    /// ���C�t�p�A�C�R���̃Q�[���I�u�W�F�N�g�𐶐�
    /// </summary>
    /// <param name="lifeCount"></param>
    /// <returns></returns>
    public IEnumerator GenerateLife(int lifeCount)
    {

        for(int i = 0; i < lifeCount; i++)
        {
            yield return new WaitForSeconds(0.25f);
            lifesList.Add(Instantiate(lifePrefab, lifeTran, false));
        }
    }

    /// <summary>
    /// ���C�t�p�A�C�R���̍ĕ\��
    /// </summary>
    /// <param name="amout"></param>
    public void UpdateDisplayLife(int amout)
    {

        for(int i = 0; i < lifesList.Count; i++)
        {

            if(i < amout)
            {
                lifesList[i].SetActive(true);
            }
            else
            {
                lifesList[i].SetActive(false);
            }
        }
    }
}