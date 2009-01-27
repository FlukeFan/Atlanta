
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Atlanta.Application.Domain.Lender;
using Atlanta.Application.Services.Interfaces;
using Atlanta.Application.Services.ServiceBase;

namespace Atlanta.Presentation
{

    public class Main : UserControl
    {

        private Button _test1;
        private ScrollViewer _scrollViewer;
        private StackPanel _messages;
        private MediaServiceClient _mediaService;

        public Main()
        {
            Loaded += new RoutedEventHandler(Main_Loaded);
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            _mediaService = new MediaServiceClient(new EndpointAddress("http://" + System.Windows.Application.Current.Host.Source.Host + "/atlanta/web/services/MediaService.svc"), Dispatcher);
            _mediaService.GetMediaListCompleted += new Atlanta.Application.Services.ServiceBase.ServiceCallback(MediaService_GetMediaListCompleted);

            _test1 = (Button)FindName("_test1");
            _scrollViewer = (ScrollViewer)FindName("_scrollViewer");
            _messages = (StackPanel)FindName("_messages");

            _test1.Click += new RoutedEventHandler(Test1_Click);

            Write("Loaded");
        }

        private void Test1_Click(object sender, RoutedEventArgs e)
        {
            Write("Calling GetMediaList");
            User user = new User() { Id=1, Login="user" };
            _mediaService.GetMediaList(user, null);
        }

        private void MediaService_GetMediaListCompleted(ServiceCallStatus status)
        {
            IList<Media> mediaList =
                _mediaService.GetMediaList(status);

            Write("GetMediaList Response:");
            foreach (Media media in mediaList)
            {
                Write("Media: " + media.Id + ", " + media.Name + ", " + media.Type + ", " + media.Description);
            }
        }

        private void Write(string message)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = DateTime.Now + " - " + message;
            _messages.Children.Add(textBlock);
            _scrollViewer.ScrollToVerticalOffset(_scrollViewer.ExtentHeight);
        }

    }
}