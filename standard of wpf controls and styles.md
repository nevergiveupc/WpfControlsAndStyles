#WPF样式与自定义控件定义及命名规范
##样式定义及命名规范
**注意：**

1. 以下所有控件的样式，建议使用Style来定义，不建议直接使用ControlTemplate等模板来实现对控件的样式定义。
2. 不建议将所有控件的大小设置为固定长度，应该是用户可自定义大小的。
3. 不建议在样式中使用图片，建议通过绘图的方式实现
4. 在使用Blend设计样式时，应将字型单位设置为像素（px）,因为Blend默认的字型单位是磅（pt）
###Button
命名：ButtonStyle+序号
如ButtonStyle1，ButtonStyle2……

样式定义：使用Style定义Button样式，并将TargetType设置为`Button`且Key设置为样式名称。
如：`<Style TargetType="Button" x:Key="ButtonStyle1">`

###CheckBox
命名：CheckBoxStyle+序号
如CheckBoxStyle1，CheckBoxStyle2……

样式定义：使用Style定义CheckBox样式，并将TargetType设置为`CheckBox`且Key设置为样式名称。
如：`<Style TargetType="{x:Type CheckBox}" x:Key="CheckBoxStyle1">`
###ComboBox
命名：ComboBoxStyle+序号
如ComboBoxStyle1，ComboBoxStyle2……

样式定义：使用Style定义ComboBox样式，并将TargetType设置为`ComboBox`且Key设置为样式名称。
如：`<Style TargetType="{x:Type CheckBox}" x:Key="CheckBoxStyle1">`

由于ComboBox有子项，所以同样可以对其子项定义样式。

子项命名：ComboBoxItemStyle+序号

如ComboBoxItemStyle1,ComboBoxItemStyle2……

样式定义：使用Style定义ComboBoxItem样式，并将TargetType设置为`ComboBoxItem`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ComboBoxItem}" x:Key="ComboBoxItemStyle1">`
###ToolTip
命名：ToolTipStyle+序号
如ToolTipStyle1，ToolTipStyle2……
在此建议不定义MenuItem的Key，应忽略Key的设置，直接将其应用到所有MenuItem

样式定义：使用Style定义ToolTip样式，并将TargetType设置为`ToolTip`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ToolTip}" x:Key="ToolTipStyle1">`

**注意：**这里ToolTip建议不要采用WPF默认的，应该自定义样式，因为WPF默认的ToolTip中的字体不会采用整个窗体中定义的字体，故自定义ToolTip样式需要指定该样式的字体。按照规范，应该指定首要字体为“微软雅黑”，次要字体“Ms Sans Serif”.
###DataGrid
命名：DataGridStyle+序号
如：DataGridStyle1，DataGridStyle2……

样式定义：使用Style定义DataGrid样式，并将TargetType设置为`DataGrid`且Key设置为样式名称。
如：`<Style TargetType="{x:Type DataGrid}" x:Key="DataGridStyle1">`

由于DataGrid涉及到多个样式设置，如DataGridColumnHeader、DataGridRowHeader、DataGridRow、DataGridCell等，在此建议直接定义这些样式，使其作用于所有DataGrid，无需设置其Key。
###Expander
命名：Expander+序号
如ExpanderStyle1，ExpanderStyle2……

样式定义：使用Style定义Expander样式，并将TargetType设置为`Expander`且Key设置为样式名称。
如：`<Style TargetType="{x:Type Expander}" x:Key="ExpanderStyle1">`
###GroupBox
命名：GroupBoxStyle+序号
如：GroupBoxStyle1，GroupBoxStyle2……

样式定义：使用Style定义GroupBox样式，并将TargetType设置为`GroupBox`且Key设置为样式名称。
如：`<Style TargetType="{x:Type GroupBox}" x:Key="GroupBoxStyle1">`
###ListBox
命名ListBoxStyle+序号
如ListBoxStyle1，ListBoxStyle2……

样式定义：使用Style定义ListBox样式，并将TargetType设置为`ListBox`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ListBox}" x:Key="ListBoxStyle1">`

由于ListBox有子项，所以同样可以对其子项定义样式。

子项命名：ListBoxItemStyle+序号

