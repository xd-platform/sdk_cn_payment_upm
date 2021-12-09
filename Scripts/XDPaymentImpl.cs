using System;
using System.Collections.Generic;
using TapTap.Common;
using UnityEngine;
using XD.Cn.Common;

namespace XD.Cn.Payment
{
    public class XDPaymentImpl : IPaymentAPI
    {
        private readonly string XDG_PAYMENT_SERVICE = "XDPaymentService"; //注意要和iOS本地桥接类名一样

        private XDPaymentImpl()
        {
            EngineBridge.GetInstance()
                .Register(XDUnityBridge.PAYMENT_SERVICE_NAME, XDUnityBridge.PAYMENT_SERVICE_IMPL);
        }

        private static volatile XDPaymentImpl _instance;
        private static readonly object Locker = new object();

        public static XDPaymentImpl GetInstance()
        {
            lock (Locker)
            {
                if (_instance == null)
                {
                    _instance = new XDPaymentImpl();
                }
            }

            return _instance;
        }

        public void QueryWithProductIds(string[] productIds, Action<XDSkuDetailInfo> callback)
        {
            var dic = new Dictionary<string, object>
            {
                { "productIds", productIds }
            };

            var command = new Command.Builder()
                .Service(XDG_PAYMENT_SERVICE)
                .Method("queryWithProductIds")
                .Args(dic)
                .Callback(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, result =>
            {
                XDTool.Log("queryWithProductIds 方法结果: " + result.ToJSON());
                callback(new XDSkuDetailInfo(result.content));
            });
        }

        public void PayWithProduct(string orderId, string productId, string roleId, string serverId, string ext,
            Action<XDOrderInfoWrapper> callback)
        {
            var dic = new Dictionary<string, object>
            {
                { "orderId", orderId },
                { "productId", productId },
                { "roleId", roleId },
                { "serverId", serverId },
                { "ext", ext }
            };

            var command = new Command.Builder()
                .Service(XDG_PAYMENT_SERVICE)
                .Method("payWithProduct")
                .Args(dic)
                .Callback(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command,
                (result) =>
                {
                    XDTool.Log("PayWithProduct 方法结果: " + result.ToJSON());

                    callback(new XDOrderInfoWrapper(result.content));
                });
        }

        public void PayWithWeb(string serverId, string roleId, Action<XDError> callback)
        {
            var dic = new Dictionary<string, object>
            {
                { "serverId", serverId },
                { "roleId", roleId }
            };
            var command = new Command.Builder()
                .Service(XDG_PAYMENT_SERVICE)
                .Method("payWithWeb")
                .Args(dic)
                .Callback(true)
                .CommandBuilder();
            EngineBridge.GetInstance()
                .CallHandler(command, result =>
                {
                    XDTool.Log("PayWithWeb 方法结果: " + result.ToJSON());
                    callback(!XDTool.checkResultSuccess(result)
                        ? new XDError(result.code, result.message)
                        : new XDError(result.content));
                });
        }

        public void QueryRestoredPurchase(Action<List<XDRestoredPurchase>> callback)
        {
            var command = new Command.Builder()
                .Service(XDG_PAYMENT_SERVICE)
                .Method("queryRestoredPurchases")
                .Callback(true)
                .CommandBuilder();
            EngineBridge.GetInstance().CallHandler(command,
                (result) =>
                {
                    XDTool.Log("QueryRestoredPurchase 方法结果: " + result.ToJSON());

                    if (!XDTool.checkResultSuccess(result))
                    {
                        callback(null);
                        return;
                    }

                    callback(new XDRestoredPurchaseWrapper(result.content).transactionList);
                });
        }

        public void RestorePurchase(string purchaseToken, string orderId, string productId, string roleId,
            string serverId, string ext, Action<XDOrderInfoWrapper> callback)
        {
            var dic = new Dictionary<string, object>
            {
                { "purchaseToken", purchaseToken },
                { "productId", productId },
                { "orderId", orderId },
                { "roleId", roleId },
                { "serverId", serverId },
                { "ext", ext }
            };
            var command = new Command.Builder()
                .Service(XDG_PAYMENT_SERVICE)
                .Method("restorePurchase")
                .Args(dic)
                .Callback(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                XDTool.Log("RestorePurchase 方法结果: " + result.ToJSON());
                callback(new XDOrderInfoWrapper(result.content));
            });
        }

        public void CheckRefundStatus(Action<XDRefundResultWrapper> callback)
        {
            var command = new Command.Builder()
                .Service(XDG_PAYMENT_SERVICE)
                .Method("checkRefundStatus")
                .Callback(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command, (result) =>
            {
                XDTool.Log("CheckRefundStatus result: " + result.ToJSON());
                callback(new XDRefundResultWrapper(result.content));
            });
        }

        public void CheckRefundStatusWithUI(Action<XDRefundResultWrapper> callback)
        {
            var command = new Command.Builder()
                .Service(XDG_PAYMENT_SERVICE)
                .Method("checkRefundStatusWithUI")
                .Callback(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command,
                result =>
                {
                    XDTool.Log("CheckRefundStatusWithUI result: " + result.ToJSON());
                    callback(new XDRefundResultWrapper(result.content));
                });
        }
    }
}