###App.xaml设置###
1. 引用WPF样式库中的样式


 - 首先，需要确定要引用的资源是App级别，还是Window级别，还是UserControl级别：
	App级别就需要在App.xaml中引用资源；Window及UserControl级别就需要在对应的.xaml文件中引用对应样式的Generic.xaml资源即可。

         	<Application.Resources>
        		<ResourceDictionary>
            		<ResourceDictionary.MergedDictionaries>
                		<ResourceDictionary Source="pack://application:,,,/WpfControlsAndStyles;component/ControlStyles/Default/Generic.xaml" />
            		</ResourceDictionary.MergedDictionaries>
        		</ResourceDictionary>
    		</Application.Resources>

		或在主程序的构造函数中调用`Resource`进行引用
 	
			private ResourceDictionary resource = new ResourceDictionary();
			resource.Source = new Uri("pack://application:,,,/WpfControlsAndStyles;component/ControlStyles/Default/Generic.xaml", UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries.Add(resource);


 - 其次，引入资源后，便可通过设置控件的Style来实现对控件样式的更改
	
            <Button Command="{Binding UpdateCommand}" Content="更新->" Width="80" Height="30" Grid.Row="2" Style="{StaticResource ResourceKey=StepButtonStyle}"/>

2.控件样式命名
	
详见《standard of wpf controls and styles.md》