如ListBoxItemStyle1,ListBoxItemStyle2……

样式定义：使用Style定义ListBoxItem样式，并将TargetType设置为`ListBoxItem`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ListBoxItem}" x:Key="ListBoxItemStyle1">`
###ListView
命名：ListViewStyle+序号
如：ListViewStyle1，ListViewStyle2……

样式定义：使用Style定义ListView样式，并将TargetType设置为`ListView`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ListView}" x:Key="ListViewStyle1">`

由于ListView有子项，所以同样可以对其子项定义样式。

子项命名：ListViewItemStyle+序号

如ListViewItemStyle1,ListViewItemStyle2……

样式定义：使用Style定义ListViewItem样式，并将TargetType设置为`ListViewItem`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ListViewItem}" x:Key="ListViewItemStyle1">`

由于ListView涉及到多个样式设置，如GridViewColumnHeader、ScrollViewer等，在此建议直接定义这些样式，使其作用于所有ListView，无需设置其Key。
###Menu
Menu分为主菜单和上下文菜单：

1. 主菜单MenuItem样式定义及命名

	命名：在此建议不定义MenuItem的Key，应忽略Key的设置，直接将其应用到所有MenuItem

	样式定义：使用Style定义MenuItem样式，并将TargetType设置为`MenuItem`。
	如：`<Style TargetType="{x:Type MenuItem}">`
2. 上下文菜单ContextMenu样式定义及命名
	命名：在此建议不定义ContextMenu的Key，应忽略Key的设置，直接将其应用到所有ContextMenu

	样式定义：使用Style定义ContextMenu样式，并将TargetType设置为`ContextMenu`。
	如：`<Style TargetType="{x:Type ContextMenu}">`

###ProgressBar
命名：ProgressBarStyle+序号
如：ProgressBarStyle1，ProgressBarStyle2……

样式定义：使用Style定义ProgressBar样式，并将TargetType设置为`ProgressBar`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ProgressBar}" x:Key="ProgressBarStyle1">`
###RadioButton
命名：RadioButtonStyle+序号
如：RadioButtonStyle1，RadioButtonStyle2……

样式定义：使用Style定义RadioButton样式，并将TargetType设置为`RadioButton`且Key设置为样式名称。
如：`<Style TargetType="{x:Type RadioButton}" x:Key="RadioButtonStyle1">`
###ScrollBar
命名：在此建议不定义ScrollBar的Key，应忽略Key的设置，直接将其应用到所有ScrollBar

样式定义：使用Style定义ScrollBar样式，并将TargetType设置为`ScrollBar`。
如：`<Style TargetType="{x:Type ScrollBar}">`
###ScrollViewer
命名：在此建议不定义ScrollViewer的Key，应忽略Key的设置，直接将其应用到所有ScrollViewer

样式定义：使用Style定义ScrollViewer样式，并将TargetType设置为`ScrollViewer`。
如：`<Style TargetType="{x:Type ScrollViewer}">`
###Slider
命名：SliderStyle+序号
如SliderStyle1，SliderStyle2……

样式定义：使用Style定义Slider样式，并将TargetType设置为`Slider`且Key设置为样式名称。
如：`<Style TargetType="{x:Type Slider}" x:Key="SliderStyle1">`
###StatusBar
命名：StatusBarStyle+序号
如StatusBarStyle1，StatusBarStyle2……

样式定义：使用Style定义StatusBar样式，并将TargetType设置为`StatusBar`且Key设置为样式名称。
如：`<Style TargetType="{x:Type StatusBar}" x:Key="StatusBarStyle1">`
###TabControl
命名：TabControlStyle+序号
如TabControlStyle1，TabControlStyle2……

样式定义：使用Style定义TabControl样式，并将TargetType设置为`TabControl`且Key设置为样式名称。
如：`<Style TargetType="{x:Type TabControl}" x:Key="TabControlStyle1">`

由于TabControl有子项，所以同样可以对其子项定义样式。

子项命名：TabItemStyle+序号

如TabItemStyle1,TabItemStyle2……

样式定义：使用Style定义TabItem样式，并将TargetType设置为`TabItem`且Key设置为样式名称。
如：`<Style TargetType="{x:Type TabItem}" x:Key="TabItemStyle1">`
###TextBox
命名：TextBoxStyle+序号
如：TextBoxStyle1，TextBoxStyle2……

