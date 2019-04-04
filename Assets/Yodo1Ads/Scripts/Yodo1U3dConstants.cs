public class Yodo1U3dConstants
{
    public const string LIB_NAME = "__Internal";//对外扩展接口的库名

    public enum Yodo1AdsType
    {
        Yodo1AdsTypeNone = -1,
        Yodo1AdsTypeBanner = 1001,//banner
        Yodo1AdsTypeInterstitial = 1002,//插屏
        Yodo1AdsTypeVideo = 1003,//视频
    };

    //banner广告展示位置
    public enum BannerAdAlign
    {
        BannerAdAlignLeft = 1 << 0,
        BannerAdAlignHorizontalCenter = 1 << 1,
        BannerAdAlignRight = 1 << 2,
        BannerAdAlignTop = 1 << 3,
        BannerAdAlignVerticalCenter = 1 << 4,
        BannerAdAlignBotton = 1 << 5,
    };

    public enum AdEvent
    {
        AdEventClose = 0,//关闭
        AdEventFinish = 1,//广告播放完成
        AdEventClick = 2,//用户点击广告
        AdEventLoaded = 3,//加载完毕
        AdEventShowSuccess = 4,//广告成功展示
        AdEventShowFail = 5,//广告展示失败
        AdEventPurchase = 6,//广告购买
        AdEventLoadFail = -1,//广告加载失败!
    };
}