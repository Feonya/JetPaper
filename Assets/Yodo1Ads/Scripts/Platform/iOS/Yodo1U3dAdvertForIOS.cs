public class Yodo1U3dAdvertForIOS
{
#if UNITY_IPHONE
	/// <summary>
	/// 初始化SDK
	/// </summary>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dInitWithAppKey (string appKey, string gameObject);

	public static void InitWithAppKey (string appKey)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dInitWithAppKey (appKey, Yodo1U3dSDK.SharedInstance.sdkGameObjectName);
		}
	}

	/// <summary>
	/// 设置是否开启Log
	/// </summary>
	/// <returns><c>true</c>, if has ad video was unityed, <c>false</c> otherwise.</returns>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool Unity3dSetLogEnable (bool enable);

	public static bool SetLogEnable (bool enable)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			return	Unity3dSetLogEnable (enable);
		}
		return false;
	}

    #region Banner

	/// <summary>
	/// 设置广告显示位置
	/// </summary>
	/// <param name="align">Align.</param>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dSetBannerAlign (int align);

	public static void SetBannerAlign (Yodo1U3dConstants.BannerAdAlign align)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dSetBannerAlign ((int)align);
		}
	}

	/// <summary>
	/// 设置广告位置偏移量
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dSetBannerOffset (float x,float y);
	public static void SetBannerOffset (float x,float y)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dSetBannerOffset (x,y);
		}
	}

	/// <summary>
	/// 设置Banner广告缩放倍数.
	/// </summary>
	/// <param name="sx">Sx.</param>
	/// <param name="sy">Sy.</param>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dSetBannerScale (float sx,float sy);
	public static void SetBannerScale (float sx,float sy)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dSetBannerScale (sx,sy);
		}
	}

	/// <summary>
	/// 显示广告
	/// </summary>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void UnityShowBanner ();

	public static void ShowBanner ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			UnityShowBanner ();
		}
	}

	/// <summary>
	/// 隐藏广告
	/// </summary>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dHideBanner ();

	public static void HideBanner ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dHideBanner ();
		}
	}

	/// <summary>
	/// 移除广告
	/// </summary>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dRemoveBanner ();

	public static void RemoveBanner ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dRemoveBanner ();
		}
	}

    #endregion Banner

    #region Interstitial

	/// <summary>
	/// 插屏广告是否可以播放
	/// </summary>
	/// <returns><c>true</c>, if switch full screen ad was unityed, <c>false</c> otherwise.</returns>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool Unity3dInterstitialIsReady ();

	public static bool InterstitialIsReady ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			return Unity3dInterstitialIsReady ();
		}
		return false;
	}

	/// <summary>
	/// 显示插屏广告
	/// </summary>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dShowInterstitial ();

	public static void ShowInterstitial ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dShowInterstitial ();
		}
	}

    #endregion Interstitial

    #region Video

	/// <summary>
	/// Video是否已经准备好
	/// </summary>
	/// <returns><c>true</c>, if switch ad video was unityed, <c>false</c> otherwise.</returns>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern bool Unity3dVideoIsReady ();

	public static bool VideoIsReady ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			return	Unity3dVideoIsReady ();
		}
		return false;
	}

	/// <summary>
	/// 显示Video广告
	/// </summary>
	/// <param name="callbackGameObj">Callback game object.</param>
	/// <param name="callbackMethod">Callback method.</param>
	[DllImport (Yodo1U3dConstants.LIB_NAME)]
	private static extern void Unity3dShowVideo ();

	public static void ShowVideo ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Unity3dShowVideo ();
		}
	}

    #endregion Video

#endif
}