样式定义：使用Style定义TextBox样式，并将TargetType设置为`TextBox`且Key设置为样式名称。
如：`<Style TargetType="{x:Type TextBox}" x:Key=TextBoxStyle1">`
###ToolBar
命名：ToolBarStyle+序号
如：ToolBarStyle1，ToolBarStyle2……

样式定义：使用Style定义ToolBar样式，并将TargetType设置为`ToolBar`且Key设置为样式名称。
如：`<Style TargetType="{x:Type ToolBar}" x:Key=ToolBarStyle1">`
###TreeView
命名：TreeViewStyle+序号
如：TreeViewStyle1，TreeViewStyle2……

样式定义：使用Style定义TreeView样式，并将TargetType设置为`TreeView`且Key设置为样式名称。
如：`<Style TargetType="{x:Type TreeView}" x:Key="TreeViewStyle1">`

由于TreeView有子项，所以同样可以对其子项定义样式。

子项命名：TreeViewItemStyle+序号

如TreeViewItemStyle1,TreeViewItemStyle2……

样式定义：使用Style定义TreeViewItem样式，并将TargetType设置为`TreeViewItem`且Key设置为样式名称。
如：`<Style TargetType="{x:Type TreeViewItem}" x:Key="TreeViewItemStyle1">`
###Color
命名：SolidBrushColor+序号

定义：使用`SolidBrushColor`来定义系统中使用的所有颜色，然后再通过`Setter`将`Foreground`或`Background`等属性设置为该颜色，来保证整个系统中的配色一致。

如：

	<SolidColorBrush x:Key="SolidBrushColor1" Color="#FF65696B"/>

    <Style TargetType="{x:Type TextBox}" x:Key="TextBoxStyle">
        <Setter Property="Foreground" Value="{StaticResource ResourceKey=SolidBrushColor1}"/>
    </Style>
###Window
1. Window样式命名及定义

	命名：WindowStyle
	由于Window的样式是相对唯一的，不会出现多个Window样式，故在此设置为WindowStyle
	
	样式定义：使用Style定义Window样式，并将TargetType设置为`Window`且Key设置为样式名称。
	如：`<Style TargetType="{x:Type Window}" x:Key=WindowStyle">`

2. 最小化按钮样式命名及定义

	命名：MinButton
	在此命名唯一确定

	样式定义：使用Style定义Button样式，并将TargetType设置为`Button`且Key设置为样式名称。
	如：`<Style TargetType="{x:Type Button}" x:Key=MinButton">`

3. 最大化按钮样式命名及定义

	命名：MaxButton
	在此命名唯一确定

	样式定义：使用Style定义Button样式，并将TargetType设置为`Button`且Key设置为样式名称。
	如：`<Style TargetType="{x:Type Button}" x:Key=MaxButton">`

4. 还原按钮样式命名及定义

	命名：NormalButton
	在此命名唯一确定

	样式定义：使用Style定义Button样式，并将TargetType设置为`Button`且Key设置为样式名称。
	如：`<Style TargetType="{x:Type Button}" x:Key=NormalButton">`

5. 关闭按钮样式命名及定义

	命名：CloseButton
	在此命名唯一确定

	样式定义：使用Style定义Button样式，并将TargetType设置为`Button`且Key设置为样式名称。
	如：`<Style TargetType="{x:Type Button}" x:Key=CloseButton">`

##自定义控件定义及命名规范
###自定义控件定义：

1. 一般自定义控件，是由于现有（WPF）控件无法满足项目中的某些需求，所以自定义的控件相对于继承的父控件有新增功能。

2. 需要继承对应的控件，如要对`ComboBox`实现新的功能，就需要继承自`ComboBox`，这样就能保证该控件的原有功能正常，也能减少很多的开发工作量。

###命名规范：
UC+控件类型+控件功能，其中UC为UserControl（用户自定义控件）的缩写

如UCComboBoxAutoSort，表示该自定义控件可以实现自动排序功能；UCDataGridAutoSplitPage，表示该自定义控件可以实现自动分页功能。