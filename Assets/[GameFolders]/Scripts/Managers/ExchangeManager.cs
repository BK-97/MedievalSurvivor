using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum CurrencyType
{
    Coin,
    Cash
}
public class ExchangeManager : Singleton<ExchangeManager>
{
    #region Params
    [HideInInspector]
    public float floadingSpeed=1;
    private GameObject _fakeUI;
    private Image exchangeIcon;

    private Dictionary<CurrencyType, int> currencyDictionary;
    #endregion
    #region Events
    public DictonaryEvent OnCurrencyChange = new DictonaryEvent();
    [HideInInspector]
    public UnityEvent OnCurrencyAdded = new UnityEvent();
    #endregion
    public ExchangeManager()
    {
        currencyDictionary = new Dictionary<CurrencyType, int>();
    }

    private void Start()
    {
        currencyDictionary[CurrencyType.Cash] = PlayerPrefs.GetInt(PlayerPrefKeys.CurrentCash, 0);
    }
    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(() => OnCurrencyChange.Invoke(currencyDictionary));
    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneLoaded.RemoveListener(() => OnCurrencyChange.Invoke(currencyDictionary));
    }
    #region CurrencyMethods
    public bool UseCurrency(CurrencyType currencyType, int amount)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            if (currencyDictionary[currencyType] >= amount)
            {
                currencyDictionary[currencyType] -= amount;
                string exchangeName = currencyType.ToString();
                PlayerPrefs.SetInt(exchangeName, currencyDictionary[currencyType]);
                OnCurrencyChange.Invoke(currencyDictionary);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void AddCurrency(CurrencyType currencyType, int amount)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            currencyDictionary[currencyType] += amount;
            string exchangeName = currencyType.ToString();
            PlayerPrefs.SetInt(exchangeName, currencyDictionary[currencyType]);
            OnCurrencyChange.Invoke(currencyDictionary);
            OnCurrencyAdded.Invoke();
        }
    }

    public int GetCurrency(CurrencyType currencyType)
    {
        if (currencyDictionary.ContainsKey(currencyType))
        {
            return currencyDictionary[currencyType];
        }
        else
        {
            return 0;
        }
    }
    #endregion
    #region GetSet
    public Vector3 GetFakeUIPos()
    {
        return _fakeUI.transform.position;
    }
    public Vector3 GetIconPos()
    {
        return exchangeIcon.rectTransform.position;
    }
    public void SetExchangeIcon(Image icon)
    {
        exchangeIcon = icon;
    }
    public void SetFakeUI(GameObject fakeUI)
    {
        _fakeUI = fakeUI;
    }
    #endregion
}
public class DictonaryEvent : UnityEvent<Dictionary<CurrencyType, int>> { }