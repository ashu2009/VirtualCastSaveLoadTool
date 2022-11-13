using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace VirtualCastSaveLoadTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VirtualCastClass.VirtualCast virtualCast = new VirtualCastClass.VirtualCast();

        public MainWindow()
        {
            InitializeComponent();
            SetupTimer();
            VCINameListView.ItemsSource = virtualCast.GetSelectVCITabItemSource();
            VCIGetMessageKindListView.ItemsSource = virtualCast.GetSelectVCIKindItemSource();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (sender == LoadMenuItem) virtualCast.LoadVCIContaints();
        }

        private void ConnectTcpPortButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ConnectTcpPortTextBox.Text, out int tcpPort))
            {
                MessageBox.Show("正常なポート番号ではありません");
                return;
            }
            virtualCast.Connect(tcpPort);
        }

        private void DisConnectTcpPortButton_Click(object sender, RoutedEventArgs e)
        {
            virtualCast.DisConnect();
        }

        private void VCIGetMessageKindListView_Selected(object sender, RoutedEventArgs e)
        {
            SetVCIGetMessageListViewItemsSource();
        }

        private void VCINameListView_Selected(object sender, RoutedEventArgs e)
        {
            SetVCIGetMessageListViewItemsSource();
        }

        private void SetVCIGetMessageListViewItemsSource()
        {
            if ((VCINameListView.SelectedIndex < 0) || (VCIGetMessageKindListView.SelectedIndex < 0)) return;
            ObservableCollection<BindingClass.SelectVCITab> selectVCITabItemSource = (ObservableCollection<BindingClass.SelectVCITab>)VCINameListView.ItemsSource;
            ObservableCollection<BindingClass.SelectVCIKind> selectVCIKindItemSource = (ObservableCollection<BindingClass.SelectVCIKind>)VCIGetMessageKindListView.ItemsSource;
            string vciName = selectVCITabItemSource.ElementAt(VCINameListView.SelectedIndex).vciName;
            string vciId = selectVCITabItemSource.ElementAt(VCINameListView.SelectedIndex).vciID;
            string vciMessageKind = selectVCIKindItemSource.ElementAt(VCIGetMessageKindListView.SelectedIndex).messageType;
            VCIGetMessageListView.ItemsSource = virtualCast.GetSelectVCIContaintsItemSource(vciName + vciId, vciMessageKind);
        }

        private void VCIGetMessageListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((VCIGetMessageListView.SelectedIndex < 0)) return;
            ItemCollection itemCollection = VCIGetMessageListView.Items;
            BindingClass.SelectVCIContaints selectItem = (BindingClass.SelectVCIContaints)itemCollection[VCIGetMessageListView.SelectedIndex];
            string saveContaints = selectItem.messageContaints;

            if ((VCINameListView.SelectedIndex < 0) || (VCIGetMessageKindListView.SelectedIndex < 0)) return;
            ObservableCollection<BindingClass.SelectVCIKind> selectVCIKindItemSource = (ObservableCollection<BindingClass.SelectVCIKind>)VCIGetMessageKindListView.ItemsSource;
            string vciMessageKind = selectVCIKindItemSource.ElementAt(VCIGetMessageKindListView.SelectedIndex).messageType;
            virtualCast.SaveVCIContaints(vciMessageKind, saveContaints);
        }

        // タイマメソッド
        private void MyTimerMethod(object sender, EventArgs e)
        {
            if (virtualCast.GetIsConnectingTcp()) TCPConnectLavel.Content ="接続中";
            else TCPConnectLavel.Content = "未接続";
        }

        // タイマのインスタンス
        private DispatcherTimer _timer;

        // タイマを設定する
        private void SetupTimer()
        {
            // タイマのインスタンスを生成
            _timer = new DispatcherTimer(); // 優先度はDispatcherPriority.Background
                                            // インターバルを設定
            _timer.Interval = new TimeSpan(0, 0, 1);
            // タイマメソッドを設定
            _timer.Tick += new EventHandler(MyTimerMethod);
            // タイマを開始
            _timer.Start();

            // 画面が閉じられるときに、タイマを停止
            this.Closing += new CancelEventHandler(StopTimer);
        }

        // タイマを停止
        private void StopTimer(object sender, CancelEventArgs e)
        {
            _timer.Stop();
        }
    }
}
