   M S B A M L   `   `   `  ����S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  5   	   )    #AudioPlayerMVVMandNAudio.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:AudioPlayerMVVMandNAudio     5   	   -   5          � Title$  
MainWindow��5   	      MainWindowStyle2#��[  5	   	   $	��320��5
   	   $	��172��5   	   $	��320��5   	   $	��450��5   	     SourceCodeProMediumItalic#��[ 5   	    J� 	AllowDrop$
 True��5   	    � Icon$. (pack://application:,,,/Images/pigsad.png��5   	    � DataContext 5      ,   &AudioPlayerMVVMandNAudio.ApplicationVM  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $
��auto �6   �� 5!      $
��auto �6   �� 5$      $��* �6   �� 5'      $
��auto �6   .+�5-   
   �� $��0��6     BlueMainBrush#��[ 5.      ._�50      �  � ColumnDefinitions 51      �� 54      $
��auto �6'   �� 56      $��* �6'   �� 59      $
��auto �6'   �� 5<      $
��auto �6'   .+�5A      �� $��0��6     ImageButton#��[ 5B      $��21��5C      $��21��5D      $��5,0,0,0q�5E      
 � Tag$. (pack://application:,,,/Images/pigsad.png��5F      �� 5J      -   5        $��2��5J      $��30��5K        SystemIconButtonTextbox#��[ 5L      $ -��5M      5N      �� 5Q      -   5        $��3��5Q      $��30��5R      #��[ 5S      $ x��5T      5U      5X      5Y      � 5^   
   $��1��6    5`      �� 5c      $
��auto �6#   �� 5f      $
��auto �6#   �� 5i      $��* �6#   .+�5n      � $��0��6   $��5,5,0,5q�5o       5q      �� 5t      $
��auto �6'   �� 5w      $
��auto �6'   �� 5z      $
��auto �6'   �� 5}      $
��auto �6'   .+�5�      �� $��0��6   #��[ 5�      $��24��5�      $��24��5�      $: 4pack://application:,,,/Images/Transport/previous.png��5�      ��5�      �� *!PlaylistVM.PreviousTrackCommand+�� 5�      -   5         ��Name$ 
playButton��5�      $��1��5�      #��[ 5�      $��24��5�      $��24��5�       5�      �� *AudioPlayerVM.IsPlaying+  BoolToImageConverter �� 	Converter# [ 	 �� Mode$	 OneWay=���5�      �� *%#AudioPlayerVM.PlayPauseAudioCommand+�� 5�      -   5        $ nextTrackButton��5�      $��3��5�      #��[ 5�      $��24��5�      $��24��5�      $6 0pack://application:,,,/Images/Transport/next.png��5�      ��5�      �� *PlaylistVM.NextTrackCommand+�� 5�      -   5        $ 
stopButton��5�      $��2��5�      #��[ 5�      $; 5pack://application:,,,/Images/Transport/stopsmall.png��5�      $��24��5�      $��24��5�      ��5�      �� * AudioPlayerVM.StopAudioCommand+5�      � 5�      $��1��6    5�      �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   .+�5�      �� $��0��6   $��24��5�      $��24��5�        LoopCheckbox#��[ 5�      
 d� 	IsChecked
 5�      �� *PlaylistVM.LoopOn+$	 OneWayToSource=��� 5�      $��1��6   $��24��5�      $��24��5�        ShuffleCheckbox#��[ 5�      
 5�      �� *PlaylistVM.ShuffleOn+$	 OneWayToSource=�5�      2   ,AudioPlayerMVVMandNAudio.VolumeSliderControl  5�      -   5          Name$ volumeSliderControl��5�   (   $��2��5�   (    5�   (   �� *AudioPlayerVM+5�      4   .AudioPlayerMVVMandNAudio.PositionSliderControl  5�   
   -   5          Name$ audioPlayerControl��5�   &   $��2��5�   &    5�   &   �� *AudioPlayerVM+.   (AudioPlayerMVVMandNAudio.PlaylistControl  5�   
   -	   5          Name$ playlistControl��5�       $��3��5�       $��Stretch=�5�        5�       �� *
PlaylistVM+   IncomingFiles 5�       �� *IncomingFiles+   AddFilesCommand 5�       �� *AddFilesAsyncCommand+   MoveItemCommand 5�       �� *MoveItemCommand+   
TargetItem 5�       �� *
TargetItem+   	MovedItem 5�       �� *	MovedItem+   OnWindowClosingCommand 5�       �� *OnWindowClosingCommand+   DoubleClickCommand 5�       �� *LoadSelectedTrackCommand+   DeletePressedCommand 5�       �� *!RemoveTracksFromPlaylistCommand+�� 5�   
   $��4��6   .+�5�      � $��3,0,3,3q�6    5�      �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   �� 5�      $
��auto �6'   .+�5�      �� $��0��6     TrackInfoTextblock#��[ 5�       	 ForegroundSimpleOrangeBrush#��[	 5�      ��5�      �� *&$AudioPlayerVM.BufferTrack.TrackTitle+$	 OneWay=�5�      �� 5�      $��1��6   #��[ 5�       #
 ForegroundSimpleLightGreenBrush#��[
 5�      ��5�      �� *" AudioPlayerVM.BufferTrack.Artist+$	 OneWay=�5�      �� 5      $��2��6   #��[ 5      " ForegroundSimpleDarkGreenBrush#��[ 5     ��5     �� *!AudioPlayerVM.BufferTrack.Album+$	 OneWay=�5     5     � 5	     $��3,0,3,3q�6    5
     �� 5     $
��auto �6'   �� 5     $
��auto �6'   �� 5     $
��auto �6'   �� 5     $
��auto �6'   �� 5     $	��13* �6'   �� 5     $��3* �6'   .+�5     �� $��3��6   #��[ 5     $	��500��5     $��CharacterEllipsis=�5     ��5     �� **(AudioPlayerVM.BufferTrack.FileProperties+$	 OneWay=�5     5     5     5!     5"     