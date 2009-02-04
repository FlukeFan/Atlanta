
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

using Atlanta.Application.Domain.DomainBase;
using Atlanta.Application.Domain.Lender;
using Atlanta.Application.Services.Interfaces;
using Atlanta.Application.Services.ServiceBase;

namespace Atlanta.Presentation
{

    public class Main : UserControl
    {

        private Button _test1;
        private Button _test2;
        private Button _test3;
        private Button _test4;
        private ScrollViewer _scrollViewer;
        private StackPanel _messages;
        private MediaServiceClient _mediaService;
        private Media _lastCreatedMedia;

        public Main()
        {
            Loaded += new RoutedEventHandler(Main_Loaded);
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            _mediaService = new MediaServiceClient(new EndpointAddress("http://" + System.Windows.Application.Current.Host.Source.Host + "/atlanta/web/services/MediaService.svc"), Dispatcher);
            _mediaService.GetMediaListCompleted += MediaService_GetMediaListCompleted;
            _mediaService.CreateCompleted += MediaService_CreateCompleted;
            _mediaService.ModifyCompleted += MediaService_ModifyCompleted;

            _scrollViewer = (ScrollViewer)FindName("_scrollViewer");
            _messages = (StackPanel)FindName("_messages");

            _test1 = (Button)FindName("_test1");
            _test2 = (Button)FindName("_test2");
            _test3 = (Button)FindName("_test3");
            _test4 = (Button)FindName("_test4");

            _test1.Click += new RoutedEventHandler(Test1_Click);
            _test2.Click += new RoutedEventHandler(Test2_Click);
            _test3.Click += new RoutedEventHandler(Test3_Click);
            _test4.Click += new RoutedEventHandler(Test4_Click);
            _test4.IsEnabled = false;

            Write("Loaded");
        }

        private void Test1_Click(object sender, RoutedEventArgs e)
        {
            Write("Calling GetMediaList");
            User user = new User() { Id=1, Login="user" };
            _mediaService.GetMediaList(user, ClientQuery.For<Media>());
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

        private void Test2_Click(object sender, RoutedEventArgs e)
        {
            Write("Calling GetMediaList");
            User user = new User() { Id=1, Login="user" };
            _mediaService.GetMediaList(user, ClientQuery.For<Media>().Add<Media>(m => m.Type == MediaType.Book));
        }

        private void Test3_Click(object sender, RoutedEventArgs e)
        {
            User user = new User() { Id=1, Login="user" };
            Media media = new Media() { Name="media " + DateTime.Now.ToString(), Description="media", Type=MediaType.Book };
            _mediaService.Create(user, media);
        }

        private void MediaService_CreateCompleted(ServiceCallStatus status)
        {
            try
            {
                _lastCreatedMedia = _mediaService.Create(status);
                Write("Media created: " + _lastCreatedMedia.Name + " (" + _lastCreatedMedia.Id + ")");
                _test4.IsEnabled = true;
            }
            catch (DuplicationException de)
            {
                Write("Attempt to create duplicate Media: " + de.DuplicateValue + " (" + de.DuplicateId + ")");
            }
        }

        private void Test4_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MediaService_ModifyCompleted(ServiceCallStatus status)
        {
            throw new NotImplementedException();
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