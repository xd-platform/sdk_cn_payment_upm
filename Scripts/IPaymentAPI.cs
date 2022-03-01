using System;
using System.Collections.Generic;
using XD.Cn.Common;

namespace XD.Cn.Payment
{
    public interface IPaymentAPI
    {
        void QueryWithProductIds(string[] productIds, Action<XDSkuDetailInfo> callback);
        void PayWithProduct(string orderId, string productId, string roleId, string serverId, string ext, Action<XDOrderInfoWrapper> callback);
        void QueryRestoredPurchases(Action<List<XDRestoredPurchase>> callback);
        void CheckRefundStatus(Action<XDRefundResultWrapper> callback);
        void CheckRefundStatusWithUI(Action<XDRefundResultWrapper> callback);
        void AndroidPay(string orderId, string productId, string roleId, string serverId, string ext, Action<int, string> callback);
    }
}