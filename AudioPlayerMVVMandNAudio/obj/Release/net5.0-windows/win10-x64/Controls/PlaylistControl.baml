   M S B A M L   `   `   `  ���� S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  .    (AudioPlayerMVVMandNAudio.PlaylistControl   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:AudioPlayerMVVMandNAudio     .���   � RowDefinitions  �� $��* ��� $
��auto �.+��� -    ��Name$ PlaylistListbox��$��0��   PlaylistStyle#��[   �� SelectionMode$ Extended=� J� 	AllowDrop$
 True������ *SongsListObservable+ �� SelectedItem �� *SelectedTrack+ �� Mode$ TwoWay=� � FocusVisualStyle Z� ����  �� 
TargetType" ��.���  �� Property �@�FocusVisualStyle	 �� Value	 Z� -   ��  � ��	 �� 
 �� 
TargetType"
 ��.R�2   ,AudioPlayerMVVMandNAudio.PlaylistItemControl  #��z�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  � ��	 �#1D556D]�  ]� 
Conditions ��  �� Property �@��
IsSelected �� Value$
 True��./���  � ��	 �#196E92�� $��1��  WhiteTextblock#��[ $��Adding items...��$��5,0,0,0q� �� Padding$ 2q�$��11(�$��Center=�$
��Left=�y��� *Info+  BoolToVisibilityConverter �� 	Converter# [ � $��1��$��5,0,5,5q�$��Right=� � ColumnDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��1��#��[ $��Items:��$ 2q�$��11(�$��Center=��� $��2��#��[ $ 2q�$��11(�$��Center=����� *Items+�� -   $��3��  ImageButtonSmall#��[ 
 � Tag$8 2pack://application:,,,/Images/Playlist/addfile.png���� -    ��Name$ removeTrackButton��$��4��#��[ $; 5pack://application:,,,/Images/Playlist/removefile.png������  �� ElementName$ PlaylistListbox�� �� Path$ SelectedItems����� *!RemoveTracksFromPlaylistCommand+�� $��5��#��[ $6 0pack://application:,,,/Images/Playlist/clear.png������ *ClearPlaylistCommand+