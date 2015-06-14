﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Reflection;

namespace WpfControlsAndStylesDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LocalResourceManager.GetInstance();
            resource.Source = new Uri("pack://application:,,,/WpfControlsAndStyles;component/ControlStyles/Default/Generic.xaml", UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries.Add(resource);
            this.Style = (Style)resource["WindowStyle"];
            this.MaxWidth = SystemParameters.WorkArea.Width + 8;
            this.MaxHeight = SystemParameters.WorkArea.Height + 8;
            this.MinHeight = 120;
            this.MinWidth = 200;
            myTemplate = (ControlTemplate)(this.Style.Setters[3] as Setter).Value;
            this.Loaded += FussWindow_Loaded;
        }

        private bool _showMax = true;
        public bool ShowMax
        {
            get
            {
                return _showMax;
            }
            set
            {
                _showMax = value;
            }
        }

        private ControlTemplate myTemplate;
        private Style myStyle;
        private ResourceDictionary resource = new ResourceDictionary();

        private const int WM_NCHITTEST = 0x0084;
        private const int WM_GETMINMAXINFO = 0x0024;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_RESTORE = 0xF120;

        private readonly int agWidth = 4; //拐角宽度
        private readonly int bThickness = 4; // 边框宽度
        private Point mousePoint = new Point(); //鼠标坐标

        private Menu moduleMenu;

        //根据对应的配置生成菜单项，实现注入
        private List<MenuItem> BuildMenu(List<ModuleInfo> modules)
        {
            var menuitems = new List<MenuItem>();
            if (modules == null || modules.Count == 0)
                return menuitems;
            foreach (var module in modules)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Header = module.MenuName;
                menuItem.Tag = module;
                if (!string.IsNullOrEmpty(module.ClassName))
                    menuItem.Click += menuItem_Click;
                var children = BuildMenu(module.ModuleChildren);
                children.ForEach(item => menuItem.Items.Add(item));
                menuitems.Add(menuItem);
            }
            return menuitems;
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            ModuleInfo module = (sender as MenuItem).Tag as ModuleInfo;
            string UriPath = string.Format("pack://application:,,,/WpfControlsAndStyles;component/ControlStyles/{0}/Generic.xaml",module.ClassName);
            resource.Source = new Uri(UriPath, UriKind.RelativeOrAbsolute);
            
            Application.Current.Resources.MergedDictionaries.Clear();
            LocalResourceManager.Dispose();
            LocalResourceManager.GetInstance();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }

        private void LoadMenu()
        {
            moduleMenu = (Menu)myTemplate.FindName("ModuleMenu", this);
            var modules = ModuleHelper.GetModuleInfo();
            var menuitems = BuildMenu(modules);
            menuitems.ForEach(item => moduleMenu.Items.Add(item));
        }
        private void FussWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMenu();
            object temp = myTemplate.FindName("MinButton",this);
            ((TextBlock)myTemplate.FindName("TitleText", this)).Text = this.Title;
            ((Button)myTemplate.FindName("MinButton", this)).Click += (s1, e1) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            ((Button)myTemplate.FindName("CloseButton", this)).Click += (s2, e2) =>
            {
                this.Close();
            };
            Button maxButton = (Button)myTemplate.FindName("MaxButton", this);

            if (!ShowMax)
            {
                maxButton.Visibility = Visibility.Collapsed;
                (myTemplate.FindName("MaxSplitter", this) as Rectangle).Visibility = Visibility.Collapsed;
            }
            else
                maxButton.Visibility = Visibility.Visible;
            maxButton.Click += (s3, e3) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    maxButton.Style = (Style)resource["MaxButton"];
                    ((Border)(myTemplate.FindName("FussWindowBorder", this))).BorderThickness = new Thickness(0);
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    maxButton.Style = (Style)resource["NormalButton"];
                    ((Border)(myTemplate.FindName("FussWindowBorder", this))).BorderThickness = new Thickness(1);
                    this.WindowState = WindowState.Normal;
                }
            };
            this.SizeChanged += (s4, e4) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    maxButton.Style = (Style)resource["NormalButton"];
                    ((Border)(myTemplate.FindName("FussWindowBorder", this))).BorderThickness = new Thickness(1);
                }
                else
                {
                    maxButton.Style = (Style)resource["MaxButton"];
                    ((Border)(myTemplate.FindName("FussWindowBorder", this))).BorderThickness = new Thickness(0);
                }
            };
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }
        }

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_NCHITTEST:
                    mousePoint.X = (lParam.ToInt32() & 0xFFFF);
                    mousePoint.Y = (lParam.ToInt32() >> 16);
                    handled = true;
                    //窗体为最大化时（如果最大化，Left、Top属性都会造成影响）
                    if (this.WindowState == WindowState.Normal)
                    {
                        #region 拖拽改变窗体大小
                        //左上角
                        if ((mousePoint.Y - this.Top - 6 <= this.agWidth) && (mousePoint.X - this.Left - 6 <= this.agWidth))
                        {
                            return new IntPtr((int)HitTest.HTTOPLEFT);
                        }
                        //左下角
                        if ((this.ActualHeight + this.Top - this.mousePoint.Y <= this.agWidth) && (this.mousePoint.X - this.Left - 6 <= this.agWidth))
                        {
                            return new IntPtr((int)HitTest.HTBOTTOMLEFT);
                        }
                        //右上角
                        if ((this.mousePoint.Y - this.Top - 6 <= this.agWidth) && (this.ActualWidth + this.Left - this.mousePoint.X <= this.agWidth))
                        {
                            return new IntPtr((int)HitTest.HTTOPRIGHT);
                        }
                        //右下角
                        if ((this.ActualWidth + this.Left - this.mousePoint.X <= this.agWidth) && (this.ActualHeight + this.Top - this.mousePoint.Y <= this.agWidth))
                        {
                            return new IntPtr((int)HitTest.HTBOTTOMRIGHT);
                        }
                        //左侧
                        if (this.mousePoint.X - this.Left - 6 <= this.bThickness)
                        {
                            return new IntPtr((int)HitTest.HTLEFT);
                        }
                        //右侧
                        if (this.ActualWidth + this.Left - this.mousePoint.X <= this.bThickness)
                        {
                            return new IntPtr((int)HitTest.HTRIGHT);
                        }
                        //上侧
                        if (this.mousePoint.Y - this.Top - 6 <= this.bThickness)
                        {
                            return new IntPtr((int)HitTest.HTTOP);
                        }
                        //下侧
                        if (this.ActualHeight + this.Top - this.mousePoint.Y <= this.bThickness)
                        {
                            return new IntPtr((int)HitTest.HTBOTTOM);
                        }
                        #endregion
                        //正常情况下移动窗体
                        if (this.mousePoint.Y - this.Top > 0 && this.mousePoint.Y - this.Top < 40 && this.Left + this.ActualWidth - mousePoint.X > 74)
                        {
                            return new IntPtr((int)HitTest.HTCAPTION);
                        }
                    }
                    //最大化时移动窗体，让窗体正常化
                    else
                    {
                        if (this.mousePoint.Y < 40 && this.ActualWidth - mousePoint.X > 74)
                        {
                            return new IntPtr((int)HitTest.HTCAPTION);
                        }
                    }
                    ////将q其他区域设置为客户端，避免鼠标事件被屏蔽
                    return new IntPtr((int)HitTest.HTCLIENT);
            }
            return IntPtr.Zero;
        }

    }

    /// <summary>
    /// 包含了鼠标的各种消息
    /// </summary>
    public enum HitTest : int
    {
        HTERROR = -2,
        HTTRANSPARENT = -1,
        HTNOWHERE = 0,
        HTCLIENT = 1,
        HTCAPTION = 2,
        HTSYSMENU = 3,
        HTGROWBOX = 4,
        HTSIZE = HTGROWBOX,
        HTMENU = 5,
        HTHSCROLL = 6,
        HTVSCROLL = 7,
        HTMINBUTTON = 8,
        HTMAXBUTTON = 9,
        HTLEFT = 10,
            
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER = 18,
        HTREDUCE = HTMINBUTTON,
        HTZOOM = HTMAXBUTTON,
        HTSIZEFIRST = HTLEFT,
        HTSIZELAST = HTBOTTOMRIGHT,
        HTOBJECT = 19,
        HTCLOSE = 20,
        HTHELP = 21
    }
}