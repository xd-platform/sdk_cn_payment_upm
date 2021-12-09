using System;
using System.Collections.Generic;
using XD.Cn.Common;

namespace XD.Cn.Payment
{
    public interface IPaymentAPI
    {
        void PayWithProduct(string orderId, string productId, string roleId, string serverId, string ext, Action<XDOrderInfoWrapper> callback);
        void PayWithWeb(string serverId, string roleId, Action<XDError> callback);
        void QueryWithProductIds(string[] productIds, Action<XDSkuDetailInfo> callback);
        void QueryRestoredPurchase(Action<List<XDRestoredPurchase>> callback);
        void RestorePurchase(string purchaseToken, string orderId, string productId, string roleId, string serverId, string ext, Action<XDOrderInfoWrapper> callback);
        void CheckRefundStatus(Action<XDRefundResultWrapper> callback);
        void CheckRefundStatusWithUI(Action<XDRefundResultWrapper> callback);
    }
}