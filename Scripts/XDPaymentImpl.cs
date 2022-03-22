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
#if UNITY_IOS
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
#endif   
        }

        public void PayWithProduct(string orderId, string productId, string roleId, string serverId, string ext,
            Action<XDOrderInfoWrapper> callback)
        {
#if UNITY_IOS
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
#endif
        }

        public void QueryRestoredPurchases(Action<List<XDRestoredPurchase>> callback)
        {
#if UNITY_IOS
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
#endif
        }

        public void CheckRefundStatus(Action<XDRefundResultWrapper> callback)
        {
#if UNITY_IOS
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
#endif            
        }

        public void CheckRefundStatusWithUI(Action<XDRefundResultWrapper> callback)
        {
#if UNITY_IOS            
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
#endif
        }
        
        public void AndroidPay(string orderId, string productId, string roleId, string serverId, string ext,
            Action<AndroidPayResultType, string> callback){
#if UNITY_ANDROID
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
                .Method("pay")
                .Args(dic)
                .Callback(true)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command,
                (result) => {
                    XDTool.Log("安卓 pay 方法结果: " + result.ToJSON());
                    if (XDTool.checkResultSuccess(result)){
                        Dictionary<string, object> resultDic = Json.Deserialize(result.content) as Dictionary<string, object>;
                            int code = SafeDictionary.GetValue<int>(resultDic, "code");
                            string message = SafeDictionary.GetValue<string>(resultDic, "message");
                            if (code == 0){
                                callback(AndroidPayResultType.Success, "支付成功");
                            }else if (code == 1){
                                callback(AndroidPayResultType.Cancel, "支付取消");
                            }else if (code == 2){
                                callback(AndroidPayResultType.Processing, "支付处理中");
                            }else {
                                callback(AndroidPayResultType.Error, "支付失败 "+message);
                            }
                    } else {
                        callback(AndroidPayResultType.Error, "支付失败:解析result失败");
                        XDTool.LogError(result.ToJSON());
                    }
                });
#endif
        }
    }
}