using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WLDataAnalysis
{
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : UserControl
    {
        Thread loadingThread;
        Storyboard Showboard;
        Storyboard Hideboard;
        private delegate void ShowDelegate(string txt);
        private delegate void HideDelegate();
        ShowDelegate showDelegate;
        HideDelegate hideDelegate;
        bool bStop;

        public AboutPage()
        {
            InitializeComponent();

            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;

            
            showDelegate = new ShowDelegate(this.showText);
            hideDelegate = new HideDelegate(this.hideText);
            Showboard = this.Resources["showStoryBoard"] as Storyboard;
            Hideboard = this.Resources["HideStoryBoard"] as Storyboard;

            bStop = false;
        }

        private void OnAboutPageLoaded(object sender, RoutedEventArgs e)
        {
            loadingThread = new Thread(load);
            loadingThread.Start();
        }

        private void load()
        {
            while (!bStop)
            {
                Thread.Sleep(1000);
                if (bStop)
                    break;
                this.Dispatcher.Invoke(showDelegate, "" + 
                                                     "\r\nSpecial thanks to:" + 
                                                     "\r\nDr. Majid Jandaghi Alaee" +
                                                     "\r\nParham Pad" +
                                                     "\r\nMostafa Nazar Ali");
                Thread.Sleep(1000);
                //load data 
                if (bStop)
                    break;
                this.Dispatcher.Invoke(hideDelegate);

                Thread.Sleep(1000);
                if (bStop)
                    break;
                this.Dispatcher.Invoke(showDelegate, "Import data from different kind of text formats" + 
                                                     "\r\nDetect Invalid Data, Noises, and Spikes" +
                                                     "\r\nDespike and Smooth Data and Export");
                Thread.Sleep(1000);
                //load data
                if (bStop)
                    break;
                this.Dispatcher.Invoke(hideDelegate);
            }
        }

        private void showText(string txt)
        {
            txtLoading.Text = txt;
            BeginStoryboard(Showboard);
        }

        private void hideText()
        {
            BeginStoryboard(Hideboard);
        }

        private void OnAboutPageUnloaded(object sender, RoutedEventArgs e)
        {
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            lock (this)
            {
                bStop = true;
            }
        }
    }
}
