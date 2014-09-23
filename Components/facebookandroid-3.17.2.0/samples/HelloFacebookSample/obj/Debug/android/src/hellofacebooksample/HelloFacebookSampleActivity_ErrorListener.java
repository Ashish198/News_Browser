package hellofacebooksample;


public class HelloFacebookSampleActivity_ErrorListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.facebook.widget.PickerFragment.OnErrorListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onError:(Lcom/facebook/widget/PickerFragment;Lcom/facebook/FacebookException;)V:GetOnError_Lcom_facebook_widget_PickerFragment_Lcom_facebook_FacebookException_Handler:Xamarin.Facebook.Widget.PickerFragment/IOnErrorListenerInvoker, Xamarin.Facebook\n" +
			"";
		mono.android.Runtime.register ("HelloFacebookSample.HelloFacebookSampleActivity/ErrorListener, HelloFacebookSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HelloFacebookSampleActivity_ErrorListener.class, __md_methods);
	}


	public HelloFacebookSampleActivity_ErrorListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HelloFacebookSampleActivity_ErrorListener.class)
			mono.android.TypeManager.Activate ("HelloFacebookSample.HelloFacebookSampleActivity/ErrorListener, HelloFacebookSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onError (com.facebook.widget.PickerFragment p0, com.facebook.FacebookException p1)
	{
		n_onError (p0, p1);
	}

	private native void n_onError (com.facebook.widget.PickerFragment p0, com.facebook.FacebookException p1);

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
