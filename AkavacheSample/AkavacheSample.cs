using System;

using Xamarin.Forms;
using Akavache;
using System.Reactive.Linq;

namespace AkavacheSample
{
    public class App : Application
    {
        public App()
        {
            var nameEntry  = new Entry { Placeholder = "名前を入力" };
            var ageEntry   = new Entry { Placeholder = "年齢を入力(数値のみ)" };
            var saveButton = new Button { Text = "保存" };
            var loadButton = new Button { Text = "読み出し" };

            saveButton.Clicked += async (sender, e) => 
            {
                // Person に詰めて…
                var person = new Person { 
                    PersonName = nameEntry.Text, 
                    PersonAge  = Convert.ToInt16(ageEntry.Text) 
                };

                // 保存
                await BlobCache.LocalMachine.InsertObject("person", person); 
            };

            loadButton.Clicked += async (sender, e) => 
            {
                // Akavache で Person を読み出し
                var loaded = await BlobCache.LocalMachine.GetObject<Person>("person");
                // 各テキストボックスに設定
                nameEntry.Text = loaded.PersonName;
                ageEntry.Text  = loaded.PersonAge.ToString();
            };
                
            // The root page of your application
            MainPage = new ContentPage
            {
                Padding = new Thickness(20),
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        nameEntry,
                        ageEntry,
                        saveButton,
                        loadButton
                    }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

