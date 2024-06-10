using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace BloodGL.Core.Database.Firebase
{
    public class FirebaseNotification
    {
        public FirebaseNotification()
        {
            var credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bloodgl-firebase-adminsdk.json"));
            if (FirebaseApp.DefaultInstance == null)
            {
				FirebaseApp.Create(new AppOptions
				{
					Credential = credential
				});
			}
        }

        public async Task SendNotification(string title,string body,string deviceToken)
        {
            var message = new Message()
            {
                
                Notification = new Notification
                {
                    Title = title,
                    Body = body,
                },
                Token = deviceToken
			};

            var messaging = FirebaseMessaging.DefaultInstance;
            await messaging.SendAsync(message);
        }
    }
}
