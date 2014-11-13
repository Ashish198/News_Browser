package newsbrowser;


public class Home
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnTouchListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onTouch:(Landroid/view/View;Landroid/view/MotionEvent;)Z:GetOnTouch_Landroid_view_View_Landroid_view_MotionEvent_Handler:Android.Views.View/IOnTouchListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("NewsBrowser.Home, NewsBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Home.class, __md_methods);
	}


	public Home () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Home.class)
			mono.android.TypeManager.Activate ("NewsBrowser.Home, NewsBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean onTouch (android.view.View p0, android.view.MotionEvent p1)
	{
		return n_onTouch (p0, p1);
	}

	private native boolean n_onTouch (android.view.View p0, android.view.MotionEvent p1);

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
