using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;
using XD.Cn.Common;

namespace XD.Cn.Payment
{
    public class XDSkuDetailInfo
    {
        public XDError xdgError;

        public List<SkuDetailBean> skuDetailList;

        public XDSkuDetailInfo(string jsonStr)
        {
            var dic = Json.Deserialize(jsonStr) as Dictionary<string, object>;
            var errorDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "error");
            if (errorDic != null)
            {
                xdgError = new XDError(errorDic);
            }

            var list = SafeDictionary.GetValue<List<object>>(dic, "products");
            if (list == null) return;
            skuDetailList = new List<SkuDetailBean>();
            foreach (var skuDetail in list)
            {
                var innerDic = skuDetail as Dictionary<string, object>;
                var skuDetailBean = new SkuDetailBean(innerDic);
                skuDetailList.Add(skuDetailBean);
            }
        }
    }
#if UNITY_IOS
    [Serializable]
    public class SkuDetailBean
    {
        public string localizedDescription;

        public string localizedTitle;

        public double price;

        public string productIdentifier;

        public string localeIdentifier;

        public PriceLocale priceLocale;

        public SkuDetailBean(string json)
        {
            Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
            this.localeIdentifier = SafeDictionary.GetValue<string>(dic,"localeIdentifier");
            this.localizedTitle = SafeDictionary.GetValue<string>(dic,"localizedTitle") ;
            this.price = SafeDictionary.GetValue<double>(dic, "price");
            this.productIdentifier = SafeDictionary.GetValue<string>(dic,"productIdentifier");
            Dictionary<string,object> priceLocaleDic = SafeDictionary.GetValue<Dictionary<string,object>>(dic,"priceLocale") as Dictionary<string,object>;
            this.priceLocale = new PriceLocale(priceLocaleDic);
        }

        public SkuDetailBean(Dictionary<string,object> dic)
        {
            this.localeIdentifier = SafeDictionary.GetValue<string>(dic,"localeIdentifier");
            this.localizedTitle = SafeDictionary.GetValue<string>(dic,"localizedTitle") ;
            this.price =SafeDictionary.GetValue<double>(dic,"price");
            this.productIdentifier = SafeDictionary.GetValue<string>(dic,"productIdentifier");
            Dictionary<string,object> priceLocaleDic = SafeDictionary.GetValue<Dictionary<string,object>>(dic,"priceLocale") as Dictionary<string,object>;
            this.priceLocale = new PriceLocale(priceLocaleDic);
        }

        public SkuDetailBean()
        {

        }

        public string ToJSON(){
            return JsonUtility.ToJson(this);
        }
        

    }
    [Serializable]
    public class PriceLocale
    {
        public string localeIdentifier;

        public string languageCode;

        public string countryCode;

        public string scriptCode;

        public string calendarIdentifier;

        public string decimalSeparator;

        public string currencySymbol;

        public PriceLocale(Dictionary<string,object> dic)
        {
            this.localeIdentifier = SafeDictionary.GetValue<string>(dic,"localeIdentifier");
            this.languageCode = SafeDictionary.GetValue<string>(dic,"languageCode");
            this.countryCode = SafeDictionary.GetValue<string>(dic,"countryCode");
            this.scriptCode = SafeDictionary.GetValue<string>(dic,"scriptCode");
            this.calendarIdentifier = SafeDictionary.GetValue<string>(dic,"calendarIdentifier");
            this.decimalSeparator = SafeDictionary.GetValue<string>(dic,"decimalSeparator");
            this.currencySymbol = SafeDictionary.GetValue<string>(dic,"currencySymbol");
        }
    }

