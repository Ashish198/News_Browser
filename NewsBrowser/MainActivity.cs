using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Xamarin.Facebook;
using Xamarin.Facebook.Model;
using Xamarin.Facebook.Widget;

using Android.Content.PM;

[assembly: Permission(Name = Android.Manifest.Permission.Internet)]
[assembly: Permission(Name = Android.Manifest.Permission.WriteExternalStorage)]
[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/app_id")]


namespace NewsBrowser
{
    [Activity(Label = "@string/app_name", MainLauncher = true, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : FragmentActivity
    {
        public MainActivity()
		{
			callback = new MyStatusCallback (this);
		}

        private string getKeyHash()
        {
            string keyhash = "";
            PackageInfo info = this.PackageManager.GetPackageInfo(this.PackageName, PackageInfoFlags.Signatures);

            foreach (Android.Content.PM.Signature signature in info.Signatures)
            {
                Java.Security.MessageDigest md = Java.Security.MessageDigest.GetInstance("SHA");
                md.Update(signature.ToByteArray());

                keyhash = Convert.ToBase64String(md.Digest());
            }

            return keyhash;
        }

		private static readonly string[] PERMISSIONS = new String [] { "publish_actions" };
		private static readonly Location SEATTLE_LOCATION = new Location ("") {
			Latitude = (47.6097),
			Longitude = (-122.3331)
		};
		readonly String PENDING_ACTION_BUNDLE_KEY = "com.facebook.samples.hellofacebook:PendingAction";
        private Button BtnProceed;
		private LoginButton loginButton;
		private ProfilePictureView profilePictureView;
		private TextView greeting;
		private PendingAction pendingAction = PendingAction.NONE;
		private ViewGroup controlsContainer;
		private IGraphUser user;

		enum PendingAction
		{
			NONE,
			POST_PHOTO,
			POST_STATUS_UPDATE
		}

		private UiLifecycleHelper uiHelper;

		class MyStatusCallback : Java.Lang.Object, Session.IStatusCallback
		{
            
			MainActivity owner;

			public MyStatusCallback (MainActivity owner)
			{
				this.owner = owner;
			}

			public void Call (Session session, SessionState state, Java.Lang.Exception exception)
			{
				owner.OnSessionStateChange (session, state, exception);
			}
		}

		private Session.IStatusCallback callback;

		class MyUserInfoChangedCallback : Java.Lang.Object, LoginButton.IUserInfoChangedCallback
		{
			MainActivity owner;

			public MyUserInfoChangedCallback (MainActivity owner)
			{
				this.owner = owner;
			}

			public void OnUserInfoFetched (IGraphUser user)
			{
				owner.user = user;
				owner.UpdateUI ();
				// It's possible that we were waiting for this.user to be populated in order to post a
				// status update.
				owner.HandlePendingAction ();
			}
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			uiHelper = new UiLifecycleHelper (this, callback);
			uiHelper.OnCreate (savedInstanceState);

			if (savedInstanceState != null) {
				string name = savedInstanceState.GetString (PENDING_ACTION_BUNDLE_KEY);
				pendingAction = (PendingAction)Enum.Parse (typeof(PendingAction), name);
			}

			SetContentView (Resource.Layout.main);

			loginButton = (LoginButton)FindViewById (Resource.Id.login_button);
			loginButton.UserInfoChangedCallback = new MyUserInfoChangedCallback (this);

            profilePictureView = FindViewById<ProfilePictureView>(Resource.Id.profilePicture);
            greeting = FindViewById<TextView>(Resource.Id.greeting);

			controlsContainer = (ViewGroup)FindViewById (Resource.Id.main_ui_container);

            Android.Support.V4.App.FragmentManager fm = SupportFragmentManager;
            Android.Support.V4.App.Fragment fragment = fm.FindFragmentById(Resource.Id.fragment_container);
			if (fragment != null) {
				controlsContainer.Visibility = ViewStates.Gone;
			}

			fm.BackStackChanged += delegate {
				if (fm.BackStackEntryCount == 0) {
					// We need to re-show our UI.
					controlsContainer.Visibility = ViewStates.Visible;
				}
			};

            BtnProceed = (Button)FindViewById(Resource.Id.BtnProceed);
            BtnProceed.Click += new EventHandler(BtnProceed_Click);

            getKeyHash();
		}

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Home));
        }

		protected override void OnResume ()
		{
			base.OnResume ();
			uiHelper.OnResume ();

			UpdateUI ();
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);
			uiHelper.OnSaveInstanceState (outState);

			outState.PutString (PENDING_ACTION_BUNDLE_KEY, pendingAction.ToString ());
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			uiHelper.OnActivityResult (requestCode, (int)resultCode, data);
		}

		protected override void OnPause ()
		{
			base.OnPause ();
			uiHelper.OnPause ();
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			uiHelper.OnDestroy ();
		}

		private void OnSessionStateChange (Session session, SessionState state, Exception exception)
		{
			if (pendingAction != PendingAction.NONE &&
				(exception is FacebookOperationCanceledException ||
				exception is FacebookAuthorizationException)) {
				new AlertDialog.Builder (this)
					.SetTitle (Resource.String.cancelled)
						.SetMessage (Resource.String.permission_not_granted)
						.SetPositiveButton (Resource.String.ok, (object sender, DialogClickEventArgs e) => {})
						.Show ();
				pendingAction = PendingAction.NONE;
			} else if (state == SessionState.OpenedTokenUpdated) {
				HandlePendingAction ();
			}
			UpdateUI ();
		}

		private void UpdateUI ()
		{
			Session session = Session.ActiveSession;
			bool enableButtons = (session != null && session.IsOpened);

            //BtnProceed.Enabled = (enableButtons);
            BtnProceed.Enabled = true;

			if (enableButtons && user != null) {
				profilePictureView.ProfileId = (user.Id);
				greeting.Text = GetString (Resource.String.hello_user, new Java.Lang.String (user.FirstName));
			} else {
				profilePictureView.ProfileId = (null);
				greeting.Text = (null);
			}
		}

		private void HandlePendingAction ()
		{
			PendingAction previouslyPendingAction = pendingAction;
			// These actions may re-set pendingAction if they are still pending, but we assume they
			// will succeed.
			pendingAction = PendingAction.NONE;

			switch (previouslyPendingAction) {
			case PendingAction.POST_PHOTO:
				PostPhoto ();
				break;
			case PendingAction.POST_STATUS_UPDATE:
				PostStatusUpdate ();
				break;
			}
		}

		private void ShowPublishResult (String message, IGraphObject result, FacebookRequestError error)
		{
			String title = null;
			String alertMessage = null;
			if (error == null) {
				title = GetString (Resource.String.success);

				var id = result.GetProperty("id").ToString();
				alertMessage = GetString (Resource.String.successfully_posted_post, message, id);
			} else {
				title = GetString (Resource.String.error);
				alertMessage = error.ErrorMessage;
			}

			new AlertDialog.Builder (this)
				.SetTitle (title)
					.SetMessage (alertMessage)
					.SetPositiveButton (Resource.String.ok, (object sender, DialogClickEventArgs e) => {})
					.Show ();
		}

		class RequestCallback : Java.Lang.Object, Request.ICallback
		{
			Action<Response> action;

			public RequestCallback (Action<Response> action)
			{
				this.action = action;
			}

			public void OnCompleted (Response response)
			{
				action (response);
			}
		}

		private void PostStatusUpdate ()
		{
			if (user != null && HasPublishPermission ()) {
				string message = GetString (Resource.String.status_update, user.FirstName, (DateTime.Now.ToString ()));
				Request request = Request.NewStatusUpdateRequest (Session.ActiveSession, message, new RequestCallback (response => ShowPublishResult (message, response.GraphObject, response.Error)));
				request.ExecuteAsync ();
			} else {
				pendingAction = PendingAction.POST_STATUS_UPDATE;
			}
		}

		private void PostPhoto ()
		{
			if (HasPublishPermission ()) {
				Bitmap image = BitmapFactory.DecodeResource (this.Resources, Resource.Drawable.Icon);
				Request request = Request.NewUploadPhotoRequest (Session.ActiveSession, image, new RequestCallback (response => {
					ShowPublishResult (GetString (Resource.String.photo_post), response.GraphObject, response.Error);
				}));
				request.ExecuteAsync ();
			} else {
				pendingAction = PendingAction.POST_PHOTO;
			}
		}

		class ErrorListener : Java.Lang.Object, PickerFragment.IOnErrorListener
		{
			Action<PickerFragment, FacebookException> action;

			public ErrorListener (Action<PickerFragment, FacebookException> action)
			{
				this.action = action;
			}

			public void OnError (PickerFragment p0, FacebookException p1)
			{
				action (p0, p1);
			}
		}

		private void ShowAlert (String title, String message)
		{
			new AlertDialog.Builder (this)
				.SetTitle (title)
					.SetMessage (message)
					.SetPositiveButton (Resource.String.ok, (object sender, DialogClickEventArgs e) => {})
					.Show ();
		}

		private bool HasPublishPermission ()
		{
			Session session = Session.ActiveSession;
			return session != null && session.Permissions.Contains ("publish_actions");
		}
    }
}

