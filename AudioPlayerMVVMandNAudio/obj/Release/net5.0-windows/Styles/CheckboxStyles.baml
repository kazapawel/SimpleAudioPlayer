   M S B A M L   `   `   `  ���� S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   MuteAudioCheckbox �� Property �� Value �� 
TargetType ��Name ��Name J� RenderTransformOrigin �� BitmapScalingMode �� EdgeMode	 �� ScaleX
 �� ScaleY �� Angle �� Triggers P� Property P� Value �� 
TargetName :� RoutedEvent �� 
TargetName �� TargetProperty	 W� To g� Duration  LoopCheckbox  ShuffleCheckbox�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     0local&clr-namespace:AudioPlayerMVVMandNAudio  %z  &	        &	 \    &	 �
    �� "  ��.���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $ checkboxBorder��
������ ._��� $ checkboxImage��$;��5pack://application:,,,/Images/Volume/audio unmute.png��$��20��$��20��$ .5,.5&�$��Center=�$��Center=�$ HighQuality=�$ Aliased=�z�V� .~��� 
ImageScale��$	 1Q�$
 1Q��� ImageRotate��$ 0Q� P�  �@d�	IsChecked$
 TrueY�. ��� $ checkboxImage�� � ��$9 3pack://application:,,,/Images/Volume/audio mute.png��:� $ 
MouseEnter��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 
0:0:0:0.05D�:� $ 
MouseLeave��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1$ 	0:0:0:0.1D�W� $ ImageRotate��$ (RotateTransform.Angle)� 0$ 	0:0:0:0.1D�:� $ PreviewMouseDown��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.5$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.5$ 	0:0:0:0.1D�:� $ PreviewMouseUp��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 	0:0:0:0.1D��� "  ��.���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $ checkboxBorder��
������ ._��� $ checkboxImage��$4��.pack://application:,,,/Images/Options/loop.png��$��15��$��15��$ .5,.5&�$��Center=�$��Center=�$ HighQuality=�$ Aliased=�z�V� .~��� 
ImageScale��$	 1Q�$
 1Q��� ImageRotate��$ 0Q� P�  �@d�	IsChecked$
 TrueY�. ��� $ checkboxBorder�� � ��
 �  ���� $ checkboxImage�� � ��$4 .pack://application:,,,/Images/Options/loop.png��:� $ 
MouseEnter��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 
0:0:0:0.05D�:� $ 
MouseLeave��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1$ 	0:0:0:0.1D�W� $ ImageRotate��$ (RotateTransform.Angle)� 0$ 	0:0:0:0.1D�:� $ PreviewMouseDown��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.5$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.5$ 	0:0:0:0.1D�:� $ PreviewMouseUp��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 	0:0:0:0.1D��� "  ��.���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $ checkboxBorder��
������ ._��� $ checkboxImage��$7��1pack://application:,,,/Images/Options/shuffle.png��$��15��$��15��$ .5,.5&�$��Center=�$��Center=�$ HighQuality=�$ Aliased=�z�V� .~��� 
ImageScale��$	 1Q�$
 1Q��� ImageRotate��$ 0Q� P�  �@d�	IsChecked$
 TrueY�. ��� $ checkboxBorder�� � ��
 �  ���� $ checkboxImage�� � ��$7 1pack://application:,,,/Images/Options/shuffle.png��:� $ 
MouseEnter��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 
0:0:0:0.05D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 
0:0:0:0.05D�:� $ 
MouseLeave��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1$ 	0:0:0:0.1D�W� $ ImageRotate��$ (RotateTransform.Angle)� 0$ 	0:0:0:0.1D�:� $ PreviewMouseDown��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.5$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.5$ 	0:0:0:0.1D�:� $ PreviewMouseUp��.[��� .���� .��W� $ 
ImageScale��$ (ScaleTransform.ScaleX)� 1.1$ 	0:0:0:0.1D�W� $ 
ImageScale��$ (ScaleTransform.ScaleY)� 1.1$ 	0:0:0:0.1D