#elif UNITY_ANDROID
    [Serializable]
    public class SkuDetailBean
    {
        public string description;

        public string freeTrialPeriod;

        public string iconUrl;

        public string introductoryPrice;

        public long introductoryPriceAmountMicros;

        public int introductoryPriceCycles;

        public string originJson;

        public string originPrice;

        public long originPriceAmountMicros;

        public string price;

        public long priceAmountMicros;

        public string priceCurrencyCode;

        public string productId;

        public string subscriptionPeriod;

        public string title;

        public string type;

        public SkuDetailBean(Dictionary<string, object> dic)
        {
            if (dic == null) return;
            description = SafeDictionary.GetValue<string>(dic, "description");
            freeTrialPeriod = SafeDictionary.GetValue<string>(dic, "freeTrialPeriod");
            iconUrl = SafeDictionary.GetValue<string>(dic, "iconUrl");
            introductoryPrice = SafeDictionary.GetValue<string>(dic, "introductoryPrice");
            introductoryPriceAmountMicros = SafeDictionary.GetValue<long>(dic, "introductoryPriceAmountMicros");
            introductoryPriceCycles = SafeDictionary.GetValue<int>(dic, "introductoryPriceCycles");
            originJson = SafeDictionary.GetValue<string>(dic, "originJson");
            originPrice = SafeDictionary.GetValue<string>(dic, "originPrice");
            originPriceAmountMicros = SafeDictionary.GetValue<long>(dic, "originPriceAmountMicros");
            price = SafeDictionary.GetValue<string>(dic, "price");
            priceAmountMicros = SafeDictionary.GetValue<long>(dic, "priceAmountMicros");
            priceCurrencyCode = SafeDictionary.GetValue<string>(dic, "priceCurrencyCode");
            productId = SafeDictionary.GetValue<string>(dic, "productId");
            subscriptionPeriod = SafeDictionary.GetValue<string>(dic, "subscriptionPeriod");
            title = SafeDictionary.GetValue<string>(dic, "title");
            type = SafeDictionary.GetValue<string>(dic, "type");
        }
    }
#else
     public class SkuDetailBean
     {
         public SkuDetailBean(Dictionary<string,object> dic)
         {
            
         }

         public string ToJSON(){
            return JsonUtility.ToJson(this);
        }
     }
#endif

    public class XDRestoredPurchaseWrapper
    {
        public List<XDRestoredPurchase> transactionList;

        public XDRestoredPurchaseWrapper(string jsonStr)
        {
            var dic = Json.Deserialize(jsonStr) as Dictionary<string, object>;
            var list = SafeDictionary.GetValue<List<object>>(dic, "transactions");
            if (list == null)
            {
                return;
            }

            transactionList = new List<XDRestoredPurchase>();
            foreach (var obj in list)
            {
                var beanDic = obj as Dictionary<string, object>;
                transactionList.Add(new XDRestoredPurchase(beanDic));
            }
        }
    }

#if UNITY_IOS
    [Serializable]
    public class XDRestoredPurchase
    {
        public string transactionIdentifier;

        public string productIdentifier;

        public XDRestoredPurchase(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }

        public XDRestoredPurchase(Dictionary<string,object> dic)
        {
            this.transactionIdentifier = SafeDictionary.GetValue<string>(dic,"transactionIdentifier") ;
            this.productIdentifier = SafeDictionary.GetValue<string>(dic,"productIdentifier") ;
        }
    }

#elif UNITY_ANDROID
    [Serializable]
    public class XDRestoredPurchase
    {
        public string orderId;

        public string packageName;

        public string productId;

        public long purchaseTime;

        public string purchaseToken;

        public int purchaseState;

        public string developerPayload;

        public bool acknowledged;

        public bool autoRenewing;

        public XDRestoredPurchase(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }

        public XDRestoredPurchase(Dictionary<string, object> dic)
        {
            if (dic == null) return;
            orderId = SafeDictionary.GetValue<string>(dic, "orderId");
            packageName = SafeDictionary.GetValue<string>(dic, "packageName");
            productId = SafeDictionary.GetValue<string>(dic, "productId");
            purchaseTime = SafeDictionary.GetValue<long>(dic, "purchaseTime");
            purchaseToken = SafeDictionary.GetValue<string>(dic, "purchaseToken");
            purchaseState = SafeDictionary.GetValue<int>(dic, "purchaseState");
            developerPayload = SafeDictionary.GetValue<string>(dic, "developerPayload");
            acknowledged = SafeDictionary.GetValue<bool>(dic, "acknowledged");
            autoRenewing = SafeDictionary.GetValue<bool>(dic, "autoRenewing");
        }
    }
