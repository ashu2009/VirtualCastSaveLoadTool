   M S B A M L   `   `   `  ???? R  NVirtualCastSaveLoadTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:VirtualCastSaveLoadToolVirtualCastSaveLoadTool  (    "VirtualCastSaveLoadTool.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:VirtualCastSaveLoadTool       ? Title$  
MainWindow??$	??450??$	??800??.???  ? RowDefinitions ?? $??20 ??? $??20 ??? $??55 ??? $??55 ??? $
??100* ?$	??100???? $??25 ?.+?? $??0??.+?]? .+?o? $	??Top=?.@?m? -    m?Name$ LoadMenuItem??$??	ロード??? $??1??.+???  ?? Orientation$ 
Horizontal=?.+??? $K??EVCIスクリプトのロガーをホストするTCPポート番号：??$
??Wrap=??? -    ??Name$ ConnectTcpPortTextBox??$
??8080??$
??Left=?$??Stretch=?$??80??$??RightToLeft=??? -    ??Name$ ConnectTcpPortButton??$??Connect??$
??Left=?$	??Top=??? -   $ DisConnectTcpPortButton??$??
DisConnect??$
??Left=?$	??Top=?$??20,0,0,0q?? $??2??.+??? -    ??Name$ VCINameListView??$
??Auto=????? .R??? $ 
Horizontal=????? .R???  J? 	AllowDrop$
 True??.+??? $??CharacterEllipsis=?$??18??$??14(????? *	vciName+?? $??CharacterEllipsis=?$??17??$??9(????? *vciID+? $??3??.+??? -   $ VCIGetMessageKindListView??$
??Auto=????? .R??? $ 
Horizontal=????? .R??? $
 True??.+??? $??CharacterEllipsis=?$??18??$??14(????? *messageType+? $??4??.+??? -   $ VCIGetMessageListView??$
??Auto=????? .R??? $ 
Horizontal=?.+??? $??35??$??35?????? *messagePicture+?? $??CharacterEllipsis=?$??18??$??14(????? *
unixDate+?? .+??? $??CharacterEllipsis=?$??18??$??14(????? *messageType+?? $??CharacterEllipsis=?$??17??$??9(????? *messageContaints+? $??5??.+??? -    ??Name$ TCPConnectLavel??$??	未接続??