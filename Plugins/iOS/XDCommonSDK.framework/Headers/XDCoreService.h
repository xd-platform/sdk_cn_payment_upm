//
//  XDCoreService.h
//  XDCommonSDK
//
//  Created by JiangJiahao on 2020/11/23.
//

/// unity 桥文件

#import <Foundation/Foundation.h>
#import <XDCommonSDK/XDSDK.h>
#import <XDCommonSDK/XDGShare.h>
#import <XDCommonSDK/XDGMessageManager.h>
#import <XDCommonSDK/XDGTrackerManager.h>
#import <TDSGlobalSDKCommonKit/NSDictionary+TDSGlobalJson.h>
#import "XDGGameDataManager.h"


NS_ASSUME_NONNULL_BEGIN

@interface XDCoreService : NSObject
+ (NSString *)getSDKVersionName;

+ (void)setDebugMode:(NSNumber *)debug;

+ (NSNumber *)isInitialized;

// report
+ (void)serverId:(NSString *)serverId
          roleId:(NSString *)roleId
        roleName:(NSString *)roleName;

+ (void)storeReview;

+ (void)trackUser:(NSString *)userId;

// trackRole
+ (void)serverId:(NSString *)serverId
          roleId:(NSString *)roleId
        roleName:(NSString *)roleName
           level:(NSNumber *)level;

+ (void)trackEvent:(NSString *)eventName;

+ (void)trackAchievement;

+ (void)eventCompletedTutorial;

+ (void)eventCreateRole;


//绑定回调
+ (void)setCallback:(void (^)(NSString *result))callback;

//initSDK
+ (void)clientId:(NSString*)clientId orientation:(NSNumber*)orientation;

@end

NS_ASSUME_NONNULL_END
