using System;
using System.Collections.Generic;
using TapTap.Common;

namespace XD.Cn.Payment
{
    [Serializable]
    public class XDRestoredPurchasesWrapper
    {
        public List<XDRestoredPurchases> restoredPurchasesList;
        
        public XDRestoredPurchasesWrapper(string json)
        {
            Dictionary<string,object> dic = Json.Deserialize(json) as Dictionary<string,object>;
            List<object> dicList = SafeDictionary.GetValue<List<object>>(dic,"transactions");

            if (dicList != null)
            {
                this.restoredPurchasesList = new List<XDRestoredPurchases>();
                foreach (var detailDic in dicList)
                {
                    Dictionary<string,object> innerDic = detailDic as Dictionary<string,object>;
                   restoredPurchasesList.Add(new XDRestoredPurchases(innerDic));   
                }
            }
        }
    }
    
    [Serializable]
    public class XDRestoredPurchases
    {
        public string transactionIdentifier;
        public string productIdentifier;

        public XDRestoredPurchases(Dictionary<string, object> dic)
        {
            if (dic == null) return;
            this.transactionIdentifier = SafeDictionary.GetValue<string>(dic, "transactionIdentifier");
            this.productIdentifier = SafeDictionary.GetValue<string>(dic, "productIdentifier");
        }
    }
}