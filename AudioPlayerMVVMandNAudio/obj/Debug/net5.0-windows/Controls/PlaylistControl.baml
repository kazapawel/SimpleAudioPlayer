   M S B A M L   `   `   `  ����S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  5      .    (AudioPlayerMVVMandNAudio.PlaylistControl   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:AudioPlayerMVVMandNAudio     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� -   5         ��Name$ PlaylistListbox��5      $��0��5         PlaylistStyle#��[  5       �� SelectionMode$ Extended=�5       J� 	AllowDrop$
 True��5      ��5      �� *SongsListObservable+ �� SelectedItem 5      �� *SelectedTrack+ �� Mode$ TwoWay=� � FocusVisualStyle 5      Z� ��5      �� 5        �� 
TargetType" ��6   .�5#      ��  �� Property �@�FocusVisualStyle	 �� Value	 69   Z� -   5        �� 5,       � ��	 5-      �� 5.      
 �� 
TargetType"
 ��6.   .R�5/   "   2   ,AudioPlayerMVVMandNAudio.PlaylistItemControl  #��z��6<   50      52       �� Triggers 55      P� 57       P� Property �@J�IsMouseOver P� Value$
 True��69   . �58      ��  � ��	 �#1D556D6;   59      ]� 5;       ]� 
Conditions 5<      �� 5=   "    �� Property �@��
IsSelected �� Value$
 True��6B   ./�5?      ��  � ��	 �#196E926;   5@      5C      5E      �� 5J   
   $��1��6     WhiteTextblock#��[ 5K      $��Adding items...��5M      $��5,0,0,0q�5N       �� Padding$ 2q�5O      $��11(�5P      $��Center=�5Q      $
��Left=�5R      y�5L      �� *Info+  BoolToVisibilityConverter �� 	Converter# [ 5R      � 5T   
   $��1��6   $��5,0,5,5q�5U      $��Right=�5V       � ColumnDefinitions 5W      �� 5Y      $
��auto �6#   �� 5Z      $
��auto �6#   �� 5[      $
��auto �6#   �� 5\      $
��auto �6#   �� 5]      $
��auto �6#   �� 5^      $
��auto �6#   �� 5a      $
��auto �6#   .+�5f      �� $��1��6   #��[ 5g      $��Items:��5h      $ 2q�5i      $��11(�5j      $��Center=�5k      �� 5o      $��2��6   #��[ 5p      $ 2q�5r      $��11(�5s      $��Center=�5t      ��5q      �� *Items+5t      �� 5x      -   5        $��3��5x        ImageButtonSmall#��[ 5y      
 � Tag$8 2pack://application:,,,/Images/Playlist/addfile.png��5z      5{      �� 5      -   5         ��Name$ removeTrackButton��5�      $��4��5      #��[ 5�      $; 5pack://application:,,,/Images/Playlist/removefile.png��5�      ��5�      ��  �� ElementName$ PlaylistListbox�� �� Path$ SelectedItems���5�      �� *!RemoveTracksFromPlaylistCommand+�� 5�      $��5��6   #��[ 5�      $6 0pack://application:,,,/Images/Playlist/clear.png��5�      ��5�      �� *ClearPlaylistCommand+5�      5�      5�      