#else 
    public class XDRestoredPurchase
    {
        public XDRestoredPurchase(Dictionary<string,object> dic)
        {

        }

        public XDRestoredPurchase(string json)
        {
            
        }
    }
#endif
    public class XDOrderInfoWrapper
    {
        public XDError xdgError;
        public XDOrderInfo orderInfo;

        public XDOrderInfoWrapper(string jsonStr)
        {
            var dic = Json.Deserialize(jsonStr) as Dictionary<string, object>;
            var errorDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "error");
            if (errorDic != null)
            {
                xdgError = new XDError(errorDic);
            }

            var orderInfoDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "orderInfo");
            orderInfo = new XDOrderInfo(orderInfoDic);
        }
    }

    [Serializable]
    public class XDOrderInfo
    {
        public string orderId;

        public string productId;

        public string roleId;

        public string serverId;

        public string ext;

        public XDOrderInfo(Dictionary<string, object> dic)
        {
            if (dic == null) return;
            orderId = SafeDictionary.GetValue<string>(dic, "orderId");
            productId = SafeDictionary.GetValue<string>(dic, "productId");
            roleId = SafeDictionary.GetValue<string>(dic, "roleId");
            serverId = SafeDictionary.GetValue<string>(dic, "serverId");
            ext = SafeDictionary.GetValue<string>(dic, "ext");
        }
    }

    public class XDRefundResultWrapper
    {
        public XDError xdgError;
        public List<XDRefundDetails> refundList;

        public XDRefundResultWrapper(string jsonStr)
        {
         
            var dic = Json.Deserialize(jsonStr) as Dictionary<string, object>;
            var code = SafeDictionary.GetValue<int>(dic, "code");
            var msg = SafeDictionary.GetValue<string>(dic, "msg");
            if (code != Result.RESULT_SUCCESS)
            {
                xdgError = new XDError(code, msg);
            }
            else
            {
                var dataDic = SafeDictionary.GetValue<Dictionary<string, object>>(dic, "data");
                if (dataDic == null) return;
                var list = SafeDictionary.GetValue<List<object>>(dataDic, "list");
                if (list == null)return;
                
                refundList = new List<XDRefundDetails>();
                for (var index = 0; index < list.Count; index++)
                {
                    var obj = list[index];
                    var beanDic = obj as Dictionary<string, object>;
                    refundList.Add(new XDRefundDetails(beanDic));
                }
            }
        }
    }

    [Serializable]
    public class XDRefundDetails
    {
        public string tradeNo;
        public string productId;
        public string currency;
        public string outTradeNo;
        public double refundAmount;
        public int supplyStatus;
        public int platform;
        public int channelType;

        public XDRefundDetails(Dictionary<string, object> dic)
        {
            if (dic == null) return;
            tradeNo = SafeDictionary.GetValue<string>(dic, "tradeNo");
            productId = SafeDictionary.GetValue<string>(dic, "productId");
            currency = SafeDictionary.GetValue<string>(dic, "currency");
            outTradeNo = SafeDictionary.GetValue<string>(dic, "outTradeNo");
            refundAmount = SafeDictionary.GetValue<double>(dic, "refundAmount");
            supplyStatus = SafeDictionary.GetValue<int>(dic, "supplyStatus");
            platform = SafeDictionary.GetValue<int>(dic, "platform");
            channelType = SafeDictionary.GetValue<int>(dic, "channelType");
        }
    }
}