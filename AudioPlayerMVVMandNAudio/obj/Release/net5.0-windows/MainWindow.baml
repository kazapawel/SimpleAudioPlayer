   M S B A M L   `   `   `  ���� S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  )    #AudioPlayerMVVMandNAudio.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:AudioPlayerMVVMandNAudio     -     � Title$  
MainWindow��   MainWindowStyle2#��[  $	��320��$	��172��$	��320��$	��450��  SourceCodeProMediumItalic#��[  J� 	AllowDrop$
 True�� � Icon$. (pack://application:,,,/Images/pigsad.png�� � DataContext ,   &AudioPlayerMVVMandNAudio.ApplicationVM  .���  � RowDefinitions �� $��30 ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+��� $��0��  BlueMainBrush#��[ ._��  � ColumnDefinitions �� $
��auto ��� $��* ��� $
��auto ��� $
��auto �.+��� $��0��  ImageButton#��[ $��21��$��21��$��5,0,0,0q�
 � Tag$. (pack://application:,,,/Images/pigsad.png���� -   $��2��$��30��  SystemIconButtonTextbox#��[ $ -���� -   $��3��$��30��#��[ $ x��� $��1�� �� $
��auto ��� $
��auto ��� $��* �.+�� $��0��$��5,5,0,5q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��#��[ $��24��$��24��$: 4pack://application:,,,/Images/Transport/previous.png������ *!PlaylistVM.PreviousTrackCommand+�� -    ��Name$ 
playButton��$��1��#��[ $��24��$��24�� �� *AudioPlayerVM.IsPlaying+  BoolToImageConverter �� 	Converter# [ 	 �� Mode$	 OneWay=����� *%#AudioPlayerVM.PlayPauseAudioCommand+�� -   $ nextTrackButton��$��3��#��[ $��24��$��24��$6 0pack://application:,,,/Images/Transport/next.png������ *PlaylistVM.NextTrackCommand+�� -   $ 
stopButton��$��2��#��[ $; 5pack://application:,,,/Images/Transport/stopsmall.png��$��24��$��24������ * AudioPlayerVM.StopAudioCommand+� $��1�� �� $
��auto ��� $
��auto �.+��� $��0��$��24��$��24��  LoopCheckbox#��[ 
 d� 	IsChecked
 �� *PlaylistVM.LoopOn+$	 OneWayToSource=��� $��1��$��24��$��24��  ShuffleCheckbox#��[ 
 �� *PlaylistVM.ShuffleOn+$	 OneWayToSource=�2   ,AudioPlayerMVVMandNAudio.VolumeSliderControl  -     Name$ volumeSliderControl��$��2�� �� *AudioPlayerVM+4   .AudioPlayerMVVMandNAudio.PositionSliderControl  -     Name$ audioPlayerControl��$��2�� �� *AudioPlayerVM+.   (AudioPlayerMVVMandNAudio.PlaylistControl  -	     Name$ playlistControl��$��3��$��Stretch=� �� *
PlaylistVM+   IncomingFiles �� *IncomingFiles+   AddFilesCommand �� *AddFilesAsyncCommand+   MoveItemCommand �� *MoveItemCommand+   
TargetItem �� *
TargetItem+   	MovedItem �� *	MovedItem+   OnWindowClosingCommand �� *OnWindowClosingCommand+   DoubleClickCommand �� *LoadSelectedTrackCommand+   DeletePressedCommand �� *!RemoveTracksFromPlaylistCommand+�� $��4��.+�� $��3,0,3,3q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��  TrackInfoTextblock#��[  	 ForegroundSimpleOrangeBrush#��[	 ���� *&$AudioPlayerVM.BufferTrack.TrackTitle+$	 OneWay=��� $��1��#��[  #
 ForegroundSimpleLightGreenBrush#��[
 ���� *" AudioPlayerVM.BufferTrack.Artist+$	 OneWay=��� $��2��#��[  " ForegroundSimpleDarkGreenBrush#��[ ���� *!AudioPlayerVM.BufferTrack.Album+$	 OneWay=�� $��3,0,3,3q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $	��13* ��� $��3* �.+��� $��3��#��[ $	��500��$��CharacterEllipsis=����� **(AudioPlayerVM.BufferTrack.FileProperties+$	 OneWay=