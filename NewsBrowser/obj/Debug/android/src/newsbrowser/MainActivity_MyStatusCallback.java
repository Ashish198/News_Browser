package newsbrowser;


public class MainActivity_MyStatusCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.facebook.Session.StatusCallback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_call:(Lcom/facebook/Session;Lcom/facebook/SessionState;Ljava/lang/Exception;)V:GetCall_Lcom_facebook_Session_Lcom_facebook_SessionState_Ljava_lang_Exception_Handler:Xamarin.Facebook.Session/IStatusCallbackInvoker, Xamarin.Facebook\n" +
			"";
		mono.android.Runtime.register ("NewsBrowser.MainActivity/MyStatusCallback, NewsBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_MyStatusCallback.class, __md_methods);
	}


	public MainActivity_MyStatusCallback () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_MyStatusCallback.class)
			mono.android.TypeManager.Activate ("NewsBrowser.MainActivity/MyStatusCallback, NewsBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_MyStatusCallback (newsbrowser.MainActivity p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_MyStatusCallback.class)
			mono.android.TypeManager.Activate ("NewsBrowser.MainActivity/MyStatusCallback, NewsBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "NewsBrowser.MainActivity, NewsBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void call (com.facebook.Session p0, com.facebook.SessionState p1, java.lang.Exception p2)
	{
		n_call (p0, p1, p2);
	}

	private native void n_call (com.facebook.Session p0, com.facebook.SessionState p1, java.lang.Exception p2);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
