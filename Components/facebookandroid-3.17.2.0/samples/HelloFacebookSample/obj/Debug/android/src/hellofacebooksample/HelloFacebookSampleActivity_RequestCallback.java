package hellofacebooksample;


public class HelloFacebookSampleActivity_RequestCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.facebook.Request.Callback
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCompleted:(Lcom/facebook/Response;)V:GetOnCompleted_Lcom_facebook_Response_Handler:Xamarin.Facebook.Request/ICallbackInvoker, Xamarin.Facebook\n" +
			"";
		mono.android.Runtime.register ("HelloFacebookSample.HelloFacebookSampleActivity/RequestCallback, HelloFacebookSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HelloFacebookSampleActivity_RequestCallback.class, __md_methods);
	}


	public HelloFacebookSampleActivity_RequestCallback () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HelloFacebookSampleActivity_RequestCallback.class)
			mono.android.TypeManager.Activate ("HelloFacebookSample.HelloFacebookSampleActivity/RequestCallback, HelloFacebookSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCompleted (com.facebook.Response p0)
	{
		n_onCompleted (p0);
	}

	private native void n_onCompleted (com.facebook.Response p0);

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
