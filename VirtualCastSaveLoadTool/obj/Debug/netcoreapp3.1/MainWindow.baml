   M S B A M L   `   `   `  ????R  NVirtualCastSaveLoadTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:VirtualCastSaveLoadToolVirtualCastSaveLoadTool  5   	   (    "VirtualCastSaveLoadTool.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:VirtualCastSaveLoadTool     5   	     ? Title$  
MainWindow??5   	   $	??450??6   $	??800??6)   .??5	      ?  ? RowDefinitions 5
   
   ?? 5      $??20 ?6   ?? 5      $??20 ?6   ?? 5      $??55 ?6   ?? 5      $??55 ?6   ?? 5      $
??100* ?6   $	??100??6*   ?? 5      $??25 ?6   .+?5   
   ? $??0??6   .+?5      ]? .+?5      o? $	??Top=?6   .@?5      m? -   5         m?Name$ LoadMenuItem??5      $??	ロード??65   6B   5      5      5      ? 5   
   $??1??6   .+?5      ??  ?? Orientation$ 
Horizontal=?6   .+?5      ?? $K??EVCIスクリプトのロガーをホストするTCPポート番号：??6   $
??Wrap=?6?   ?? 5      -   5         ??Name$ ConnectTcpPortTextBox??5      $
??8080??69   $
??Left=?6E   $??Stretch=?6`   $??80??6|   $??RightToLeft=?6?   ?? 5      -   5         ??Name$ ConnectTcpPortButton??5      $??Connect??67   $
??Left=?6I   $	??Top=?6e   6}   ?? 5      -   5        $ DisConnectTcpPortButton??5      $??
DisConnect??6:   $
??Left=?6O   $	??Top=?6k   $??20,0,0,0q?6?   5      5       ? 5!   
   $??2??6   .+?5"      ?? -   5         ??Name$ VCINameListView??5"      $
??Auto=?60   ??5#      ?? 5$      .R?5%      ?? $ 
Horizontal=?6%   5&      ??5(      ?? 5)      .R?5*      ??  J? 	AllowDrop$
 True??6%   .+?5+      ?? $??CharacterEllipsis=?6A   $??18??6c   $??14(?6o   ??6(   ?? *	vciName+6o   ?? 5,      $??CharacterEllipsis=?6?   $??17??6a   $??9(?6m   ??6(   ?? *vciID+6m   5-      5.      50      51      ? 52   
   $??3??6   .+?53      ?? -   5        $ VCIGetMessageKindListView??53      $
??Auto=?6:   ??54      ?? 55      .R?56      ?? $ 
Horizontal=?6%   57      ??59      ?? 5:      .R?5;      ?? $
 True??6%   .+?5<      ?? $??CharacterEllipsis=?6E   $??18??6g   $??14(?6s   ??6(   ?? *messageType+6s   5=      5>      5@      5A      ? 5B   
   $??4??6   .+?5C      ?? -   5        $ VCIGetMessageListView??5C      $
??Auto=?66   ??5D      ?? 5E      .R?5F      ?? $ 
Horizontal=?6%   .+?5G      ?? $??35??6F   $??35??6T   ??6$   ?? *messagePicture+6T   ?? 5H      $??CharacterEllipsis=?6B   $??18??6d   $??14(?6p   ??6(   ?? *
unixDate+6p   ?? 5I      .+?5J   "   ?? $??CharacterEllipsis=?6I   $??18??6k   $??14(?6w   ??6,   ?? *messageType+6w   ?? 5K   "   $??CharacterEllipsis=?6N   $??17??6p   $??9(?6|   ??6,   ?? *messageContaints+6|   5L      5M      5N      5P      5Q      ? 5R   
   $??5??6   .+?5S      ?? -   5         ??Name$ TCPConnectLavel??5S      $??	未接続??6-   6=   5T      5U      5V      