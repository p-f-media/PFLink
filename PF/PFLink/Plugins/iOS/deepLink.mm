@interface Launcher : NSObject
+(void)launch:(NSString*) urlStr itunesAppId : (NSString*) itunesAppId;
@end

@implementation Launcher
+(void)launch:(NSString*) urlStr itunesAppId : (NSString*) itunesAppId{
    NSURL *url = [NSURL URLWithString:urlStr];
    NSURL *storeurl = [NSURL URLWithString:[NSString stringWithFormat:@"itms-apps://itunes.apple.com/app/%@", itunesAppId]];
    
    if(floor(NSFoundationVersionNumber) > NSFoundationVersionNumber_iOS_9_x_Max){
        // iOS10 以降
        if([[UIApplication sharedApplication] canOpenURL:url]){
            [[UIApplication sharedApplication] openURL:url options:@{} completionHandler:nil];
        }else{
            [[UIApplication sharedApplication] openURL:storeurl options:@{} completionHandler:nil];
        }
    }else{
        // iOS9 以前
        if([[UIApplication sharedApplication] canOpenURL:url]){
            [[UIApplication sharedApplication] openURL:url];
        }else{
            [[UIApplication sharedApplication] openURL:storeurl];
        }
    }
}
@end

extern "C"{
    void launch(const char *url, const char *itunesAppId){
        NSString *urlString = [NSString stringWithCString:url encoding:NSUTF8StringEncoding];
        NSString *itunesAppIdString = [NSString stringWithCString:itunesAppId encoding:NSUTF8StringEncoding];
        
        [Launcher launch:urlString itunesAppId:itunesAppIdString];
    }
}
