using System;
using System.Collections.Generic;
using XD.Cn.Common;

namespace XD.Cn.Payment
{
    public class XDPayment
    {
        public static void QueryWithProductIds(string[] productIds, Action<XDSkuDetailInfo> callback)
        {
            XDPaymentImpl.GetInstance().QueryWithProductIds(productIds, callback);
        }
        
        public static void PayWithProduct(string orderId, string productId, string roleId, string serverId, string ext,
            Action<XDOrderInfoWrapper> callback)
        {
            XDPaymentImpl.GetInstance().PayWithProduct(orderId, productId, roleId, serverId, ext, callback);
        }

        public static void QueryRestoredPurchases(Action<List<XDRestoredPurchase>> callback)
        {
            XDPaymentImpl.GetInstance().QueryRestoredPurchases(callback);
        }

        public static void CheckRefundStatus(Action<XDRefundResultWrapper> callback)
        {
            XDPaymentImpl.GetInstance().CheckRefundStatus(callback);
        }

        public static void CheckRefundStatusWithUI(Action<XDRefundResultWrapper> callback)
        {
            XDPaymentImpl.GetInstance().CheckRefundStatusWithUI(callback);
        }

        public static void AndroidPay(string orderId, string productId, string roleId, string serverId, string ext,
            Action<int, string> callback){
            XDPaymentImpl.GetInstance().AndroidPay(orderId, productId, orderId, serverId, ext, callback);
        }
    }
}