   M S B A M L   `   `   `  ����S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  5      2    ,AudioPlayerMVVMandNAudio.PlaylistItemControl   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:AudioPlayerMVVMandNAudio     5      .��5
      � 
������   J� 	AllowDrop$
  True��5       � ColumnDefinitions 5   
   �� 5      $��26 �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $:��4pack://application:,,,/Images/Playlist/notegreen.png��5         NoteImageStyle#��[  5      $��18��5      y�5      ��   BoolToVisibilityConverter �� 	Converter# [  �� Path$ IsAudioFilePlaying�� 5#   
   $��1��6   $��Stretch=�5$       � RowDefinitions 5&      �� 5'      $
��auto �6    �� 5(      $
��auto �6    .+�5,      �� $��0��6     PinkTextblock#��[ 5-      $��2,0,15,0q�5.      ��5/      �� *
TrackTitle+ �� Mode$ OneTime=��� 53      $��1��6   $
��Left=�54        GrayTextblock#��[ 55      $��2,0,15,1q�56      $��11(�57      ��58      �� *Artist+$ OneTime=�5;      5<      �� 5@   
   $��2��6     WhiteTextblock#��[ 5A      $��Right=�5C      $	��Top=�5D      $��2,0,5,0q�5E      $��11(�5F      ��5B      �� *	TimeTotal+$ OneTime=�5F      5H      5K      