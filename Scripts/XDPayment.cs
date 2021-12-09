using System;
using System.Collections.Generic;
using XD.Cn.Common;

namespace XD.Cn.Payment
{
    public class XDPayment
    {
        public static void PayWithProduct(string orderId, string productId, string roleId, string serverId, string ext,
            Action<XDOrderInfoWrapper> callback)
        {
            XDPaymentImpl.GetInstance().PayWithProduct(orderId, productId, roleId, serverId, ext, callback);
        }

        public static void PayWithWeb(string serverId, string roleId, Action<XDError> callback)
        {
            XDPaymentImpl.GetInstance().PayWithWeb(serverId, roleId, callback);
        }

        public static void QueryWithProductIds(string[] productIds, Action<XDSkuDetailInfo> callback)
        {
            XDPaymentImpl.GetInstance().QueryWithProductIds(productIds, callback);
        }

        public static void QueryRestoredPurchase(Action<List<XDRestoredPurchase>> callback)
        {
            XDPaymentImpl.GetInstance().QueryRestoredPurchase(callback);
        }

        public static void RestorePurchase(string purchaseToken, string orderId, string productId, string roleId, string serverId, string ext, Action<XDOrderInfoWrapper> callback)
        {
            XDPaymentImpl.GetInstance()
                .RestorePurchase(purchaseToken, orderId, productId, roleId, serverId, ext, callback);
        }

        public static void CheckRefundStatus(Action<XDRefundResultWrapper> callback)
        {
            XDPaymentImpl.GetInstance().CheckRefundStatus(callback);
        }

        public static void CheckRefundStatusWithUI(Action<XDRefundResultWrapper> callback)
        {
            XDPaymentImpl.GetInstance().CheckRefundStatusWithUI(callback);
        }
    }
}